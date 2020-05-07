namespace ancient.runtime
{
    using emit.@unsafe;

    public abstract class AdvancedInstruction : Instruction
    {
        internal byte? c1 { get; }
        internal byte? c2 { get; }
        internal byte? c3 { get; }

        protected AdvancedInstruction(IID opCode, byte? resultCell, byte? cell1, byte? cell2) 
            : base(opCode)
        {
            c1 = resultCell;
            c2 = cell1;
            c3 = cell2;
        }

        protected override void OnCompile()
        {
            if (c1 is null && c2 is null && c3 is null)
                Construct(x3: (byte)Mode.StackOnly);
            if (c1 is {} resultCell && c2 is null && c3 is null)
            {
                var (r1, r2) = new d8u(resultCell);
                Construct(r1, r2, x3: (byte)Mode.ResultCellOnly);
            }
            if (c1 is null && c2 is {} cell1 && c3 is {} cell2)
            {
                var (r3, r4) = new d8u(cell1);
                var (u2, u3) = new d8u(cell2);
                Construct(r3: r3, u1: r4, u2: u2, x1: u3, x3: (byte)Mode.TwoCellAndStack);
            }
            if (c1 is {} result && c2 is { } cell3 && c3 is { } cell4)
            {
                var (r1, r2) = new d8u(result);
                var (r3, r4) = new d8u(cell3);
                var (u2, u3) = new d8u(cell4);
                Construct(r1, r2, r3, r4, u2, u3, x3: (byte)Mode.All);
            }
        }

        public enum Mode : byte
        {
            StackOnly = 0x0,
            ResultCellOnly = 0x1,
            TwoCellAndStack = 0x2,
            All = 0x3,
        }
    }
}