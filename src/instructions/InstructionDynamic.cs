namespace ancient.runtime
{
    using System.Linq;
    using emit.@unsafe;
    using exceptions;

    public abstract class InstructionDynamic : InstructionWithArgs
    {
        protected InstructionDynamic(IID opCode) : base(opCode) { }
        public enum Mode : byte
        {
            StackOnly = 0x0,
            ResultCellOnly = 0x1,
            ArgCellAndStack = 0x2,
            All = 0x3,
        }

        protected InstructionDynamic(IID opCode, byte? resultCell, params byte[] arguments)
            : base(opCode)
        {
            c1 = resultCell;
            args = arguments;
            if (arguments.Length > 2)
                throw new TooManyArgumentsException();
        }

        protected override void OnCompile()
        {
            if (c1 is null && !args.Any())
                Construct(x3: (byte)Mode.StackOnly);
            if (c1 is { } resultCell && !args.Any())
            {
                var (r1, r2) = new d8u(resultCell);
                Construct(r1, r2, x3: (byte)Mode.ResultCellOnly);
            }
            if (c1 is null && args.Any() && args.Length == 1)
            {
                var (r3, r4) = new d8u(args.First());
                Construct(r3: r3, u1: r4, x3: (byte)Mode.ArgCellAndStack);
            }
            if (c1 is null && args.Any() && args.Length == 2)
            {
                var (r3, r4) = new d8u(args.First());
                var (u2, u3) = new d8u(args.Last());
                Construct(r3: r3, u1: r4, u2: u2, x1: u3, x3: (byte)Mode.ArgCellAndStack);
            }
            if (c1 is { } result1 && args.Any() && args.Length == 1)
            {
                var (r1, r2) = new d8u(result1);
                var (r3, r4) = new d8u(args.First());
                Construct(r1, r2, r3, r4, x3: (byte)Mode.All);
            }
            if (c1 is { } result2 && args.Any() && args.Length == 2)
            {
                var (r1, r2) = new d8u(result2);
                var (r3, r4) = new d8u(args.First());
                var (u2, u3) = new d8u(args.Last());
                Construct(r1, r2, r3, r4, u2, u3, x3: (byte)Mode.All);
            }
        }
    }
}