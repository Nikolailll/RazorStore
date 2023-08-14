using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorStore.Model;
using RazorStore.Services;

namespace RazorStore.Pages.Detail
{
	public class GoodsPageModel : PageModel
    {
        private readonly AppDbContext db;

        public GoodsPageModel(AppDbContext db)
        {
            this.db = db;
        }
        [BindProperty]
        public Goods Goods { get; set; }

        public void OnGet(int id)
        {
             Goods = db.Goods.Find(id);
        }

        public void OnPost(Goods good)
        {
            
        }

    }
}
