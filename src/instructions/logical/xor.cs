namespace ancient.runtime
{
    public class xor : InstructionDynamic
    {
        public xor(byte? resultCell, byte cell1, byte cell2) 
            : base(IID.xor, resultCell, cell1, cell2) { }
    }
}