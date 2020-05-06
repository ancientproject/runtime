namespace ancient.runtime
{
    public class cbrt : AdvancedInstruction
    {
        public cbrt(byte? resultCell, byte? cell1, byte? cell2)
            : base(IID.cbrt, resultCell, cell1, cell2) { }
    }
}