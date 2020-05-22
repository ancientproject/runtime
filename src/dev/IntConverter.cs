﻿namespace ancient.runtime.hardware
{
    using System;
    using System.ComponentModel;

    public class IntConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return (sourceType == typeof(int) 
                    || sourceType == typeof(byte) 
                    || sourceType == typeof(short) 
                    || sourceType == typeof(ushort)
                    || sourceType == typeof(sbyte)
                    || sourceType == typeof(uint)
                    || sourceType == typeof(long)
                    || sourceType == typeof(ulong)
                    || base.CanConvertFrom(context, sourceType));
        }
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is int t    ) return (char) t;
            if (value is uint ut  ) return (char) ut;
            if (value is short s  ) return (char) s;
            if (value is ushort us) return (char) us;
            if (value is byte b   ) return (char) b;
            if (value is long l   ) return (char) l;
            if (value is ulong ul ) return (char) ul;
            return base.ConvertFrom(context, culture, value);
        }


        public static void Register<T>()
        {
            var attr = new Attribute[1];
            var vConv = new TypeConverterAttribute(typeof(IntConverter));
            attr[0] = vConv;
            TypeDescriptor.AddAttributes(typeof(T), attr);
        }
    }
}