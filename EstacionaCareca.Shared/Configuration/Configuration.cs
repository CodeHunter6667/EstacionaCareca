namespace EstacionaCareca.Shared.Configuration;

public static class Configuration
{
    public const string ApiBaseUrl = "https://api.platerecognizer.com/v1/plate-reader/";
    public const string ApiToken = "755cee8bf2a7bfbc7eabf01d036e8cd53fdf2e98";

    // RabbitMQ - ajuste conforme seu ambiente
    public const string RabbitMqHost = "localhost";
    public const int RabbitMqPort = 5672;
    public const string RabbitMqUser = "guest";
    public const string RabbitMqPassword = "guest";
    public const string RabbitMqQueue = "placas.queue";
}
