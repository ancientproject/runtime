namespace ancient.runtime
{
    using System.Collections.Generic;
    using System.Text;
    using emit.sys;
    using emit.@unsafe;

    public class sig : Instruction
    {
        private readonly string _name;
        private readonly byte _count;
        private readonly string _returnType;

        public sig(string name, int count, string returnType) : base(IID.sig)
        {
            _name = name;
            _count = (byte)count;
            _returnType = returnType;
        }

        protected override void OnCompile()
        {
            var (r1, r2) = new d8u(_count);
            var (r3, u1, u2, x1, x2, x3, x4, o1) = ExternType.FindAndDeconstruct(_returnType);
            Construct(r1, r2, r3, u1, u2, x1, x2, x3, x4, o1);
        }

        public override bool HasMetadata() => true;
        protected internal override byte[] metadataBytes
        {
            get
            {
                var len = Encoding.UTF8.GetByteCount($"function {_name}() -> {_returnType}");
                var bu = new List<byte>();
                bu.AddRange(MetaTemplate.By(TemplateType.STR).Len(len).ToBytes());
                bu.AddRange(Encoding.UTF8.GetBytes($"function {_name}() -> {_returnType}"));
                return bu.ToArray();
            }
        }
    }
}