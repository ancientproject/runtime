namespace ancient.runtime
{
    public class div : InstructionDynamic
    {
        public div(byte? resultCell, byte? cell1, byte? cell2)
            : base(IID.div, resultCell, cell1, cell2) { }
    }
}