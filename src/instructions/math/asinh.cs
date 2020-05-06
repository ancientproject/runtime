namespace ancient.runtime
{
    public class asinh : AdvancedInstruction
    {
        public asinh(byte? resultCell, byte? cell1, byte? cell2)
            : base(IID.acosh, resultCell, cell1, cell2) { }
    }
}