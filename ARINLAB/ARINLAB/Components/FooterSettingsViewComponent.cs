using ARINLAB.Services.Settings;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TSTB.Web.Components
{
    public class FooterSettingsViewComponent : ViewComponent
    {
        private readonly ISettingsService _settingsService;

        public FooterSettingsViewComponent(ISettingsService settingsService)
        {
            _settingsService = settingsService;
          
        }

        public IViewComponentResult Invoke()
        {
            string[] settingsString = new string[] { "Phone", "Address", "Email" };

            List<Settings> settings = new List<Settings>();

            var getSettings = _settingsService.GetAllSettings();
           

            foreach (var name in settingsString)
            {
                if (name == "Address" )
                {
                    settings.Add(new Settings()
                    {
                        Id = 0,
                        Name = name,
                      
                    });
                }
                else
                {
                    settings.Add(getSettings.SingleOrDefault(s => s.Name == name));
                }
            }

            return View(settings);
        }
    }
}
