namespace ancient.runtime
{
    public class sin : InstructionDynamic
    {
        public sin(byte? resultCell, byte? cell1)
            : base(IID.sin, resultCell, cell1) { }
    }
}