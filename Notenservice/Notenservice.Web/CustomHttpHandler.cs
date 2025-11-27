namespace Notenservice.Web
{
    public class CustomHttpHandler
    {
    }
}
namespace Web
{
    public class CustomHttpHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage>
            SendAsync(HttpRequestMessage request, CancellationToken
                cancellationToken)
        {
            HttpResponseMessage response = await base.SendAsync(request,
                cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Request failed with code: {response.StatusCode}");
            }
            return response;
        }
    }
}