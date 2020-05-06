namespace ancient.runtime
{
    public class or : AdvancedInstruction
    {
        public or(byte? resultCell, byte? cell1, byte? cell2) 
            : base(IID.or, resultCell, cell1, cell2) { }
    }
}