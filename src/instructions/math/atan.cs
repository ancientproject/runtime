namespace ancient.runtime
{
    public class atan : AdvancedInstruction
    {
        public atan(byte? resultCell, byte? cell1, byte? cell2)
            : base(IID.atan, resultCell, cell1, cell2) { }
    }
}