  using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
  using Azure;
  using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
  using Microsoft.EntityFrameworkCore;
  using RazorStore.Model;
using RazorStore.Services;

namespace RazorStore.Pages.Detail
{
	public class SearchPageModel : PageModel
    {
        private readonly AppDbContext appDbContext;
        private readonly ILogger<SearchPageModel> logger;
        private readonly ISearchAlgorithm<Goods> searchAlgorithm;

        public SearchPageModel(AppDbContext appDbContext, ILogger<SearchPageModel> logger, ISearchAlgorithm<Goods> searchAlgorithm)
        {
            this.appDbContext = appDbContext;
            this.logger = logger;
            this.searchAlgorithm = searchAlgorithm;
        }
        public IEnumerable<Goods> Goods { get; set; }
        public IActionResult OnGet(string search)
        {
            if(search == null)
            {
                Goods = appDbContext.Goods.Take(10).Include(x => x.MultiplePath);
                return Page();
            }
            
            logger.LogInformation("Search param {search}", search);
            Goods = appDbContext.Goods.Include(x => x.MultiplePath);
            Goods = searchAlgorithm.Search(search, Goods);
            return Page();
            

        }
        public IActionResult OnPost(int searchType)
        {
            Goods = appDbContext.Goods.Include(x=>x.MultiplePath).Where(x => (int)x.Type == searchType);
            return Page();
        }
    }
}
