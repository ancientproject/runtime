namespace ancient.runtime
{
    public class cbrt : InstructionDynamic
    {
        public cbrt(byte? resultCell, byte cell1)
            : base(IID.cbrt, resultCell, cell1) { }
    }
}