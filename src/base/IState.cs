namespace ancient.runtime.@base
{
    using emit.sys;
    using tools;

    public interface IState
    {
        void LoadMeta(byte[] meta);

        ulong? next(ulong pc_ref);
        ulong fetch();

        ushort AcceptOpCode(BitwiseContainer container);

        long SP { get; set; }
        ulong pc { get; set; }

        /// <summary>
        /// base register
        /// </summary>
        ushort r1 { get; set; }

        ushort r2 { get; set; }
        ushort r3 { get; set; }

        /// <summary>
        /// value register
        /// </summary>
        ushort u1 { get; set; }

        ushort u2 { get; set; }

        /// <summary>
        /// magic registers
        /// </summary>
        ushort x1 { get; set; }

        ushort x2 { get; set; }
        ushort x3 { get; set; }
        ushort x4 { get; set; }

        /// <summary>
        /// meta registers
        /// </summary>
        ushort o1 { get; set; }

        ushort o2 { get; set; }
        ushort o3 { get; set; }

        /// <summary>
        /// id
        /// </summary>
        ushort iid { get; set; }

        /// <summary>
        /// trace flag
        /// </summary>
        bool tc { get; set; }

        /// <summary>
        /// Error flag
        /// </summary>
        bool ec { get; set; }

        /// <summary>
        /// Keep memory flag
        /// </summary>
        bool km { get; set; }

        /// <summary>
        /// fast write flag
        /// </summary>
        bool fw { get; set; }

        /// <summary>
        /// overflow flag
        /// </summary>
        bool of { get; set; }

        /// <summary>
        /// negative flag
        /// </summary>
        bool nf { get; set; }

        /// <summary>
        /// break flag (for next execute)
        /// </summary>
        bool bf { get; set; }

        /// <summary>
        /// float flag
        /// </summary>
        bool ff { get; set; }

        /// <summary>
        /// stack forward flag
        /// </summary>
        bool sf { get; set; }

        /// <summary>
        /// control stack flag
        /// </summary>
        bool northFlag { get; set; }

        /// <summary>
        /// control stack flag
        /// </summary>
        bool eastFlag { get; set; }

        /// <summary>
        /// bios read-access
        /// </summary>
        bool southFlag { get; set; }

        /// <summary>
        /// Current Address
        /// </summary>
        ulong curAddr { get; set; }

        /// <summary>
        /// Last executed address
        /// </summary>
        ulong lastAddr { get; set; }

        /// <summary>
        /// CPU Steps
        /// </summary>
        ulong step { get; set; }

        /// <summary>
        /// Halt flag
        /// </summary>
        sbyte halt { get; set; }

        /// <summary>
        /// L1 Memory
        /// </summary>
        ulong[] mem { get; }
        /// <summary>
        /// L1 Types 
        /// </summary>
        ExternType[] mem_types { get; }
    }
}