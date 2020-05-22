namespace ancient.runtime
{
    using System.Collections.Generic;
    using System.Text;
    using emit.@unsafe;
    using @unsafe;

    public class use : Instruction
    {
        public use(string path) : base(IID.use) => ModulePath = path;
        public string ModulePath { get; set; }
        protected override void OnCompile()
            => Construct(new d32i(NativeString.GetHashCode(ModulePath)));

        public override bool HasMetadata() => true;

        protected internal override byte[] metadataBytes
        {
            get
            {
                var len = Encoding.UTF8.GetByteCount(ModulePath);
                var bu = new List<byte>();
                bu.AddRange(MetaTemplate.By(TemplateType.STR).Len(len).ToBytes());
                bu.AddRange(Encoding.UTF8.GetBytes(ModulePath));
                return bu.ToArray();
            }
        }
    }
}