namespace ancient.runtime
{
    public class or : InstructionDynamic
    {
        public or(byte? resultCell, byte? cell1, byte? cell2) 
            : base(IID.or, resultCell, cell1, cell2) { }
    }
}