namespace ancient.runtime
{
    public class bitd : InstructionDynamic
    {
        public bitd(byte? resultCell, byte cell1)
            : base(IID.bitd, resultCell, cell1) { }
    }
}