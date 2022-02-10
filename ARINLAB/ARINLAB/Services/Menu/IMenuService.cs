using DAL.Models.Dto.MenuModelDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace ARINLAB.Services.Menu
{
    public interface IMenuService
    {
        IEnumerable<MenuDTO> GetAllMenus();
        IEnumerable<MenuDTO> GetAllPublishMenus();

        Task<MenuDTO> GetMenuByIdAsync(int id);
        Task CreateMenu(CreateMenuDTO modelDTO);

        Task EditMenu(EditMenuDTO modelDTO);

        Task RemoveMenu(int id);
        public Task<EditMenuDTO> GetMenuForEditById(int id);
        IEnumerable<MenuDTO> GetMenuWithOutPage();
        public IEnumerable<MenuDTO> GetMenuWithOutPage(int id);
        public IEnumerable<MenuDTO> GetAllMenuButThis(int id);
        
    }
}
