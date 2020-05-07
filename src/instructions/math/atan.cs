namespace ancient.runtime
{
    public class atan : InstructionDynamic
    {
        public atan(byte? resultCell, byte? cell1)
            : base(IID.atan, resultCell, cell1) { }
    }
}