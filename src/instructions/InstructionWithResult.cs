namespace ancient.runtime
{
    public abstract class InstructionWithResult : Instruction
    {
        internal byte? c1 { get; set; }

        protected InstructionWithResult(IID opCode) : base(opCode)  { }
    }
}