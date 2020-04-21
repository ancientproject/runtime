namespace ancient.runtime
{
    using emit.@unsafe;

    public class rfd : Instruction
    {
        private readonly byte _addressDev;
        private readonly byte _addressField;

        public rfd(byte addressDev, byte addressField) : base(IID.rfd)
        {
            _addressDev = addressDev;
            _addressField = addressField;
        }

        protected override void OnCompile()
        {
            var (r1, r2) = new d8u(_addressDev);
            var (u1, u2) = new d8u(_addressField);
            Construct(r1, r2, u1: u1, u2: u2);
        }
    }
}