using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Xamarin2.Data.Models;
using Xamarin2.Web.Controllers;

namespace Xamarin2.Test
{
    [TestFixture]
    public class TestOrdersController
    {
        [Test]
        public void PostOrder_ShouldReturnSameOrder()
        {
            var controller = new OrdersController(new TestModel());

            var item = GetDemoOrder();

            var result = controller.PostOrder(item) as CreatedAtRouteNegotiatedContentResult<Order>;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteName, "DefaultApi");
            Assert.AreEqual(result.RouteValues["id"], result.Content.OrderID);
            Assert.AreEqual(result.Content.CreateDate, item.CreateDate);
            Assert.AreEqual(result.Content.Status, item.Status);
        }

        [Test]
        public void GetOrder_ShouldReturnOrderWithSameID()
        {
            var context = new TestModel();
            var item = GetDemoOrder();
            context.Orders.Add(item);

            var controller = new OrdersController(context);
            var result = controller.GetOrder(item.OrderID);

            Assert.IsNotNull(result);
            Assert.AreEqual(item.OrderID, result.OrderID);
        }

        [Test]
        public void GetOrders_ShouldReturnAllUncompletedOrders()
        {
            var context = new TestModel();

            foreach (var item in GetDemoOrders())
            {
                context.Orders.Add(item);
            }

            var controller = new OrdersController(context);
            var result = controller.GetOrders();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public void PutOrder_ShouldReturnStatusCode()
        {
            var controller = new OrdersController(new TestModel());

            var item = GetDemoOrder();

            Assert.Throws(typeof(NullReferenceException), () => { controller.PutOrder(item.OrderID, item); });
        }

        [Test]
        public void PutOrder_ShouldFail_WhenDifferentID()
        {
            var controller = new OrdersController(new TestModel());

            var badresult = controller.PutOrder(999, GetDemoOrder());
            Assert.IsInstanceOf(typeof(BadRequestResult), badresult);
        }

        Order GetDemoOrder()
        {
            return new Order() { OrderID = 1, Status = OrderStatus.InProgress, CreateDate = new DateTime (2007, 12, 15), Tables = new List<Table>(), OrderItems = new List<OrderItem>() };
        }

        List<Order> GetDemoOrders()
        {
            var orders = new List<Order>();
            orders.Add(new Order() { OrderID = 1, Status = OrderStatus.InProgress, CreateDate = new DateTime(2007, 12, 15), Tables = new List<Table>(), OrderItems = new List<OrderItem>() });
            orders.Add(new Order() { OrderID = 2, Status = OrderStatus.Completed, CreateDate = new DateTime(2003, 2, 5), Tables = new List<Table>(), OrderItems = new List<OrderItem>() });
            orders.Add(new Order() { OrderID = 3, Status = OrderStatus.InProgress, CreateDate = new DateTime(2005, 1, 1), Tables = new List<Table>(), OrderItems = new List<OrderItem>() });

            return orders;
        }
    }
}
