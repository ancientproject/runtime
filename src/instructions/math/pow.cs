namespace ancient.runtime
{
    public class pow : InstructionDynamic
    {
        public pow(byte? resultCell, byte cell1, byte cell2)
            : base(IID.pow, resultCell, cell1, cell2) { }
    }
}