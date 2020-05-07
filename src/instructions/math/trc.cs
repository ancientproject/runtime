namespace ancient.runtime
{
    public class trc : InstructionDynamic
    {
        public trc(byte? resultCell, byte cell1)
            : base(IID.trc, resultCell, cell1) { }
    }
}