namespace ancient.runtime.@base
{
    using System.Collections.Generic;
    using System.Linq;
    using MoreLinq;

    public class ControlRegister
    {
        private readonly IState _state;
        private readonly Stack<ulong> storage = new Stack<ulong>();

        public ControlRegister(IState state) 
            => _state = state;

        public bool IsRoot() => !storage.Any();

        public void Branch(ulong target) 
            => storage.Push(target);

        public ulong Recoil() 
            => IsRoot() ? _state.pc : storage.Pop();

        public ulong Recoil(int depth)
        {
            if (IsRoot() || GetDepth() <= depth)
            {
                storage.Clear();
                return _state.pc;
            }
            (..depth).ToEnumerable().Pipe(_ => storage.Pop()).Consume();
            return storage.Pop();
        }

        public int GetDepth() => storage.Count;
    }
}