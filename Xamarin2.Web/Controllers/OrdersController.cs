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
    public class OrdersController : ApiController
    {
        private IModel db = new Model();

        public OrdersController()
        {

        }

        public OrdersController(IModel db)
        {
            this.db = db;
        }

        // GET: api/Orders
        public IQueryable<Order> GetOrders()
        {
            return db.Orders.Include(o => o.Tables).Where(o=> o.Status == OrderStatus.InProgress).ToList().AsQueryable();
        }

        // GET: api/Orders/5
        [ResponseType(typeof(Order))]
        public Order GetOrder(int id)
        {
            Order order = db.Orders.Find(id);

            return order;
        }

        // PUT: api/Orders/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrder(int id, Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.OrderID)
            {
                return BadRequest();
            }

            var currentOrder = db.Orders.Find(id);
            currentOrder.CloseDate = order.CloseDate;
            currentOrder.CreateDate = order.CreateDate;
            currentOrder.Status = order.Status;

            var orderItemIds = currentOrder.OrderItems.Select(i => i.OrderItemID);
            var orderItems = db.OrderItems.Where(i => orderItemIds.Contains(i.OrderItemID));
            
            var itemsToRemove = new List<OrderItem>();

            foreach (var item in orderItems)
            {
                var orderItem = order.OrderItems.FirstOrDefault(i => i.OrderItemID == item.OrderItemID);
                if (orderItem != null)
                {
                    item.Quantity = orderItem.Quantity;
                }
                else
                {
                    itemsToRemove.Add(item);
                }
            }

            foreach (var item in order.OrderItems)
            {
                if (currentOrder.OrderItems.FirstOrDefault(i => i.OrderItemID == item.OrderItemID) == null)
                {
                     db.OrderItems.Add(new OrderItem()
                     {
                         MenuItem = db.MenuItems.Find(item.MenuItem.MenuItemID),
                         Order = currentOrder,
                         Quantity = item.Quantity
                     });
                }
            }

            db.OrderItems.RemoveRange(itemsToRemove);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Orders
        [ResponseType(typeof(Order))]
        public IHttpActionResult PostOrder(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orderToAdd = new Order();
            orderToAdd.CreateDate = order.CreateDate;
            orderToAdd.Status = order.Status;
            if(order.Reservation != null)
            {
                orderToAdd.Reservation = db.Reservations.Find(order.Reservation.ReservationID);
            }
            orderToAdd.Tables = new List<Table>();

            foreach (var table in order.Tables)
            {
                orderToAdd.Tables.Add(db.Tables.First(t => t.TableID == table.TableID));
            }

            db.Orders.Add(orderToAdd);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = orderToAdd.OrderID }, orderToAdd);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderExists(int id)
        {
            return db.Orders.Count(e => e.OrderID == id) > 0;
        }
    }
}