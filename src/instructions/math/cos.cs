namespace ancient.runtime
{
    public class cos : AdvancedInstruction
    {
        public cos(byte? resultCell, byte? cell1, byte? cell2)
            : base(IID.cos, resultCell, cell1, cell2) { }
    }
}