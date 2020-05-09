﻿#pragma warning disable 618

namespace ancient.runtime
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public abstract class Instruction : OpCode
    {
        public IID ID { get; protected set; }
        public ushort OPCode { get; protected set; }

        private byte _r1, _r2, _r3, _u1, _u2, _x1, _x2, _x3;

        protected Instruction()
        {
        }

        protected Instruction(IID id)
        {
            ID = id;
            OPCode = (ushort)id.getOpCode();
        }

        public void SetOpCode(IID id)
        {
            ID = id;
            OPCode = (ushort)id.getOpCode();
        }

        public virtual ulong Assembly()
        {
            OnCompile();
            Func<int> Shift = ShiftFactory.Create(36);

            var op1 = ((OPCode & 0xF0UL) >> 4) << Shift();
            var op2 = ((OPCode & 0x0FUL) >> 0) << Shift();
            var rr1 = (ulong)_r1 << Shift();
            var rr2 = (ulong)_r2 << Shift();
            var rr3 = (ulong)_r3 << Shift();
            var ru1 = (ulong)_u1 << Shift();
            var ru2 = (ulong)_u2 << Shift();
            var rx1 = (ulong)_x1 << Shift();
            var rx2 = (ulong)_x2 << Shift();
            var rx3 = (ulong)_x3 << Shift();
            return op1 | op2 | rr1 |
                   rr2 | rr3 |
                   ru1 | ru2 |
                   rx1 | rx2 | rx3;
        }

        public override byte[] GetBodyILBytes() => BitConverter.GetBytes(Assembly()).Reverse().ToArray();

        public override byte[] GetMetaDataILBytes() => !HasMetadata() ? Array.Empty<byte>() : metadataBytes;

        protected internal virtual byte[] metadataBytes { get; } = Array.Empty<byte>();

        protected abstract void OnCompile();

        #region serviced

        public static implicit operator ulong(Instruction i) => i.Assembly();

        public override string ToString() => $"{ID} [{string.Join(" ", GetBodyILBytes().Select(x => x.ToString("X2")))}]";

        public void Construct(byte r1 = 0, byte r2 = 0, byte r3 = 0, byte u1 = 0, byte u2 = 0, byte x1 = 0,
            byte x2 = 0, byte x3 = 0) => SetRegisters(r1, r2, r3, u1, u2, x1, x2, x3);

        [Obsolete("use Construct")]
        public void SetRegisters(byte r1 = 0, byte r2 = 0, byte r3 = 0, byte u1 = 0, byte u2 = 0, byte x1 = 0, byte x2 = 0, byte x3 = 0)
        {
            _r1 = r1;
            _r2 = r2;
            _r3 = r3;
            _u1 = u1;
            _u2 = u2;
            _x1 = x1;
            _x2 = x2;
            _x3 = x3;
        }

        public static Instruction Summon(IID id, params object[] args)
        {
            var currentAsm = typeof(Instruction).Assembly;
            var classes =
                from type in currentAsm.GetTypes()
                where type.IsClass
                where !type.IsAbstract
                where type.IsSubclassOf(typeof(Instruction))
                select type;

            foreach (var @class in classes)
            {
                var name = @class.Name;
                static object @default(ParameterInfo t) => t.ParameterType.IsValueType ?
                    Activator.CreateInstance(t.ParameterType) :
                    null;
                static T Activate<T>(Type t, object[] args) where T : class
                {
                    var @params = t.GetConstructors().First().GetParameters();
                    if (!@params.Any() || !args.Any())
                        return Activator.CreateInstance(t) as T;
                    if (args.Length == @params.Length)
                        return Activator.CreateInstance(t, args, null) as T;
                    return default;
                }
                static object[] PrepareArgs(object[] args, ParameterInfo[] target) 
                    => target.Select((v, i) => (args.Length > i ? args[i] : null) ?? @default(v)).ToArray();

                static Instruction ConstructUnsafeObject(Type @class) 
                    => Activate<Instruction>(@class, @class.GetConstructors().First().GetParameters().Select(@default).ToArray());

                if(ConstructUnsafeObject(@class).ID != id)
                    continue;
                var ctors = @class.GetConstructors();
                if (!ctors.Any())
                    throw new Exception($"[{id}] Nearby type '{@class.FullName}' is not valid constructors.");
                var ctor = ctors.First();
                
                var classConstructorParams = ctor.GetParameters();
                if (!args.Any())
                    args = classConstructorParams.Select(@default).ToArray();
                if (!(ctor.Invoke(PrepareArgs(args, classConstructorParams)) is Instruction inst))
                    throw new InvalidOperationException($"Failed invoke constructor for [{id}:{@class.Name}].");
                return inst;
            }
            throw new InvalidOperationException($"Not found class for '{id}' operation.");
        }

        public static int GetArgumentCountBy(IID id)
            => Summon(id).GetType().GetConstructors().First().GetParameters().Length;

        #endregion serviced


    }
}