namespace ancient.runtime
{
    public class abs : InstructionDynamic
    {
        public abs(byte? resultCell, byte cell1)
            : base(IID.abs, resultCell, cell1) { }
    }
}