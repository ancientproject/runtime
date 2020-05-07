namespace ancient.runtime
{
    public class and : InstructionDynamic
    {
        public and(byte? resultCell, byte? cell1, byte? cell2)
            : base(IID.and, resultCell, cell1, cell2) { }
    }
}