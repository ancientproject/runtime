namespace ancient.runtime
{
    public class log : AdvancedInstruction
    {
        public log(byte? resultCell, byte? cell1, byte? cell2)
            : base(IID.log, resultCell, cell1, cell2) { }
    }
}