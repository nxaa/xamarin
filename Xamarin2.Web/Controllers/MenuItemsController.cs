using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Xamarin2.Data;
using Xamarin2.Data.Models;

namespace Xamarin2.Web.Controllers
{
    public class MenuItemsController : ApiController
    {
        private Model db = new Model();

        // GET: api/MenuItems
        public IQueryable<MenuItem> GetMenuItems()
        {
            return db.MenuItems.Where(i => i.Active).ToList().AsQueryable();
        }

        // GET: api/MenuItems/5
        [ResponseType(typeof(MenuItem))]
        public MenuItem GetMenuItem(int id)
        {
            MenuItem menuItem = db.MenuItems.Find(id);

            return menuItem;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MenuItemExists(int id)
        {
            return db.MenuItems.Count(e => e.MenuItemID == id) > 0;
        }
    }
}