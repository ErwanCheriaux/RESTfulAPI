namespace MountainBike.Services.Settings;

public class ReactClientSettings
{
    public string? Host { get; set; }
    public int Port { get; set; }
    public string ConnectionString
    {
        get
        {
            return $"http://{Host}:{Port}";
        }
    }
}