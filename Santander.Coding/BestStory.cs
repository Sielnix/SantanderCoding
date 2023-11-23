namespace Santander.Coding;

public class BestStory
{
    public required string Title { get; init; }
    public string? Uri { get; init; }
    public required string PostedBy { get; init; }
    public required DateTimeOffset Time { get; init; }
    public required long Score { get; init; }
    public required uint CommentCount { get; init; }
}
