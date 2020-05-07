namespace ancient.runtime
{
    public class mul : InstructionDynamic
    {
        public mul(byte? resultCell, byte cell1, byte cell2)
            : base(IID.mul, resultCell, cell1, cell2) { }
    }
}