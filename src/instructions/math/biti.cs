namespace ancient.runtime
{
    public class biti : AdvancedInstruction
    {
        public biti(byte? resultCell, byte? cell1, byte? cell2)
            : base(IID.biti, resultCell, cell1, cell2) { }
    }
}