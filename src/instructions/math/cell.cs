namespace ancient.runtime
{
    public class cell : AdvancedInstruction
    {
        public cell(byte? resultCell, byte? cell1, byte? cell2)
            : base(IID.cell, resultCell, cell1, cell2) { }
    }
}