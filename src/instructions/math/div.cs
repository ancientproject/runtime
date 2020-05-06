namespace ancient.runtime
{
    public class div : AdvancedInstruction
    {
        public div(byte? resultCell, byte? cell1, byte? cell2)
            : base(IID.div, resultCell, cell1, cell2) { }
    }
}