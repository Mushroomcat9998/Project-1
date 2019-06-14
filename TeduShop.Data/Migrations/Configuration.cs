namespace TeduShop.Data.Migrations
{
    using Common;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Model.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Diagnostics;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TeduShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(TeduShopDbContext context)
        {
            CreateProductCategorySample(context);
            CreateSlide(context);
            //  This method will be called after migrating to the latest version.
            CreatePage(context);
            CreateContactDetail(context);

            CreateConfigTitle(context);
            CreateFooter(context);
            CreateUser(context);

        }
        private void CreateConfigTitle(TeduShopDbContext context)
        {
            if (!context.SystemConfigs.Any(x => x.Code == "HomeTitle"))
            {
                context.SystemConfigs.Add(new SystemConfig()
                {
                    Code = "HomeTitle",
                    ValueString = "Trang chủ DAV Shop",
                });
            }
            if (!context.SystemConfigs.Any(x => x.Code == "HomeMetaKeyword"))
            {
                context.SystemConfigs.Add(new SystemConfig()
                {
                    Code = "HomeMetaKeyword",
                    ValueString = "Trang chủ DAV Shop",
                });
            }
            if (!context.SystemConfigs.Any(x => x.Code == "HomeMetaDescription"))
            {
                context.SystemConfigs.Add(new SystemConfig()
                {
                    Code = "HomeMetaDescription",
                    ValueString = "Trang chủ DAV Shop",
                });
            }
        }
        private void CreateUser(TeduShopDbContext context)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new TeduShopDbContext()));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new TeduShopDbContext()));

            var user = new ApplicationUser()
            {
                UserName = "sami",
                Email = "boboiboyfc9998@gmail.com",
                EmailConfirmed = true,
                BirthDay = DateTime.Now,
                FullName = "Tran Tung Duong"

            };
            if (manager.Users.Count(x => x.UserName == "sami") == 0)
            {
                manager.Create(user, "123654$");

                if (!roleManager.Roles.Any())
                {
                    roleManager.Create(new IdentityRole { Name = "Admin" });
                    roleManager.Create(new IdentityRole { Name = "User" });
                }

                var adminUser = manager.FindByEmail("boboiboyfc9998@gmail.com");

                manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
            }

        }
        private void CreateProductCategorySample(TeduShopDbContext context)
        {
            if (context.ProductCategories.Count() == 0)
            {
                List<ProductCategory> listProductCategory = new List<ProductCategory>()
                {
                    new ProductCategory() { Name="Điện lạnh",Alias="dien-lanh",Status=true },
                    new ProductCategory() { Name="Viễn thông",Alias="vien-thong",Status=true },
                    new ProductCategory() { Name="Đồ gia dụng",Alias="do-gia-dung",Status=true },
                    new ProductCategory() { Name="Mỹ phẩm",Alias="my-pham",Status=true },
                    new ProductCategory() { Name="Thời trang",Alias="thoi-trang",Status=true }
                };
                context.ProductCategories.AddRange(listProductCategory);
                context.SaveChanges();
            }
        }
        private void CreateFooter(TeduShopDbContext context)
        {
            if (context.Footers.Count(x => x.ID == CommonConstants.DefaultFooterId) == 0)
            {
                string content = @"<div class=""footer-bottom-cate"">
					<h6>CATEGORIES</h6>
					<ul>
						<li><a href = ""#"" ></ a ></ li >

                        <li><a href = ""#"" >Điện lạnh</a></li>
						<li><a href = ""#"" > Viễn thông</a></li>
						<li><a href = ""#"" > Đồ gia dụng</a></li>
						<li><a href = ""#"" > Mỹ phẩm</a></li>
						<li><a href = ""#"" > Thời trang</a></li>
					</ul>
				</div>
				<div class=""footer-bottom-cate"">
					<h6>TOP BRANDS</h6>
					<ul>
						<li><a href = ""#"" > Toshiba </ a ></ li >
                        <li>< a href= ""#"" > Shiseido </ a ></ li >
                        <li>< a href= ""#"" > Botani </ a ></ li >
                        <li>< a href= ""#"" > Adidas </ a ></ li >
                        <li>< a href= ""#"" > FPTShop </ a ></ li >
                        <li>< a href= ""#"" > VNPT </ a ></ li >
                        <li>< a href= ""#"" > Bitis </ a ></ li >
                        <li>< a href= ""#"" > Panasonic </ a ></ li >
                        <li>< a href= ""#"" > Samsung </ a ></ li >
                        <li>< a href= ""#"" > LG </ a ></ li >
                    </ ul >
                </ div >
                < div class=""footer-bottom-cate cate-bottom"">
					<h6>ĐỊA CHỈ</h6>
					<ul>
						<li>Cửa hàng: Số 17, Đ.Giải Phóng, Q.Hai Bà Trưng, TP.Hà Nội</li>
						<li>Văn Phòng: 18, ngõ 51, Đ.Tương Mai, Q.Hoàng Mai, TP.Hà Nội</li>
						<li class=""phone"">SĐT : 0987636280</li>
						<li class=""temp""><p>© 2019 DAVShop.Liên hệ số điện thoại của chúng tôi để được hỗ trợ tốt nhất.</p></li>
					</ul>
				</div>
				<div class=""clearfix""> </div>";
                context.Footers.Add(new Footer()
                {
                    ID = CommonConstants.DefaultFooterId,
                    Content = content
                });
                context.SaveChanges();
            }
        }

        private void CreateSlide(TeduShopDbContext context)
        {
            if (context.Slides.Count() == 0)
            {
                List<Slide> listSlide = new List<Slide>()
                {
                    new Slide() {
                        Name ="Slide 1",
                        DisplayOrder = 1,
                        Status = true,
                        Url ="#",
                        Image ="/Assets/client/images/bag.jpg",
                        Content =@"<h2>FLAT 70% 0FF</h2>
                                   <label>FOR ALL PURCHASE<b>VALUE</b></label>
                                   <p>Backpack with shocking discount</p><br>
                                   <span class=""on-get"">GET NOW</span>" },
                    new Slide() {
                        Name ="Slide 2",
                        DisplayOrder = 2,
                        Status = true,
                        Url ="#",
                        Image ="/Assets/client/images/bag1.jpg",
                        Content=@"<h2>FLAT 70% 0FF</h2>
                                <label>FOR ALL PURCHASE <b>VALUE</b></label>
                                <p>Fashion bag with shocking discount</p><br>
                                <span class=""on-get"">GET NOW</span>" },
                };
                context.Slides.AddRange(listSlide);
                context.SaveChanges();
            }
        }

        private void CreatePage(TeduShopDbContext context)
        {
            if (context.Pages.Count() == 0)
            {
                try
                {
                    var page = new Page()
                    {
                        Name = "Giới thiệu",
                        Alias = "gioi-thieu",
                        Content = @"Trang mua bán những sản phẩm tốt nhất",
                        Status = true
                    };
                    context.Pages.Add(page);
                    context.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var eve in ex.EntityValidationErrors)
                    {
                        Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                        }
                    }
                }
            }
        }

        private void CreateContactDetail(TeduShopDbContext context)
        {
            if (context.ContactDetails.Count() == 0)
            {
                try
                {
                    var contactDetail = new Model.Models.ContactDetail()
                    {
                        Name = "DAV Shop",
                        Address = "Số 17, Giải Phóng, Hai Bà Trưng, HN",
                        Email = "davshop@gmail.com",
                        Lat = 21.0633645,
                        Lng = 105.8053274,
                        Phone = "0987636280",
                        Website = "http://tedu.com.vn",
                        Other = "",
                        Status = true
                    };
                    context.ContactDetails.Add(contactDetail);
                    context.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var eve in ex.EntityValidationErrors)
                    {
                        Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                        }
                    }
                }
            }
        }
    }
}
