using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARINLAB.Services.Pages;
using AutoMapper;
using DAL.Data;
using DAL.Models.Dto.MenuModelDTO;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Microsoft.EntityFrameworkCore;


namespace ARINLAB.Services.Menu
{
    public class MenuService : IMenuService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IPagesService _pageService;
        private List<MenuDTO> _allMenuDTOs;

        public MenuService(ApplicationDbContext applicationDbContext, IMapper iMapper, IPagesService pagesService)
        {
            _dbContext = applicationDbContext;
            _mapper = iMapper;
            _pageService = pagesService;
            _allMenuDTOs = new List<MenuDTO>(GetAllMenus());
        }

      

        public IEnumerable<MenuDTO> GetAllMenus()
        {
            /*
            string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            var res = from d in _dbContext.Menus
            join c in _dbContext.MenuTranslates.Where(p => p.LanguageCulture == culture) on d.Id equals c.MenuId
            join s in _dbContext.MenuTranslates.Where(p => p.LanguageCulture == culture) on d.ParentId equals s.MenuId
            select new MenuDTO
            {
                Id = d.Id,
                Link = d.Link,
                Name = c.Name,
                Order = d.Order,
                IsPublish = d.IsPublish,
                ParentIdName = s.Name
              
            };
            
            return res;*/
           string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            var menus = _dbContext.Menus.Include(i => i.Pages);
            var result = _dbContext.MenuTranslates
                .Where(p => p.LanguageCulture == culture).Join(menus, p => p.MenuId, k => k.Id,
                    (p, k) => new MenuDTO
                    {
                        Id = k.Id,
                        Link = k.Link,
                        Name = p.Name,
                        Order = k.Order,
                        IsPublish = k.IsPublish,
                        ParentId = k.ParentId,
                        Pages = _mapper.Map<PagesDTO>(k.Pages),
                        ParentIdName = _dbContext.MenuTranslates.Where(p => p.LanguageCulture == culture).FirstOrDefault(p=>p.MenuId == k.ParentId).Name
                    }) ;
           
            
            return result;
       
        }

        public async Task CreateMenu(CreateMenuDTO modelDTO)
        {
            DAL.Models.Menu.Menu menu = _mapper.Map<DAL.Models.Menu.Menu>(modelDTO);
            await _dbContext.Menus.AddAsync(menu);
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditMenu(EditMenuDTO modelDTO)
        {
            DAL.Models.Menu.Menu menu = _mapper.Map<DAL.Models.Menu.Menu>(modelDTO);
            _dbContext.Menus.Update(menu);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveMenu(int id)
        {
            DAL.Models.Menu.Menu menu = await _dbContext.Menus.FindAsync(id);
            _dbContext.Menus.Remove(menu);
            await _dbContext.SaveChangesAsync();
        }

        public IEnumerable<MenuDTO> GetAllPublishMenus()
        {
            //string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

            //var menus = _dbContext.Menus
            //    .Include(i => i.ParentMenu)
            //    .ThenInclude(t => t.MenuTranslates).Where(k => k.IsPublish == true);
            //var result = _dbContext.MenuTranslates
            //    .Where(p => p.LanguageCulture == culture).Join(menus, p => p.MenuId, k => k.Id,
            //        (p, k) => new MenuDTO
            //        {
            //            Id = k.Id,
            //            Link = k.Link,
            //            Name = p.Name,
            //            Order = k.Order,
            //            ParentId = k.ParentId,
            //            ParentMenu = k.ParentMenu,
            //            ParentIdName = k.ParentMenu.MenuTranslates.FirstOrDefault(p => p.LanguageCulture == culture).Name,
            //            IsPublish = k.IsPublish
            //        });

            //return result;

            MenuDTO result = new MenuDTO();
            result.Childs = new List<MenuDTO>();

            var menu = _allMenuDTOs
                    .Where(p => p.ParentId == null && p.IsPublish == true).OrderBy(o => o.Order).ToList();

            foreach (var m in menu)
                result.Childs.Add(HierarchicalMenu(m, null));
            return result.Childs.OrderBy(o => o.Order);

        }

        private MenuDTO HierarchicalMenu(MenuDTO menuDto, int? idParent = null)
        {
            MenuDTO t = new MenuDTO();

            var menu = _allMenuDTOs
                .Where(p => p.ParentId == idParent && p.IsPublish == true).ToArray();
            menuDto.Childs = new List<MenuDTO>();
            menuDto.Childs.AddRange(menu);

            foreach (var m in menuDto.Childs)
            {
                t.Id = m.Id;
                t.Name = m.Name;
                t.Link = m.Link;
                t.Pages = m.Pages;
                t.Childs = new List<MenuDTO>();
                m.Childs = new List<MenuDTO>();

                HierarchicalMenu(m, m.Id);
            }

            return menuDto;
        }


        public async Task<MenuDTO> GetMenuByIdAsync(int menuId)
        {
            string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            var menu = await _dbContext.Menus.FindAsync(menuId);
            var translate = await _dbContext.MenuTranslates
                .Where(p => p.LanguageCulture == culture).SingleOrDefaultAsync(p => p.MenuId == menu.Id);
            MenuDTO result = new MenuDTO
            {
                Id = menu.Id,
                Name = translate.Name,
                Link = menu.Link,
                IsPublish = menu.IsPublish,
                Order = menu.Order,

            };
            return result;
        }

        public async Task<EditMenuDTO> GetMenuForEditById(int id)
        {
            var pr = await _dbContext.Menus
                .Include(i => i.MenuTranslates)
                .SingleOrDefaultAsync(k => k.Id == id);
            EditMenuDTO editMenuDTO = _mapper.Map<EditMenuDTO>(pr);
            return editMenuDTO;
        }

        public IEnumerable<MenuDTO> GetMenuWithOutPage()
        {
            string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            var menus = _dbContext.Menus.Where(c => !_dbContext.Pages.Where(p => p.MenuId != null).Select(b => b.MenuId)
            .Contains(c.Id));
            var result = _dbContext.MenuTranslates
                .Where(p => p.LanguageCulture == culture).Join(menus, p => p.MenuId, k => k.Id,
                    (p, k) => new MenuDTO
                    {
                        Id = k.Id,
                        Link = k.Link,
                        Name = p.Name,
                        Order = k.Order,
                        IsPublish = k.IsPublish
                    });

            return result;
        }

        public IEnumerable<MenuDTO> GetMenuWithOutPage(int id)
        {
            string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            var menus = _dbContext.Menus.Where(c => !_dbContext.Pages.Where(p => p.MenuId != null).Where(b => b.MenuId != id).Select(b => b.MenuId)
            .Contains(c.Id));
            var result = _dbContext.MenuTranslates
                .Where(p => p.LanguageCulture == culture).Join(menus, p => p.MenuId, k => k.Id,
                    (p, k) => new MenuDTO
                    {
                        Id = k.Id,
                        Link = k.Link,
                        Name = p.Name,
                        Order = k.Order,
                        IsPublish = k.IsPublish
                    });

            return result;
        }

        public IEnumerable<MenuDTO> GetAllMenuButThis(int id)
        {
            string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            var menus = _dbContext.Menus.Where(c => c.Id != id);
            var result = _dbContext.MenuTranslates
                .Where(p => p.LanguageCulture == culture).Join(menus, p => p.MenuId, k => k.Id,
                    (p, k) => new MenuDTO
                    {
                        Id = k.Id,
                        Link = k.Link,
                        Name = p.Name,
                        Order = k.Order,
                        IsPublish = k.IsPublish
                    });

            return result;
        }
    }
}
