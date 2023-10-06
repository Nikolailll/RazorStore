using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorStore.Model;
using RazorStore.Services;

namespace RazorStore.Pages.Detail
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly IAuthorizationService authorizationService;
        private readonly ILogger<DeleteModel> logger;

        public Goods Goods { get; set; }
        private AppDbContext Db { get; }

        public DeleteModel(AppDbContext db, IAuthorizationService authorizationService, ILogger<DeleteModel> logger)
        {
            Db = db;
            this.authorizationService = authorizationService;
            this.logger = logger;
        }
        public async Task<IActionResult> OnGet(int? id)
        {

            Goods = Db.Goods.Find(id);

            if (Goods == null)
            {
                logger.LogWarning("Goods Id : {id} dosen't exist", id);
                return RedirectToPage("/Error");
            }
            else
            {
                return Page();
            }

        }
        public async Task<IActionResult> OnPost(int id)
        {
            Goods = Db.Goods.Find(id);
           
            if (Goods == null)
            {
                
                return RedirectToPage("/index");
            }

            var autharization = await authorizationService.AuthorizeAsync(User, Goods, "CanManageGoods");
            if (autharization.Succeeded)
            {
                Db.Goods.Remove(Goods);
                Db.SaveChanges();

            }
            return RedirectToPage("/index");

        }
    }
}
