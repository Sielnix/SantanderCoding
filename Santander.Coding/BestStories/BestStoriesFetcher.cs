namespace Santander.Coding.BestStories;

public interface IBestStoriesFetcher
{
    Task<BestStory[]> GetBestStories(int count, CancellationToken ct);
}

internal class BestStoriesFetcher : IBestStoriesFetcher
{
    private const string BestStoriesIdsUrl = "https://hacker-news.firebaseio.com/v0/beststories.json";

    private const int MinCount = 1;
    private const int MaxCount = 200;

    private readonly HttpClient _httpClient;

    public BestStoriesFetcher(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<BestStory[]> GetBestStories(int count, CancellationToken ct)
    {
        if (count is < MinCount or > MaxCount)
        {
            throw new ArgumentOutOfRangeException(nameof(count), $"Value must be within {MinCount} and {MaxCount}");
        }

        long[] bestStoriesIds = await GetBestStoriesIds(ct);
        int storiesToReturn = Math.Min(count, bestStoriesIds.Length);

        BestStory[] result = new BestStory[storiesToReturn];
        await Parallel.ForAsync(
            0,
            storiesToReturn,
            ct,
            async (i, cancellation) =>
            {
                HackerNewsItem hackerNewsItem = await GetHackerNewsItem(bestStoriesIds[i], cancellation);
                result[i] = MapToBestStory(hackerNewsItem);
            });

        return result;
    }

    private BestStory MapToBestStory(HackerNewsItem hackerNewsItem)
    {
        return new BestStory()
        {
            CommentCount = hackerNewsItem.Descendants,
            PostedBy = hackerNewsItem.By,
            Score = hackerNewsItem.Score,
            Time = DateTimeOffset.FromUnixTimeSeconds(hackerNewsItem.Time),
            Title = hackerNewsItem.Title,
            Uri = hackerNewsItem.Url
        };
    }

    private async Task<HackerNewsItem> GetHackerNewsItem(long itemId, CancellationToken ct)
    {
        string url = FormattableString.Invariant($"https://hacker-news.firebaseio.com/v0/item/{itemId}.json");
        return await GetFromJson<HackerNewsItem>(url, ct);
    }

    private async Task<long[]> GetBestStoriesIds(CancellationToken ct)
    {
        return await GetFromJson<long[]>(BestStoriesIdsUrl, ct);
    }

    private async Task<T> GetFromJson<T>(string url, CancellationToken ct)
        where T: notnull
    {
        T? fetchedObject;
        try
        {
            fetchedObject = await _httpClient.GetFromJsonAsync<T>(url, ct);
        }
        catch (Exception e) when (!ct.IsCancellationRequested)
        {
            throw new HackerNewsResponseException("Failed to fetch data", e);
        }

        if (fetchedObject is null)
        {
            throw new HackerNewsResponseException("Response is null");
        }

        return fetchedObject;
    }


}
