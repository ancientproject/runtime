namespace ancient.runtime
{
    public class asin : AdvancedInstruction
    {
        public asin(byte? resultCell, byte? cell1, byte? cell2)
            : base(IID.asin, resultCell, cell1, cell2) { }
    }
}