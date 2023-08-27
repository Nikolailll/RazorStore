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
    public class GoodsPageModel : PageModel
    {
        private readonly AppDbContext db;
        private readonly IAuthorizationService authorizationService;
        private readonly ILogger<GoodsPageModel> logger;

        public GoodsPageModel(AppDbContext db, IAuthorizationService authorizationService, ILogger<GoodsPageModel> logger)
        {
            this.db = db;
            this.authorizationService = authorizationService;
            this.logger = logger;
        }
        [BindProperty]
        public Goods Goods { get; set; }
        public bool ShowButon { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {

            Goods = db.Goods.Find(id);
            logger.LogInformation("Searc Id{id}", id);
            var autharization = await authorizationService.AuthorizeAsync(User, Goods, "CanManageGoods");
            ShowButon = autharization.Succeeded;
            return Page();
        }

        public void OnPost(Goods good)
        {

        }

    }
}
