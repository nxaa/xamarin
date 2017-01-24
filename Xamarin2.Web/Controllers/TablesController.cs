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
using Xamarin2.Data.Interfaces;
using Xamarin2.Data.Models;

namespace Xamarin2.Web.Controllers
{
    public class TablesController : ApiController
    {
        private IModel db = new Model();

        public TablesController()
        {

        }

        public TablesController(IModel db)
        {
            this.db = db;
        }

        // GET: api/Tables
        public IQueryable<Table> GetTables()
        {
            return db.Tables;
        }

        // GET: api/Tables/5
        [ResponseType(typeof(Table))]
        public IHttpActionResult GetTable(int id)
        {
            Table table = db.Tables.Find(id);
            if (table == null)
            {
                return NotFound();
            }

            return Ok(table);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TableExists(int id)
        {
            return db.Tables.Count(e => e.TableID == id) > 0;
        }
    }
}