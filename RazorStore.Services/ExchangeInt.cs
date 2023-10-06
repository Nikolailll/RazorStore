using System;
using Microsoft.Extensions.Logging;

namespace RazorStore.Services
{
	public class ExchangeInt : IExchangeInt
	{
        private readonly HttpClient client;
        private readonly ILogger<ExchangeInt> logger;

        public ExchangeInt(HttpClient client, ILogger<ExchangeInt> logger )
		{
            this.client = client;
            this.logger = logger;
        }
        public async Task<string> GetLatest(string asset_id_base, string asset_id_quote, string time = null)
        {
            try
            {
                var response = await client.GetAsync($"exchangerate/{asset_id_base}/{asset_id_quote}?time={time}");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();

            }
            catch(Exception ex)
            {
                logger.LogWarning("Failed response {ex}", ex.Message);
                return "Failed";
            }
        }
	}
}

    