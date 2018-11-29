using System;
using Xunit;
using System.Net.Http;
using System.Threading.Tasks;
using Shouldly;
using TwitterSentiment;
using System.Collections.Generic;
using Tests.Fakes;
using System.Net;
using Newtonsoft.Json;
using System.Text;
using NSubstitute;

namespace Tests
{
    public class TwitterClientShould
    {
        public TwitterClientShould()
        {
        }

        [Fact]
        public async Task Get_Tweets_For_Hastag_Using_TypedHttpClient()
        {
            var tweet1 = new Tweet("This is a really negative tweet");
            var tweet2 = new Tweet("This is a super positive great tweet");
            var tweet3 = new Tweet("This is another really super positive amazing tweet");

            var tweets = new List<Tweet> { tweet1, tweet2, tweet3  };

            var fakeHttpMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(tweets), Encoding.UTF8, "application/json")
            });

            var fakeHttpClient = new HttpClient(fakeHttpMessageHandler);

            var sut = new TwitterClient(fakeHttpClient);

            var result = await sut.GetTimeline("wholeschool");

            result.Count.ShouldBe(3);
        }
       
    }
}
