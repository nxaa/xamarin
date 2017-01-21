using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xamarin2.Web.Models;

namespace Xamarin2.Web.Controllers
{
    public class MenuController : BaseController
    {
        // GET: Menu
        public ActionResult Index()
        {
            var vm = db.MenuItems.Where(i => i.Active).OrderBy(i => i.MenuItemCategory).ToList()
                .Select(i => new MenuViewModel()
                {
                    Name = i.Name,
                    Description = i.Description,
                    Price = i.Price,
                    MenuItemCategory = i.MenuItemCategory.ToString(),
                });

            return View(vm);
        }
    }
}