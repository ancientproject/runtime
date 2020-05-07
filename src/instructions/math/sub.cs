namespace ancient.runtime
{
    public class sub : InstructionDynamic
    {
        public sub(byte? resultCell, byte? cell1, byte? cell2)
            : base(IID.sub, resultCell, cell1, cell2) { }
    }
}