namespace ancient.runtime
{
    using emit.@unsafe;

    public class dif_t : Instruction
    {
        private readonly byte _cell;
        private readonly byte _skip;

        public dif_t(byte cell, byte skip) : base(IID.dif_t)
        {
            _cell = cell;
            _skip = skip;
        }
        protected override void OnCompile()
        {
            var (r1, r2) = new d8u(_cell);
            var (r3, u1) = new d8u(_skip);
            base.Construct(r1, r2, r3, u1);
        }
    }
    public class dif_f : Instruction
    {
        private readonly byte _cell;
        private readonly byte _skip;

        public dif_f(byte cell, byte skip) : base(IID.dif_f)
        {
            _cell = cell;
            _skip = skip;
        }
        protected override void OnCompile()
        {
            var (r1, r2) = new d8u(_cell);
            var (r3, u1) = new d8u(_skip);
            base.Construct(r1, r2, r3, u1);
        }
    }
}