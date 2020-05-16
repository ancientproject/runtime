namespace ancient.runtime.emit.sys
{
    using System.Globalization;

    public class EvaluationSegment
    {
        public byte Index { get; }
        public string Type { get; }

        public EvaluationSegment(byte index, string type)
        {
            Index = index;
            Type = type;
        }

        public (Instruction idx, Instruction type) OpCode
        {
            get
            {
                var index = new raw(ulong.Parse($"00{Index:X}0000", NumberStyles.AllowHexSpecifier));
                var type = new raw(ExternType.FindAndConstruct(Type));
                return (index, type);
            }
        }


        public static EvaluationSegment Construct(in ulong idx, in ulong type)
        {
            var i = (byte)((idx & 0x0000_0000_00FF_0000) >> 16);
            return new EvaluationSegment(i, ExternType.FindAndConstruct(type).ShortName);
        }
    }
}