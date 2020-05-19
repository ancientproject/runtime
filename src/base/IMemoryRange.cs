namespace ancient.runtime.@base
{
    using System;
    using System.Linq;
    using System.Text;

    public interface IMemoryRange
    {
        void write(ulong address, long data);
        void write(long address, long data);
        void write(ulong address, ulong data);
        void write(long address, ulong data);
        ulong read(ulong address);
        ulong read(long address);
    }


    public static class MemoryExtensions
    {
        public static ulong GetFreeAddress(this IMemoryRange range)
        {
            var address = range.read(0x899);
            return address == 0x0 ? 0x900 : address + 1;
        }

        public static string readString(this IMemoryRange range, ref ushort point)
        {
            var size = range.read(point++);
            var chars = new char[size];
            for (var i = 0ul; i != size; i++)
                chars[i] = MarshalChar(range.read(point++));
            return string.Join("", chars);
        }

        public static void writeString(this IMemoryRange range, ref ushort point, string value)
        {
            var size = value.Length;
            range.write(point++, size);
            for(var i = 0; i != size; i++)
                range.write(point++, MarshalChar(value[i]));
        }



        /// <summary>
        /// Cast char to uint64
        /// </summary>
        /// <remarks>
        /// Don't ever do that
        /// </remarks>
        private static ulong MarshalChar(char c)
        {
            var data = $"{c}";
            
            var empty_size = sizeof(ulong) - Encoding.UTF8.GetByteCount(data);

            return BitConverter.ToUInt64(Encoding.UTF8.GetBytes(data).Concat(new byte[empty_size]).ToArray(), 0);
        }
        /// <summary>
        /// Cast uint64 to char
        /// </summary>
        private static char MarshalChar(ulong value)
            => Encoding.UTF8.GetString(BitConverter.GetBytes(value)).First();
    }
}