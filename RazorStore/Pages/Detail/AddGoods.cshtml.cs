﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public AddGoodsModel(AppDbContext db, IWebHostEnvironment host)
        {
            this.db = db;
            this.host = host;
        }
        [BindProperty]
        public Goods Goods { get; set; }
        [BindProperty]
        public IFormFile? Photo { get; set; }
        public void OnGet()
        {
            Goods = new Goods();
        }
        public void OnPost()
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
                db.Goods.Add(Goods);
                db.SaveChanges();
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
