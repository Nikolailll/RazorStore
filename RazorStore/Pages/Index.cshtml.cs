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

    public void OnGet()
    {
        Goods = db.Goods.Where(x => x.Delete == false).ToList();
        Goods = db.Goods.Where(x => x.Delete == false).ToList();

    }

}

