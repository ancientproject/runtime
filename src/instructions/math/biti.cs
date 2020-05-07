namespace ancient.runtime
{
    public class biti : InstructionDynamic
    {
        public biti(byte? resultCell, byte cell1)
            : base(IID.biti, resultCell, cell1) { }
    }
}