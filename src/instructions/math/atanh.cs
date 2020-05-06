namespace ancient.runtime
{
    public class atanh : AdvancedInstruction
    {
        public atanh(byte? resultCell, byte? cell1, byte? cell2)
            : base(IID.atanh, resultCell, cell1, cell2) { }
    }
}