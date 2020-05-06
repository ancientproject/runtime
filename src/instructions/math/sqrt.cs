namespace ancient.runtime
{
    public class sqrt : AdvancedInstruction
    {
        public sqrt(byte? resultCell, byte? cell1, byte? cell2) 
            : base(IID.sqrt, resultCell, cell1, cell2) { }
    }
}