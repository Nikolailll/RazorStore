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


    public IndexModel(ILogger<IndexModel> logger, AppDbContext db)
    {
        _logger = logger;
        this.db = db;
    }

    public IEnumerable<Goods> Goods { get; set; }
    public PagePagination<Goods> PagePagination { get; set; }

    public void OnGet(int pageNumber = 1)
    {
        var pageSize = 1;
        var countGoods = db.Goods.Count();
        var paginatio = new Pagination(pageSize, pageNumber, countGoods);
        var item = db.Goods.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        PagePagination = new PagePagination<Goods>(item, paginatio);
    }

    public void OnPost()
    {

    }

}

