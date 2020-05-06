namespace ancient.runtime
{
    public class cosh : AdvancedInstruction
    {
        public cosh(byte? resultCell, byte? cell1, byte? cell2)
            : base(IID.cosh, resultCell, cell1, cell2) { }
    }
}