using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Primitives;
using WatchPortalFunction.Functions;
using WatchPortalFunction.Repository;
using Xunit;

namespace WatchPortalFunctionTests
{
    public class WatchPortalFunctionUnitTests
    {
        [Fact]
        public void TestWatchFunctionSuccess()
        {
            var queryStringValue = "ccc";
            var request = new DefaultHttpRequest(new DefaultHttpContext())
            {
                Query = new QueryCollection
                (
                    new System.Collections.Generic.Dictionary<string, StringValues>()
                    {
                        { "model", queryStringValue }
                    }
                )
            };
            var logger = NullLoggerFactory.Instance.CreateLogger("Null Logger");
            WatchInfo watchInfo = new WatchInfo(new WatchRepository());
            var response = watchInfo.Run(request, logger);
            response.Wait();

            // Check that the response is an "OK" response
            Assert.IsAssignableFrom<OkObjectResult>(response.Result);

            // Check that the contents of the response are the expected contents
            var result = (OkObjectResult)response.Result;
            Assert.NotNull(result.Value);
        }

        [Fact]
        public void TestWatchFunctionNotFound()
        {
            var queryStringValue = "abc";
            var request = new DefaultHttpRequest(new DefaultHttpContext())
            {
                Query = new QueryCollection
                (
                    new System.Collections.Generic.Dictionary<string, StringValues>()
                    {
                        { "model", queryStringValue }
                    }
                )
            };
            var logger = NullLoggerFactory.Instance.CreateLogger("Null Logger");
            WatchInfo watchInfo = new WatchInfo(new WatchRepository());
            var response = watchInfo.Run(request, logger);
            response.Wait();

            // Check that the response is an "OK" response
            Assert.IsAssignableFrom<NotFoundObjectResult>(response.Result);

            // Check that the contents of the response are the expected contents
            var result = (NotFoundObjectResult)response.Result;
            Assert.Equal("This model doesn't exist", result.Value);
        }

        [Fact]
        public void TestWatchFunctionFailureNoQueryString()
        {
            var request = new DefaultHttpRequest(new DefaultHttpContext());
            var logger = NullLoggerFactory.Instance.CreateLogger("Null Logger");

            WatchInfo watchInfo = new WatchInfo(new WatchRepository());
            var response = watchInfo.Run(request, logger);
            response.Wait();

            // Check that the response is an "Bad" response
            Assert.IsAssignableFrom<BadRequestObjectResult>(response.Result);

            // Check that the contents of the response are the expected contents
            var result = (BadRequestObjectResult)response.Result;
            Assert.Equal("Please provide a watch model in the query string or in the request body", result.Value);
        }

        [Fact]
        public void TestWatchFunctionFailureNoModel()
        {
            var queryStringValue = "abc";
            var request = new DefaultHttpRequest(new DefaultHttpContext())
            {
                Query = new QueryCollection
                (
                    new System.Collections.Generic.Dictionary<string, StringValues>()
                    {
                        { "not-model", queryStringValue }
                    }
                )
            };

            var logger = NullLoggerFactory.Instance.CreateLogger("Null Logger");

            WatchInfo watchInfo = new WatchInfo(new WatchRepository());
            var response = watchInfo.Run(request, logger);
            response.Wait();

            // Check that the response is an "Bad" response
            Assert.IsAssignableFrom<BadRequestObjectResult>(response.Result);

            // Check that the contents of the response are the expected contents
            var result = (BadRequestObjectResult)response.Result;
            Assert.Equal("Please provide a watch model in the query string or in the request body", result.Value);
        }
    }
}
