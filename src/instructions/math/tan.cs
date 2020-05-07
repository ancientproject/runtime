namespace ancient.runtime
{
    public class tan : InstructionDynamic
    {
        public tan(byte? resultCell, byte? cell1)
            : base(IID.tan, resultCell, cell1) { }
    }
}