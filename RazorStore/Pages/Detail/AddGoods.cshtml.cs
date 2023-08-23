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

        public AddGoodsModel(AppDbContext db, IWebHostEnvironment host,
            UserManager<User> user, IAuthorizationService authorizationService)
        {
            this.db = db;
            this.host = host;
            this._user = user;
            this.authorizationService = authorizationService;
        }
        [BindProperty]
        public Goods Goods { get; set; }
        [BindProperty]
        public IFormFile? Photo { get; set; }
        public void OnGet()
        {
            Goods = new Goods();
        }
        public async void OnPost( )
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
                    var good = db.Goods.Find(Goods.Id);
                    db.Goods.Remove(good);
                    db.SaveChanges();
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
                        RedirectToPage("/Account/Login");
                    }
                    
                    db.Goods.Add(Goods);
                    db.SaveChanges();
                }
            }





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

