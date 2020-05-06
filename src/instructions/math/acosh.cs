namespace ancient.runtime
{
    public class acosh : AdvancedInstruction
    {
        public acosh(byte? resultCell, byte? cell1, byte? cell2)
            : base(IID.acosh, resultCell, cell1, cell2) { }
    }
}