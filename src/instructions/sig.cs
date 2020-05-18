namespace ancient.runtime
{
    using System.Collections.Generic;
    using System.Text;
    using emit.@unsafe;

    public class sig : Instruction
    {
        private readonly string _name;
        private readonly byte _count;
        private readonly int _returnTypeHash;

        public sig(string name, int count) : base(IID.sig)
        {
            _name = name;
            _count = (byte)count;
        }

        protected override void OnCompile()
        {
            var (r1, r2) = new d8u(_count);
            //var (r3, u1, u2, x1, x2, x3, n7, n8) = new d32u();
            Construct(r1, r2);
        }

        public override bool HasMetadata() => true;
        protected internal override byte[] metadataBytes
        {
            get
            {
                var len = Encoding.UTF8.GetByteCount($"function->{_name}");
                var bu = new List<byte>();
                bu.AddRange(MetaTemplate.By(TemplateType.STR).Len(len).ToBytes());
                bu.AddRange(Encoding.UTF8.GetBytes($"function->{_name}"));
                return bu.ToArray();
            }
        }
    }
}