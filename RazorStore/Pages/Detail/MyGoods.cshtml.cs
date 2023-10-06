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
	public class MyGoodsModel : PageModel
    {
        private readonly AppDbContext db;
        private readonly ILogger<MyGoodsModel> logger;
        private readonly IAuthorizationService authorizationService;

        public bool ShowButton { get; set; }
        public IEnumerable<Goods> goods { get; set; }
        public MyGoodsModel(AppDbContext db, ILogger<MyGoodsModel> logger, IAuthorizationService authorizationService)
        {
            this.db = db;
            this.logger = logger;
            this.authorizationService = authorizationService;
        }
        public async Task<IActionResult> OnGet(string id)
        {
            logger.LogInformation("Goods {id}, try to access", id);
            goods = db.Goods.Where(x => x.User.Id == id);
            if (goods.Count() == 0)
            {
                return Page();
            }

            var go = goods.First();
            var authorize = await authorizationService.AuthorizeAsync(User,go, "CanManageGoods");

            if (authorize.Succeeded)
            {
                ShowButton = true;
                return Page();
            }
            return RedirectToPage("/Account/Login");
        }
    }
}
