namespace ancient.runtime
{
    public class log10 : AdvancedInstruction
    {
        public log10(byte? resultCell, byte? cell1, byte? cell2)
            : base(IID.log10, resultCell, cell1, cell2) { }
    }
}