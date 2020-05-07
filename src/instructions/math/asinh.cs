namespace ancient.runtime
{
    public class asinh : InstructionDynamic
    {
        public asinh(byte? resultCell, byte cell1)
            : base(IID.asinh, resultCell, cell1) { }
    }
}