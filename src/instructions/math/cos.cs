namespace ancient.runtime
{
    public class cos : InstructionDynamic
    {
        public cos(byte? resultCell, byte? cell1)
            : base(IID.cos, resultCell, cell1) { }
    }
}