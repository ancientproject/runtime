namespace ancient.runtime
{
    public class acosh : InstructionDynamic
    {
        public acosh(byte? resultCell, byte? cell1)
            : base(IID.acosh, resultCell, cell1) { }
    }
}