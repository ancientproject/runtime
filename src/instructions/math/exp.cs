namespace ancient.runtime
{
    public class exp : AdvancedInstruction
    {
        public exp(byte? resultCell, byte? cell1, byte? cell2)
            : base(IID.exp, resultCell, cell1, cell2) { }
    }
}