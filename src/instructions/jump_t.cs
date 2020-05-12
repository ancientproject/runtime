namespace ancient.runtime
{
    using System;
    using emit.@unsafe;
    [Obsolete]
    public class jump_t : jump
    {
        public jump_t(byte cell) : base(cell, 0x0, 0x0, 0x0, IID.jump_t)
        {
        }
    }
    [Obsolete]
    public class jump_e : jump
    {
        public jump_e(byte cell, byte f_cell, byte t_cell) : base(cell, f_cell, t_cell, 0x1, IID.jump_e)
        {
        }
    }
    [Obsolete]
    public class jump_g : jump
    {
        public jump_g(byte cell, byte f_cell, byte t_cell) : base(cell, f_cell, t_cell, 0x2, IID.jump_g)
        {
        }
    }
    [Obsolete]
    public class jump_u : jump
    {
        public jump_u(byte cell, byte f_cell, byte t_cell) : base(cell, f_cell, t_cell, 0x3, IID.jump_u)
        {
        }
    }
    [Obsolete]
    public class jump_y : jump
    {
        public jump_y(byte cell, byte f_cell, byte t_cell) : base(cell, f_cell, t_cell, 0x4, IID.jump_y)
        {
        }
    }
    public class jump_p : Instruction
    {
        private readonly byte _point;

        public jump_p(byte point) : base(IID.jump_p) => _point = point;

        protected override void OnCompile()
        {
            var (n1, n2) = new d8u(_point);
            Construct(n1, n2, x3: 0x1);
        }
    }

    public class jump_x : Instruction
    {
        private readonly byte _cell;
        private readonly byte _ref;

        public jump_x(byte cell, byte @ref)
        {
            _cell = cell;
            _ref = @ref;
        }
        protected override void OnCompile()
        {
            var (n1, n2) = new d8u(_cell);
            var (n3, u1) = new d8u(_ref);
            Construct(n1, n2, n3, u1, x3: 0x2);
        }

    }

    public abstract class jump : Instruction
    {
        internal readonly byte _cell;
        internal readonly byte _r2;
        internal readonly byte _r3;
        internal readonly byte _x1;

        protected jump(byte cell, byte r2, byte r3, byte x1, IID id) : base(id)
        {
            _cell = cell;
            _r2 = r2;
            _r3 = r3;
            _x1 = x1;
        }

        protected override void OnCompile()
            => Construct(_cell, r2: _r2, r3: _r3, u2: 0xF, x1: _x1);
    }
}