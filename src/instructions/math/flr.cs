namespace ancient.runtime
{
    public class flr : AdvancedInstruction
    {
        public flr(byte? resultCell, byte? cell1, byte? cell2)
            : base(IID.flr, resultCell, cell1, cell2) { }
    }
}