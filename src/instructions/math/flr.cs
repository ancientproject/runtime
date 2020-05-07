namespace ancient.runtime
{
    public class flr : InstructionDynamic
    {
        public flr(byte? resultCell, byte cell1)
            : base(IID.flr, resultCell, cell1) { }
    }
}