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
	public class DeleteModel : PageModel
    {
        public Goods Goods { get; set; }
        private AppDbContext Db { get; }

        public DeleteModel(AppDbContext db)
        {
            Db = db;
        }
        public IActionResult OnGet(int? id)
        {
            
            Goods = Db.Goods.Find(id);
            if(Goods == null)
            {
                return RedirectToPage("/Error");
            }
            else
            {
                return Page();
            }

        }
        public IActionResult OnPost(int? id)
        {
            Goods = Db.Goods.Find(id);
            if (Goods == null)
            {
                return RedirectToPage("/index");
            }
            else
            {
                Db.Goods.Remove(Goods);
                Db.SaveChanges();
                return RedirectToPage("/index");
            }

        }
    }
}
