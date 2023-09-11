using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorStore.Model;
using RazorStore.Services;
using System.Linq;

namespace RazorStore.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly AppDbContext db;
    private readonly ISearchAlgorithm<Goods> goods;

    public IndexModel(ILogger<IndexModel> logger, AppDbContext db, ISearchAlgorithm<Goods> goods)
    {
        _logger = logger;
        this.db = db;
        this.goods = goods;
    }
    public bool ShowButton { get; set; } = false;
    [BindProperty(SupportsGet = true)]
    public int QuantityGoods { get; set; } = 1;
    public IEnumerable<Goods> Goods { get; set; }
    public PagePagination<Goods> PagePagination { get; set; }

    public void OnGet(int quantity, int pageNumber = 1)
    {
        
        PagePagination = new PagePagination<Goods>(db.Goods, quantity, pageNumber);
    }
    public void OnPost()
    {

    }
    
}

