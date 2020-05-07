namespace ancient.runtime
{
    public class add : InstructionDynamic
    {
        public add(byte? resultCell, byte? cell1, byte? cell2)
            : base(IID.add, resultCell, cell1, cell2) { }
    }
}