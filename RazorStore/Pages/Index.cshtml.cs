using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using RazorStore.Model;
using RazorStore.Services;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RazorStore.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly AppDbContext db;
    private readonly ISearchAlgorithm<Goods> goods;
    private readonly IExchangeInt exchangeInt;

    public IndexModel(ILogger<IndexModel> logger, AppDbContext db, ISearchAlgorithm<Goods> goods, IExchangeInt exchangeInt)
    {
        _logger = logger;
        this.db = db;
        this.goods = goods;
        this.exchangeInt = exchangeInt;
    }
    

    public string Exchange { get; set; }

    public IEnumerable<Goods> Goods { get; set; }
    public PagePagination<Goods> PagePagination { get; set; }

    public async Task<IActionResult> OnGet(int quantity = 1, int pageNumber = 1)
    {
        var ss = db.Goods.Include(x => x.MultiplePath);
        //var response = await exchangeInt.GetLatest("BTC", "USD");
        //if(!(response == "Failed"))
        //{
        //    var ss = JsonConvert.DeserializeObject<Exchange>(response);
        //    Exchange = ss.Rate;
        //}
        PagePagination = new PagePagination<Goods>(ss, quantity, pageNumber);
        return Page();
    }
    public void OnPost(int quantity, int pageNumber)
    {
        var ss = db.Goods.Include(x => x.MultiplePath);
       PagePagination = new PagePagination<Goods>(ss, quantity, pageNumber);


    }

}

