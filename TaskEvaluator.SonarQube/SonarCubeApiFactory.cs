using System.Net;
namespace TaskEvaluator.SonarQube;

public sealed class SonarCubeApiFactory(IHttpClientFactory httpClientFactory) {
    public async Task<SonarQubeApi> Create(string url, string username, string password) {
        var httpClient = httpClientFactory.CreateClient();
        httpClient.BaseAddress = new Uri(url);
        if (!await Login(httpClient, username, password)) throw new WebException("Failed to login");

        return new SonarQubeApi(httpClient);
    }

    private static async Task<bool> Login(HttpClient httpClient, string username, string password) {
        var response = await httpClient
            .PostAsync($"/api/authentication/login?login={username}&password={password}", new StringContent(""))
            .ConfigureAwait(false);

        // We need to get the XSRF-TOKEN from the cookie to be able to make authenticated requests
        if (response.Headers.TryGetValues("Set-Cookie", out var values)) {
            foreach (var cookie in values.ToList()) {
                foreach (var s in cookie.Split(";")) {
                    var strings = s.Split("=");
                    if (strings is ["XSRF-TOKEN", {} token, ..]) {
                        httpClient.DefaultRequestHeaders.Add("X-XSRF-TOKEN", token);
                    }
                }
            }
        } else {
            return false;
        }

        return response.IsSuccessStatusCode;
    }
}
