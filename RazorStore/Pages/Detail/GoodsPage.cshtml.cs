using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly SignInManager<User> signInManager;

        public GoodsPageModel(AppDbContext db, IAuthorizationService authorizationService,
            ILogger<GoodsPageModel> logger, SignInManager<User> signInManager)
        {
            this.db = db;
            this.authorizationService = authorizationService;
            this.logger = logger;
            this.signInManager = signInManager;
        }
        [BindProperty]
        public Goods Goods { get; set; }
        public bool ShowButon { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            Goods = db.Goods.Find(id);
            if (signInManager.IsSignedIn(User))
            {
                logger.LogInformation("Searc Id{id}", id);
                var autharization = await authorizationService.AuthorizeAsync(User, Goods, "CanManageGoods");
                ShowButon = autharization.Succeeded;
            }

            return Page();
        }

        public void OnPost(Goods good)
        {

        }

    }
}
