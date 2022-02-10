
using DAL.Models;
using DAL.Models.Configs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Data
{
    public static class Roles
    {
        public const string Admin = "Admin";
        public const string Trusted = "Trusted";
        public const string Registered = "Registered";
    }
    public static class CreateAdminData
    {
        
        public async static Task CreateDataTask(IHost host)
        {
            
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var _dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                await _dbContext.Database.MigrateAsync();
                var context = services.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await context.Roles.AnyAsync())
                {
                    await context.CreateAsync(new IdentityRole
                    {
                        Name = Roles.Admin
                    });
                    await context.CreateAsync(new IdentityRole
                    {
                        Name = Roles.Trusted
                    });
                    await context.CreateAsync(new IdentityRole
                    {
                        Name = Roles.Registered
                    });                   
                }

                var userContext = services.GetRequiredService<UserManager<ApplicationUser>>();
               
                var res = new List<Dictionary>(_dbContext.Dictionaries);
                int dictId = 0;
                if(res != null && res.Count() > 0)
                {
                    var tm = res.Where(p => p.Language.Contains("Türkmen"));
                    if(tm == null || tm.Count() < 1)
                    {
                        dictId = _dbContext.Dictionaries.Add(
                            new Dictionary
                            {
                                Language = "Türkmen",
                            }).Entity.Id;
                    }
                    else
                    {
                        dictId = new List<Dictionary>(tm)[0].Id;
                    }
                }
                var wordClauseCategory = new List<WordClauseCategory>(_dbContext.WordClauseCategories.AsNoTracking());
                if(wordClauseCategory.Count > 0)
                {
                    var item = wordClauseCategory.Where(p => p.Id == 1);
                    if(item != null && item.Count() > 0)
                    {
                    }
                    else
                    {
                        _dbContext.WordClauseCategories.Add(
                            new WordClauseCategory
                            {
                                Id = 0,
                                WordClauseCategoryTranslates = new List<WordClauseCategoryTranslate>()
                                {
                                  new WordClauseCategoryTranslate(){ Id=1,  CategoryName = "جذر", LanguageCulture="ar", WordClauseCategoryId = 0 },
                                  new WordClauseCategoryTranslate(){ Id=1,  CategoryName = "Root", LanguageCulture="en", WordClauseCategoryId = 0 },
                                  new WordClauseCategoryTranslate(){ Id=1,  CategoryName = "Kök", LanguageCulture="tk", WordClauseCategoryId = 0}
                                }                       
                            });  
                    }
                }
                else
                {
                    _dbContext.WordClauseCategories.Add(
                             new WordClauseCategory
                             {                                
                                 WordClauseCategoryTranslates = new List<WordClauseCategoryTranslate>()
                                 {
                                  new WordClauseCategoryTranslate(){ CategoryName = "جذر", LanguageCulture="ar", WordClauseCategoryId = 0 },
                                  new WordClauseCategoryTranslate(){ CategoryName = "Root", LanguageCulture="en", WordClauseCategoryId = 0 },
                                  new WordClauseCategoryTranslate(){ Id=1,  CategoryName = "Kök", LanguageCulture="tk", WordClauseCategoryId = 0}

                                 }
                             });
                }
                _dbContext.SaveChanges();

                Country country = null;
                country = await _dbContext.Countries.FirstOrDefaultAsync(p => p.Name.Contains("Türkmenistan"));
                if (country == null)
                {
                   country = _dbContext.Countries.Add(
                        new Country
                        {
                            Name = "Türkmenistan"
                        }
                    ).Entity;
                }
                await _dbContext.SaveChangesAsync();
                var admin = await userContext.FindByNameAsync("Admin");

                if (admin == null)
                {
                    ApplicationUser adminUser = new ApplicationUser
                    {
                        FirstName = "Admin",
                        FamilyName = "Admin",
                        Email = $"allakyyev@gmail.com",
                        UserName = "Admin",
                        IsApproved = true,
                        EmailConfirmed = true,
                        CountryId = country.Id
                    };

                    await userContext.CreateAsync(adminUser, "Password!1");
                    await userContext.AddToRoleAsync(adminUser, Roles.Admin);
                }

                List<Language> languages = new List<Language>();
                languages.Add(new Language() { Culture = "en", Name = "English", DisplayOrder = 0, IsPublish = true });
                languages.Add(new Language() { Culture = "ar", Name = "Arabic", DisplayOrder = 1, IsPublish = true });
                languages.Add(new Language() { Culture = "tk", Name = "Türkmen", DisplayOrder = 1, IsPublish = true });

                _dbContext.Languages.RemoveRange(_dbContext.Languages);
                _dbContext.SaveChanges();
                foreach (var lng in languages)
                {
                    var language = await _dbContext.Languages.SingleOrDefaultAsync(s => s.Culture == lng.Culture);
                    if (language == null)
                    {
                        _dbContext.Languages.Add(lng);
                        await _dbContext.SaveChangesAsync();
                    }
                }

                var lg = new List<Logo>(_dbContext.Logos);
                if(lg.Count == 0)
                {
                    Logo l1 = new Logo() { Name = "Main Logo", Image = "images/logo_arinlang_1.png", Link="http://arinlang.com" };
                    Logo l2 = new Logo() { Name = "Secondary Logo", Image = "client/assets/AR_logo.png", Link = "" };
                    _dbContext.Logos.Add(l1);
                    _dbContext.Logos.Add(l2);
                    await _dbContext.SaveChangesAsync();
                }

                List<Settings> settings = new List<Settings>();

                settings.Add(new Settings() { Name = "userName", Value = "ARINLANG" });
                settings.Add(new Settings() { Name = "password", Value = "tstbAPI@2020" });

                settings.Add(new Settings() { Name = "language", Value = "ru" });


                settings.Add(new Settings() { Name = "AdminEmail", Value = "arinlang@outlook.com" });
                settings.Add(new Settings() { Name = "AdminEmailPassword", Value = "YhussY2022" }); 

                settings.Add(new Settings() { Name = "Phone", Value = " " });
                settings.Add(new Settings() { Name = "Address", Value = " " });
                settings.Add(new Settings() { Name = "Email", Value = "info@arinlang.com" });
               
                settings.Add(new Settings() { Name = "UnsubLink", Value = "https://localhost:5001/Home/UnsubLink" });
                settings.Add(new Settings() { Name = "UnsubscribeLink", Value = "https://localhost:5001/Home/Unsubscribe" });


                _dbContext.Settings.RemoveRange(_dbContext.Settings);
                _dbContext.SaveChanges();

                foreach (var setting in settings)
                {
                    var stng = await _dbContext.Settings.SingleOrDefaultAsync(s => s.Name == setting.Name);
                    if(stng == null)
                    {
                        _dbContext.Settings.Add(setting);
                        await _dbContext.SaveChangesAsync();
                    }
                }
            }
           
        }
    }
}
