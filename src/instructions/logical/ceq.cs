namespace ancient.runtime
{
    public class ceq : AdvancedInstruction
    {
        public ceq(byte? resultCell, byte? cell1, byte? cell2)
            : base(IID.ceq, resultCell, cell1, cell2) { }
    }
}