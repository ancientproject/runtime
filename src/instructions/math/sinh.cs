namespace ancient.runtime
{
    public class sinh : InstructionDynamic
    {
        public sinh(byte? resultCell, byte? cell1)
            : base(IID.sinh, resultCell, cell1) { }
    }
}