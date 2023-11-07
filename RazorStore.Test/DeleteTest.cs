using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RazorStore.Model;
using Moq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RazorStore.Services;
using RazorStore.Pages.Detail;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace RazorStore.Test
{
    public class DeleteTest
    {
        [Fact]
        public async Task OnPost_DeletesItem_WhenAuthorized()
        {
            // Arrange
            var id = 1; // Здесь укажите действительный id товара
            var goods = new Goods { Id = 1, Name = "Stas", Price = "123", Subscribe = "Good", Location = "Bar" };
            var authorizationServiceMock = new Mock<IAuthorizationService>();


            var logger = new Mock<ILogger<DeleteModel>>();
            var good = new Mock<Goods>();

            authorizationServiceMock.Setup(service => service.AuthorizeAsync(It.IsAny<ClaimsPrincipal>(),
                   good, "CanManageGoods")).ReturnsAsync(AuthorizationResult.Success());

            var httpContext = new DefaultHttpContext();
            httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                   new Claim(ClaimTypes.Name, "TestUser"),
                                    new Claim("Id","1")},"mock"));

            var connection = new SqliteConnection("DataSource=:memory");
         

            var option = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(connection)
                .Options;

            using (var context = new AppDbContext(option))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.AddRange(
                    new Goods { Id = 1, Name = "Stasik", Price = "123", Subscribe = "Good", Location = "Bar", Delete = false, User = new User { Id = "1"} },
                    new Goods { Id = 2, Name = "Volodya", Price = "1234", Subscribe = "Bad", Location = "Budva" },
                    new Goods { Id = 3, Name = "Ignat", Price = "1235", Subscribe = "Ugly", Location = "Kotor" }
                    );
                context.SaveChanges();

            }
            using (var context = new AppDbContext(option))
            {
                // Может потребоваться настроить результат авторизации
                


                var pageModel = new DeleteModel(context, authorizationServiceMock.Object, logger.Object)
                {
                    
                    PageContext = {HttpContext = httpContext}
                };

                
                var result = await pageModel.OnPost(id);



                //Assert.True(goods.Delete);
                Assert.Equal($"YourGoodsName was delete", pageModel.TempData["Message"]);
                Assert.Equal("/index", ((RedirectToPageResult)result).PageName); // Проверка переадресации 
            }








        }
    }
}

