namespace ancient.runtime
{
    public class atanh : InstructionDynamic
    {
        public atanh(byte? resultCell, byte cell1)
            : base(IID.atanh, resultCell, cell1) { }
    }
}