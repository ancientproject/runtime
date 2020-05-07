namespace ancient.runtime
{
    public class div : InstructionDynamic
    {
        public div(byte? resultCell, byte? cell1)
            : base(IID.div, resultCell, cell1) { }
    }
}