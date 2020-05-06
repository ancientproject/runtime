namespace ancient.runtime
{
    public class bitd : AdvancedInstruction
    {
        public bitd(byte? resultCell, byte? cell1, byte? cell2)
            : base(IID.bitd, resultCell, cell1, cell2) { }
    }
}