namespace ancient.runtime
{
    public class exp : InstructionDynamic
    {
        public exp(byte? resultCell, byte? cell1)
            : base(IID.exp, resultCell, cell1) { }
    }
}