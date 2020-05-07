namespace ancient.runtime
{
    public class acos : InstructionDynamic
    {
        public acos(byte? resultCell, byte cell1)
            : base(IID.acos, resultCell, cell1) { }
    }
}