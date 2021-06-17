namespace Consul.HostService
{
    public interface IConsulBuilder
    {
        ConsulOption Options { get;}
        string GetContainerID();
        string GetCurrentHost();
    }
}
