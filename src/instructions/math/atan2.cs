namespace ancient.runtime
{
    public class atan2 : AdvancedInstruction
    {
        public atan2(byte? resultCell, byte? cell1, byte? cell2)
            : base(IID.atan2, resultCell, cell1, cell2) { }
    }
}