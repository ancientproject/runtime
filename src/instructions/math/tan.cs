namespace ancient.runtime
{
    public class tan : AdvancedInstruction
    {
        public tan(byte? resultCell, byte? cell1, byte? cell2)
            : base(IID.tan, resultCell, cell1, cell2) { }
    }
}