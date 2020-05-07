namespace ancient.runtime
{
    public abstract class InstructionWithArgs : InstructionWithResult
    {
        internal byte[] args { get; set; } = new byte[0];
        protected InstructionWithArgs(IID opCode) : base(opCode) { }
    }
}