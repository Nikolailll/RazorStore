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
	public class SearchPageModel : PageModel
    {
        private readonly AppDbContext appDbContext;
        private readonly ILogger<SearchPageModel> logger;

        public SearchPageModel(AppDbContext appDbContext, ILogger<SearchPageModel> logger)
        {
            this.appDbContext = appDbContext;
            this.logger = logger;
        }
        public IEnumerable<Goods> Goods { get; set; }
        public void OnGet(string search)
        {
            
            logger.LogInformation("Search param {search}", search);
            Goods = appDbContext.Goods.Where(x => x.Name == search);
            

        }
    }
}
