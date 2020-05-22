namespace ancient.runtime.@base
{
    using runtime;

    public interface IBus
    {
        void Add(IDevice device);
        IDevice find(int address);

        IState GetState();
    }
}