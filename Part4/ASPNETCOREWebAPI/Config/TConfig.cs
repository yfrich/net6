namespace ASPNETCOREWebAPI.Config;

public class TConfig
{
    public string? connStr { get; set; }
    public Logging? Logging { get; set; }
}
public class Logging
{
    public LogLevel? LogLevel { get; set; }
}
public class LogLevel
{
    public string? Default { get; set; }
}

//public record Config(string connStr, Logging Logging);
//public record Logging(LogLevel LogLevel);
//public record LogLevel(string Default);
