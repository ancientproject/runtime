namespace ancient.runtime.emit.sys
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using @unsafe;

    public abstract class ExternType
    {
        public static ExternType Find(params d8u[] bytes) 
            => Find(Encoding.ASCII.GetString(bytes.Select(u8 => u8.Value).ToArray()));

        public static ExternType Find(string code)
        {
            var type = Type.GetType($"ancient.runtime.emit.sys.{code}_Type");
            if(type is null)
                return new Unknown_Type();
            return Activator.CreateInstance(type) as ExternType;
        }

        public static ulong FindAndConstruct(string code)
        {
            ValidateTypeCode(code);
            return ulong.Parse($"00{string.Join("", Encoding.ASCII.GetBytes(code).Select(x => $"{x:X}"))}",
                NumberStyles.AllowHexSpecifier);
        }

        public static d32i FindAndDeconstruct(string code)
        {
            ValidateTypeCode(code);
            return new d32i(int.Parse($"{string.Join("", Encoding.ASCII.GetBytes(code).Select(x => $"{x:X}"))}",
                NumberStyles.AllowHexSpecifier));
        }

        public static ExternType FindAndConstruct(ulong idx)
        {
            var code = string.Join("", Encoding.ASCII.GetString(BitConverter.GetBytes(idx))
                .TrimEnd('\0').Reverse());
            var type = ValidateTypeCode(code);
            return Activator.CreateInstance(type) as ExternType;
        }

        private static Type ValidateTypeCode(string code)
        {
            const string target = "ancient.runtime.emit.sys.{0}_Type";
            var path = string.Format(target, code);
            var type = Type.GetType(path);
            if(type is null)
                throw new AggregateException($"Cannot find {path} type.");
            return type;
        }

        public string ShortName => this.GetType().Name.Replace("_Type", "");
    }
}