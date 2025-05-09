namespace EnkaSharp;

public class EnkaClientConfig
{
    /// <summary>
    /// UserAgent to use for API requests
    /// </summary>
    public string UserAgent { get; set; } = null!;
    /// <summary>
    /// Language to use for Localization purposes.
    /// Defaults to "en" - english.
    /// </summary>
    public string Language { get; set; } = "en";
}