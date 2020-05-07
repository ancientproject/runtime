namespace ancient.runtime
{
    public class sqrt : InstructionDynamic
    {
        public sqrt(byte? resultCell, byte? cell1, byte? cell2) 
            : base(IID.sqrt, resultCell, cell1, cell2) { }
    }
}