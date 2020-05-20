namespace ancient.runtime
{
    using emit.sys;
    using emit.@unsafe;
    using @unsafe;

    public class call_i : Instruction
    {
        protected internal readonly int _sign;
        public call_i(int sign) : base(IID.call_i) => _sign = sign;
        public call_i(string sign) : base(IID.call_i) 
            => _sign = NativeString.GetHashCode(sign.Replace("()", ""));

        protected override void OnCompile() 
            => Construct(new d32i(_sign));
    }
    public class __static_extern_call : Instruction
    {
        protected internal readonly ushort _sign;
        public __static_extern_call(ushort sign) : base(IID.__static_extern_call) 
            => _sign = sign;
        public __static_extern_call(string sign) : base(IID.__static_extern_call) 
            => this._sign = Module.CompositeIndex(sign);

        protected override void OnCompile() 
            => Construct(new d16u(_sign));
    }
}