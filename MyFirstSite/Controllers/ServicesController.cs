using Microsoft.AspNetCore.Mvc;
using MyFirstSite.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstSite.Controllers
{
    public class ServicesController : Controller
    {
        [BindProperty]
        public DateTime DateTime { get; set; }

        private readonly DataManager dataManager;

        public ServicesController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }
        public IActionResult Index(Guid id)
        {
            if (id != default)
            {
                    return View("Show", dataManager.ServiceItems.GetServiceItemById(id));
            }
            ViewBag.TextField = dataManager.TextFields.GetTextFieldByCodeWord("PageServices"); 
            return View(dataManager.ServiceItems.GetServiceItem());
        }
    }
}
