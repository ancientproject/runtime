namespace ancient.runtime
{
    public class max : InstructionDynamic
    {
        public max(byte? resultCell, byte cell1, byte cell2)
            : base(IID.max, resultCell, cell1, cell2) { }
    }
}