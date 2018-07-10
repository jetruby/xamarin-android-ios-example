using System;
using Refit;
using System.Net.Http;

namespace Data.Api
{
    public class ApiFactory : IApiFactory
    {
        private readonly AppHttpClientHandler ClientHandler;

        public ApiFactory(AppHttpClientHandler clientHandler)
        {
            this.ClientHandler = clientHandler;

        }

        public IGithubApi Create() => RestService.For<IGithubApi>(
                new HttpClient(new AppHttpClientHandler())
                {
                    BaseAddress = new Uri("https://api.github.com")
                });
    }
}
