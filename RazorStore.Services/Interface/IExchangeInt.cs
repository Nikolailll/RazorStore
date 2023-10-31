using System;
namespace RazorStore.Services
{
	public interface IExchangeInt
	{
       Task<string> GetLatest(string id, string asset_id_quote, string time = null);

    }
}

