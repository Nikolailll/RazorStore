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
	public class AddGoodsModel : PageModel
    {
        private readonly AppDbContext db;
        private readonly IWebHostEnvironment host;
        private readonly UserManager<User> _user;
        private readonly IAuthorizationService authorizationService;
        private readonly ILogger<AddGoodsModel> logger;

        public AddGoodsModel(AppDbContext db, IWebHostEnvironment host,
            UserManager<User> user, IAuthorizationService authorizationService, ILogger<AddGoodsModel> logger)
        {
            this.db = db;
            this.host = host;
            this._user = user;
            this.authorizationService = authorizationService;
            this.logger = logger;
        }
        public bool ShowButon { get; set; } = false;
        [BindProperty]
        public Goods Goods { get; set; }
        [BindProperty]
        public IFormFile? Photo { get; set; }
        public void OnGet(int? id)
        {
            if (id > 0)
            {
                Goods = db.Goods.Find(id);
                ShowButon = id > 0;
            }
            else
            {
                Goods = new Goods();
            }
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                if (Photo != null)
                {
                    if (Goods.PicturePath != null)
                    {
                        var deletePath = Path.Combine(host.WebRootPath, "Image", Goods.PicturePath);
                        System.IO.File.Delete(deletePath);
                    }
                    Goods.PicturePath = UploadPhoto();

                }
                if(Goods.Id > 0)
                {
                    logger.LogInformation("Try to update user with id : {id}", Goods.Id);
                    var good = db.Goods.Attach(Goods);
                    good.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                    TempData["Message"] = $"Goods name - {Goods.Name} was update";
                    return RedirectToPage("/Index");

                }
                else
                {
                   
                    var users = await _user.GetUserAsync(User);
                    if(users != null)
                    {
                        Goods.User = users;
                    }
                    else
                    {
                        logger.LogWarning("User : {name} not found", User.Identity.Name);
                        return RedirectToPage("/Account/Login");
                    }
                    
                    db.Goods.Add(Goods);
                    db.SaveChanges();

                    TempData["Message"] = $"Goods name - {Goods.Name} was added";
                    return RedirectToPage("index");
                }
            }
            TempData["Message"] = $"Goods - {Goods.Name} wasn't added";
            return RedirectToPage("index");




        }
        private string UploadPhoto()
        {
            string uniqName = null;
            if (Photo != null)
            {
                var path = Path.Combine(host.WebRootPath, "images");
                uniqName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                var filePath = Path.Combine(path, uniqName);
                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fs);
                }

            }
            return uniqName;

        }
    }
}

