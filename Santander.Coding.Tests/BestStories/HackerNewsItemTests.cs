using System.Text.Json;
using Santander.Coding.BestStories;

namespace Santander.Coding.Tests.BestStories;

public class HackerNewsItemTests
{
    private const string Json =
        @"{""by"":""ismaildonmez"",""descendants"":588,""id"":21233041,""kids"":[21233229,21233577,21235077,21233633,21233159,21233523,21233181,21233289,21233377,21233166,21233206,21233208,21233255,21234044,21233896,21233310,21233141,21233760,21233999,21235018,21233232,21233654,21233381,21233122,21233374,21237776,21233580,21234174,21233332,21235834,21235067,21233542,21235125,21233213,21238388,21234665,21233727,21235822,21240691,21233734,21233138,21237367,21234121,21234429,21234089,21233271,21234699,21234294,21233302,21234992,21234519,21233329,21234722,21234730,21234796,21233371,21235229,21235101,21233351,21235325,21234625,21238572,21234957,21233509,21234596,21233359,21233909,21237619,21233635,21235112,21251217,21233357,21235371,21233939,21233484,21233578],""score"":1757,""time"":1570887781,""title"":""A uBlock Origin update was rejected from the Chrome Web Store"",""type"":""story"",""url"":""https://github.com/uBlockOrigin/uBlock-issues/issues/745""}";

    [Fact]
    public void Deserializes_from_json()
    {
        HackerNewsItem? deserialized = JsonSerializer.Deserialize<HackerNewsItem>(Json, new JsonSerializerOptions(JsonSerializerDefaults.Web));

        Assert.NotNull(deserialized);
        Assert.Equal("ismaildonmez", deserialized.By);
        Assert.Equal("A uBlock Origin update was rejected from the Chrome Web Store", deserialized.Title);
        Assert.Equal("https://github.com/uBlockOrigin/uBlock-issues/issues/745", deserialized.Url);
        Assert.Equal(1757, deserialized.Score);
        Assert.Equal(588u, deserialized.Descendants);
    }
}
