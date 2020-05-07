namespace ancient.runtime
{
    public class cosh : InstructionDynamic
    {
        public cosh(byte? resultCell, byte? cell1)
            : base(IID.cosh, resultCell, cell1) { }
    }
}