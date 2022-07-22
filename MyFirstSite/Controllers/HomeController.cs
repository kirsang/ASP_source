using Microsoft.AspNetCore.Mvc;
using MyFirstSite.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstSite.Controllers
{    
    public class HomeController : Controller
    {
        private readonly DataManager dataManager;

        public HomeController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }

        public IActionResult Index()
        { 
            return View(dataManager.TextFields.GetTextFieldByCodeWord("PageIndex"));
        }//Ид и время добираются сюда

        public IActionResult Contaсtsss()
        {
            return View(dataManager.TextFields.GetTextFieldByCodeWord("PageContacts"));
        }
    }
}
