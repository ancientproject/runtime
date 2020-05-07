namespace ancient.runtime
{
    public class tanh : InstructionDynamic
    {
        public tanh(byte? resultCell, byte cell1)
            : base(IID.tanh, resultCell, cell1) { }
    }
}