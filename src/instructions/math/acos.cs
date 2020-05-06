namespace ancient.runtime
{
    public class acos : AdvancedInstruction
    {
        public acos(byte? resultCell, byte? cell1, byte? cell2)
            : base(IID.acos, resultCell, cell1, cell2) { }
    }
}