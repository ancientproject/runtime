namespace ancient.runtime
{
    public class asin : InstructionDynamic
    {
        public asin(byte? resultCell, byte? cell1)
            : base(IID.asin, resultCell, cell1) { }
    }
}