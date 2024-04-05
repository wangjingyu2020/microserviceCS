namespace MainService.API.Consul
{
    public class ConsulOptions
    {
        public string? IP { get; set; }
        public string? Port { get; set; }
        public string? ServiceName { get; set; }
        public string? ConsulHost { get; set; }
        public string? ConsulDataCenter { get; set; }
    }
}
