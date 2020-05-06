namespace ancient.runtime
{
    public class add : AdvancedInstruction
    {
        public add(byte? resultCell, byte? cell1, byte? cell2)
            : base(IID.add, resultCell, cell1, cell2) { }
    }
}