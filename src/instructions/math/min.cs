namespace ancient.runtime
{
    public class min : AdvancedInstruction
    {
        public min(byte? resultCell, byte? cell1, byte? cell2)
            : base(IID.min, resultCell, cell1, cell2) { }
    }
}