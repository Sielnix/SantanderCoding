namespace Santander.Coding.BestStories;

internal class HackerNewsItem
{
    public required string By { get; init; }
    public required uint Descendants { get; init; }
    public required long Score { get; init; }
    public required long Time { get; init; }
    public required string Title { get; init; }
    public string? Url { get; init; }
}
