namespace ancient.runtime.emit.sys
{
    using System.Collections.Generic;
    using System.Linq;
    using @base;
    using tools;

    public class CallFrame
    {
        private readonly Function _method;
        private readonly DebugSymbols _symbols;
        private readonly ulong _executeOffset;
        private readonly CallStack _stack;


        public Function GetMethod() => _method;
        public ulong GetExecuteMemoryOffset() => _executeOffset;
        public int GetDepth() => _stack.frames.Count;
        public CallFrame Caller => _stack.frames.Peek();

        public bool IsRoot() => _method is null;


        public CallFrame(Function method, DebugSymbols symbols, ulong executeOffset, CallStack stack)
        {
            _method = method;
            _symbols = symbols;
            _executeOffset = executeOffset;
            _stack = stack;
        }

        public override string ToString()
        {
            if (IsRoot())
                return $"<root>";
            return $"<{_method.Name}>({_executeOffset:X})";
        }
    }

    public class CallStack
    {
        internal Stack<CallFrame> frames { get; set; } = new Stack<CallFrame>();

        private readonly DebugSymbols _symbols;

        public CallStack(DebugSymbols symbols) => _symbols = symbols;

        public void Enter(Function method, ulong offset) 
            => frames.Push(new CallFrame(method, _symbols, offset, this));

        public void Exit() => frames.Pop();

        public List<CallFrame> GetFrames() => frames.ToList();
    }
}