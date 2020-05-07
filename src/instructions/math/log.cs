namespace ancient.runtime
{
    public class log : InstructionDynamic
    {
        public log(byte? resultCell, byte? cell1)
            : base(IID.log, resultCell, cell1) { }
    }
}