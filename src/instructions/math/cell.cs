namespace ancient.runtime
{
    public class cell : InstructionDynamic
    {
        public cell(byte? resultCell, byte? cell1)
            : base(IID.cell, resultCell, cell1) { }
    }
}