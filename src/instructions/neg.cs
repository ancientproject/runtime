namespace ancient.runtime
{
    using emit.@unsafe;

    public class neg : Instruction
    {
        private readonly byte _cell1;

        public neg(byte cell) : base(IID.neg) => _cell1 = cell;

        #region Overrides of Instruction

        protected override void OnCompile()
        {
            var (r1, r2) = new d8u(_cell1);
            Construct(r1, r2);
        }

        #endregion
    }
}