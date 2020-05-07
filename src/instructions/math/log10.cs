namespace ancient.runtime
{
    public class log10 : InstructionDynamic
    {
        public log10(byte? resultCell, byte cell1)
            : base(IID.log10, resultCell, cell1) { }
    }
}