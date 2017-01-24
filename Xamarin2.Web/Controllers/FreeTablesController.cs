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
    public class FreeTablesController : ApiController
    {
        private IModel db = new Model();

        public FreeTablesController()
        {

        }

        public FreeTablesController(IModel db)
        {
            this.db = db;
        }

        // GET: api/FreeTables
        public IQueryable<object> GetTables()
        {
            var hourAgo = DateTime.Now.AddHours(-1);
            var hourLater = DateTime.Now.AddHours(1);

            var reservations = db.Reservations.Where(r => r.Status == ReservationStatus.New && r.Date >= hourAgo && r.Date <= hourLater).ToList();
            var orders = db.Orders.Where(o => o.Status == OrderStatus.InProgress).ToList();
            var tablesUsed = new HashSet<int>();

            foreach (var reservation in reservations)
            {
                foreach (var table in reservation.Tables)
                {
                    tablesUsed.Add(table.TableID);
                }
            }

            foreach (var order in orders)
            {
                foreach (var table in order.Tables)
                {
                    tablesUsed.Add(table.TableID);
                }
            }

            var tables = db.Tables.Where(t => !tablesUsed.Contains(t.TableID)).ToList();

            return tables.Select(t => new { TableID = t.TableID, Number = t.Number, NumberOfPeople = t.NumberOfPeople, Orders = new List<Order>(), Reservations = new List<Reservation>() }).AsQueryable();
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