using System;
using System.Web;
using Moq;
using NUnit.Framework;
using TechTalk.SpecFlow;


namespace Redirect.tests
{
    [TestFixture]
    [Binding]
    public class RedirectTest
    {
        private string oldUrl;
        private string newUrl;
        private string requestedUrl;
        private string finalUrl;

        private RedirectHandler _handler;
        private Mock<HttpContextBase> mockContext;
        private Mock<HttpResponseBase> mockResponse;



        [Given(@"I have entered a request to (.*)")]
        public void GivenIHaveEnteredARequestToHttpWww_Frickinsweet_ComRyanlanciaux_ComPage2(string url)
        {
            var uri = new Uri(url);
            requestedUrl = url;

            mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(x => x.Request.Url).Returns(uri);

        }

       [Given(@"the old url is (.*)")]
        public void GivenTheOldUrlIsFrickinsweet_ComRyanlanciaux_Com(string url)
        {
           oldUrl = url;
        }

        [Given(@"my new url is (.*)")]
        public void GivenMyNewUrlIsRyanlanciaux_Com(string url)
        {
            newUrl = url;

            //now that we know both old and new url do a replace on httpcontexts' url
            //setup what we expect the called url to be and throw a callback on the mock so we can verify later
            mockResponse = new Mock<HttpResponseBase>();
            mockResponse.SetupProperty(x => x.Status);
            mockResponse.Setup(x => x.AddHeader("Location", requestedUrl.Replace(oldUrl, newUrl)))
                .Callback(() => finalUrl = requestedUrl.Replace(oldUrl, newUrl));               
            
            mockContext.Setup(x => x.Response).Returns(mockResponse.Object);
            
        }

        [Given(@"the old url scheme is not matched")]
        public void GivenTheOldUrlSchemeIsNotMatched()
        {
            
        }


        [When(@"the request is made")]
        public void WhenTheRequestIsMade()
        {
            _handler = new RedirectHandler();
            _handler.ProcessRequest(mockContext.Object, oldUrl, newUrl);
        }

        [Then(@"the response has a 301 in the status")]
        public void ThenTheResponseHasA301InTheStatus()
        {
            Assert.That(mockContext.Object.Response.Status == "301 Moved Permanently");            
        }

        [Then(@"the response url is (.*)")]
        public void ThenTheResponseUrlIsTheNewUrl(string expectedUrl)
        {
            Assert.AreEqual(expectedUrl, finalUrl);
        }

        [Then(@"301 is not in the headers")]
        public void Then_301IsNotInTheHeaders()
        {
            Assert.IsNull(mockResponse.Object.Status);
        }



    }
}
