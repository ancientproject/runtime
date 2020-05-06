namespace ancient.runtime
{
    public class trc : AdvancedInstruction
    {
        public trc(byte? resultCell, byte? cell1, byte? cell2)
            : base(IID.trc, resultCell, cell1, cell2) { }
    }
}