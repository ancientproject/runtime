namespace ancient.runtime.@base
{
    using System;
    using System.Linq;
    using emit.sys;

    public sealed class Function
    {
        public Utb[] Arguments { get; private set; }
        public ulong[] Body => ReadBody();
        public VMRef metadataPoint { get; }
        public VMRef bodyPoint { get; }
        public string Name { get; private set; }

        public ExternType ReturnType { get; private set; }

        private IState State { get; set; }
        private IMemoryRange memory { get; set; } 
        private IntPtr handle { get; set; }



        private ulong[] ReadBody()
        {
            var (p, size) = bodyPoint;

            return Enumerable.Range(0, size)
                .Select(x => (ulong)x)
                .Select(x => memory.read(p + x))
                .ToArray();
        }

        private void LoadFunctionMetadata()
        {
            var (p, _) = metadataPoint;
            var module = memory.readString(ref p);
            var name = memory.readString(ref p);
            var arg_size = memory.read(p++);
            var args = new Utb[arg_size];

            for (var i = 0ul; i != arg_size; i++)
                args[i] = (memory.readString(ref p), memory.read(p++));
            ReturnType = ExternType.Find(memory.readString(ref p));
            handle = Module.Find(module).Handle;

            this.Name = name;
            this.Arguments = args;
        }



        public Function((VMRef metadata, VMRef body) @ref, IMemoryRange memory)
        {
            this.memory = memory;
            this.metadataPoint = @ref.metadata;
            this.bodyPoint = @ref.body;
            LoadFunctionMetadata();
        }
    }

    public readonly struct VMRef
    {
        public ushort Point { get; }
        public ushort Size { get; }

        public VMRef(ushort point, ushort size)
        {
            Point = point;
            Size = size;
        }

        public void Deconstruct(out ushort point, out ushort size)
        {
            point = Point;
            size = Size;
        }
    }

    public struct Utb
    {
        public Type Type { get; set; }
        public ulong Value { get; set; }

        public Utb(Type type, ulong value)
        {
            Type = type;
            Value = value;
        }

        public bool Is<T>() where T : ExternType 
            => typeof(T) == Type;

        public Utb(ExternType type, ulong value)
        {
            Type = type.GetType();
            Value = value;
        }

        public Utb Create<T>(ulong value) where T : ExternType 
            => new Utb(typeof(T), value);
        
        public static implicit operator Utb((string, ulong) v) 
            => new Utb(ExternType.Find(v.Item1).GetType(), v.Item2);
    }

}