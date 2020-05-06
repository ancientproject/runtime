namespace ancient.runtime
{
    public class abs : AdvancedInstruction
    {
        public abs(byte? resultCell, byte? cell1, byte? cell2)
            : base(IID.abs, resultCell, cell1, cell2) { }
    }
}