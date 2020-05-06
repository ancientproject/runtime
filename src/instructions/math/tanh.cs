namespace ancient.runtime
{
    public class tanh : AdvancedInstruction
    {
        public tanh(byte? resultCell, byte? cell1, byte? cell2)
            : base(IID.tanh, resultCell, cell1, cell2) { }
    }
}