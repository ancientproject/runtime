namespace ancient.runtime
{
    public class min : InstructionDynamic
    {
        public min(byte? resultCell, byte cell1, byte cell2)
            : base(IID.min, resultCell, cell1, cell2) { }
    }
}