namespace ancient.runtime.emit.sys
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using @base;
    using MoreLinq;
    using runtime.@unsafe;
    using @unsafe;
    using static System.Environment;

    public sealed class Module
    {
        public static VMContext Context { get; } = new VMContext();

        public static Module Current => modules[0x0];
        public IntPtr Handle { get; set; }
        public string Name { get; set; }
        public IBus Bus { get; set; }
        public Module(string name) => this.Name = name;


        public static Module Find(string name) =>
            modules.FirstOrDefault(x => x.Value.Name == name).Value ?? 
            throw new ModuleNotResolveException(name);


        internal static readonly Dictionary<int, Module> modules = new Dictionary<int, Module>();



        public static Module Import(string modulePath) => 
            modulePath.StartsWith("./") ? 
                ImportFromLocal(modulePath) : 
                ImportFromGlobal(modulePath);

        private static Module ImportFromLocal(string modulePath)
        {
            var exception = new ModuleNotResolveException(modulePath);
            modulePath = modulePath.Remove(0, 2);
            if (modulePath.EndsWith(".asm")) 
                modulePath = modulePath.Remove(modulePath.Length - ".asm".Length, ".asm".Length);
            var paths = new List<FileInfo>
            {
                new FileInfo($"./{modulePath}.dlx"),
                new FileInfo($"./obj/{modulePath}.dlx"),
                new FileInfo($"./bin/{modulePath}.dlx"),
            };
            var target = paths.FirstOrDefault(x => x.Exists);
            if (target is null)
                throw exception;
            return Import(AncientAssembly.LoadFrom(target.FullName));
        }

        private static Module ImportFromGlobal(string moduleName)
        {
            // TODO
            var globalPath = Path.Combine(GetFolderPath(SpecialFolder.UserProfile), ".rune/cache");
            var directory = new DirectoryInfo(globalPath);
            var files = directory.GetFiles(".dlx", SearchOption.AllDirectories);
            return Import(AncientAssembly.LoadFrom(files.First(x => x.Name.Contains(moduleName)).FullName));
        }
        
        public static ulong[] CastFromBytes(byte[] bytes)
        {
            if (bytes.Length % sizeof(ulong) == 0)
                return bytes.Batch(sizeof(ulong)).Select(x => BitConverter.ToUInt64(x.Reverse().ToArray())).Reverse().ToArray();
            throw new Exception("invalid offset file.");
        }

        public static Module Import(AncientAssembly assembly)
        {
            var module = new Module(assembly.Name);
            modules.Add(assembly.GetILCode().GetHashCode(), module);
            Current.Bus.GetState().LoadMeta(assembly.GetMetaILCode());
            ImportFunctions(assembly.GetILCode(), module)
                .Select(x => (x, module))
                .Pipe(module.RegisterFunction)
                .Consume();
            return module;
        }


        private static readonly Unicast<byte  , ulong > u8  = new Unicast<byte  , ulong>();

        public static Function[] ImportFunctions(byte[] il, Module module)
        {
            var state = Current.Bus.GetState();
            var functions = new List<Function>();

            var enumerator = CastFromBytes(il).GetEnumerator();

            ulong fetch() => enumerator.MoveNext() && enumerator.Current != null ? new d64u((ulong)enumerator.Current).Value : 0ul;

            while (enumerator.MoveNext())
            {
                if(!(enumerator.Current is ulong))
                    continue;
                var current = (ulong) enumerator.Current;
                if(state.AcceptOpCode(current) == 0xA5)
                {
                    var (_, _, _, _, r1, r2, r3, u1, u2, x1, x2, x3, x4, o1, _, _) 
                        = new d64u(current);
                    d8u arg_count = (u8 & r1, u8 & r2);
                    var returnType = ExternType.Find(
                        (u8 & r3, u8 & u1),
                        (u8 & u2, u8 & x1),
                        (u8 & x2, u8 & x3),
                        (u8 & x4, u8 & o1)
                    );
                    var v1 = fetch();
                    var lp = (v1 & 0x0000_0000_FFFF_FFFF_0000) >> 12 >> 4;
                    var p = StringLiteralMap.GetInternedString((int) lp);
                    NativeString.Unwrap(p,
                        out var functionName, false, true);

                    Debug.Assert(functionName != null, $"[native] {nameof(functionName)} != null");
                    // read argument declaration
                    var args = new Utb[arg_count];
                    for (var i = 0; i != arg_count; i++)
                        args[i] = (ExternType.FindAndConstruct(fetch()), 0);

                    var memory = Current.Bus.find(0x0) as IMemoryRange;

                    Debug.Assert(memory != null, $"{nameof(memory)} != null");

                    // write function into memory
                    var (free, startPoint) = memory.GetFreeAddress();

                    memory.writeString(ref free, module.Name);
                    memory.writeString(ref free, functionName);
                    
                    memory.write(free++, arg_count);
                    foreach (var arg in args)
                    {
                        memory.writeString(ref free, arg.ConstructType().ShortName);
                        memory.write(free++, arg.Value);
                    }
                    memory.writeString(ref free, returnType.ShortName);

                    var bodyStart = free;

                    var n_frag = fetch();
                    var complexity = 0;
                    while (state.AcceptOpCode(n_frag) != 0xA6 /* @ret */)
                    {
                        complexity++;
                        memory.write(free++, n_frag);
                        n_frag = fetch();
                        Debug.Assert(complexity < 1_000, $"function complexity < 1_000");
                    }
                    memory.write(free++, n_frag);

                    VMRef metadataRef = (startPoint, free - startPoint);
                    VMRef bodyRef = (bodyStart, free - bodyStart);
                    functions.Add(new Function((metadataRef, bodyRef), memory));
                }
            }

            return functions.ToArray();
        }

        #region Functions

        public Dictionary<string, Function> Functions = new Dictionary<string, Function>();

        public void RegisterFunction(Function func) 
            => Functions.Add(func.Name, func);
        public void RegisterFunction((Function func, Module module) box) 
            => Functions.Add($"{box.module.Name}|{box.func.Name}", box.func);

        public Function FindFunction(int hash)
        {
            var target = StringLiteralMap.GetInternedString(hash);
            NativeString.Unwrap(target,
                out var functionName, false, true);
            return modules.SelectMany(x => x.Value.Functions.Select(z => z.Value))
                .FirstOrDefault(x => x.Name == functionName);

            // TODO

            Debug.Assert(functionName.Count(x => x == '|') <= 1, "'|' in function name too many");

            if (functionName.Contains("|"))
            {
                var module = functionName.Split('|').First();
                var funcName = functionName.Split('|').Last();
                return Find(module).FindFunction(NativeString.GetHashCode(funcName));
            }

            return Functions.First(x => x.Key == functionName).Value;
        }

        #endregion

       

        #region Types
        public static ExternType[] DefinedTypes { get; } =
        {
            new u8_Type(), new u16_Type(), new u32_Type(), new u64_Type(), new f64_Type(), new u2_Type(),
            new i8_Type(), new i16_Type(), new i32_Type(), new i64_Type(), new i2_Type()
        };
        public static ExternSignature Composite(string sign, ushort index)
        {
            var s = new ExternSignature
            {
                Signature = sign, MethodIndex = index, Arguments = new List<ExternType>()
            };
            foreach (Match match in new Regex(@"(?<type>i32|i64|i16|i8|i2|u32|u64|u16|u8|u2|f64)+?\,?\s?").Matches(sign))
                s.Arguments.Add(DefinedTypes.First(x => x.GetType().Name == $"{match.Groups["type"]}_Type"));
            return s;
        }

        public static ushort CompositeIndex(string sign) 
            => ushort.Parse($"{NativeString.GetHashCode(sign):X}".Remove(0, 4), NumberStyles.AllowHexSpecifier);

        #endregion

        public static void Boot(IBus bus)
        {
            if(!modules.ContainsKey(0x0))
                modules.Add(0x0, new Module("main.module") {Bus = bus});
        }

        public override string ToString() 
            => $"Module '{Name}' [{Functions.Count} functions defined]";
    }

    public class ModuleNotResolveException : Exception
    {
        public ModuleNotResolveException(string name) : base($"Cannot find {name} module and resolve it.")
        {
            
        }
    }
}