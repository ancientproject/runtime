namespace ancient.runtime.devices
{
    using MagicOnion;

    public interface IDeviceLayer : IService<IDeviceLayer>
    {
        UnaryResult<None> warmUp();
        UnaryResult<None> shutdown();
        UnaryResult<None> write(ulong address, ulong data);
        UnaryResult<ulong> read(ulong address);
    }

    public struct None
    {

    }
}