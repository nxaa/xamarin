using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin2.Data.Models;
using Xamarin2.WebClient.Services;

namespace Xamarin2
{
    public partial class OrderDetailsPage : ContentPage
    {
        private ObservableCollection<CustomTextCell> menuItemsSource;
        private int id;
        private Order order;
        private Data.Models.MenuItem item;
        private int localId;

        public OrderDetailsPage(int id)
        {
            InitializeComponent();
            this.Title = "Order Details";

            menuItemsSource = new ObservableCollection<CustomTextCell>();
            listView.ItemsSource = menuItemsSource;
            this.id = id;
            this.localId = 0;

            LoadOrder();
        }

        protected override void OnAppearing()
        {
            if(item != null && menuItemsSource != null)
            {
                var orderItem = order.OrderItems.FirstOrDefault(o => o.MenuItem.MenuItemID == item.MenuItemID);
                if(orderItem != null)
                {
                    orderItem.Quantity++;
                    menuItemsSource.First(x => x.ItemID == orderItem.OrderItemID).Text = orderItem.Quantity + " " + orderItem.MenuItem.Name;
                }
                else
                {
                    LoadNewOrder();
                }
            }
        }

        void LoadOrder()
        {
            Task.Run(async () =>
            {
                try
                {
                    order = await RestOrderService.GetOrder(id);

                    foreach (var item in order.OrderItems)
                    {
                        var cell = new CustomTextCell();
                        cell.ItemID = item.OrderItemID;
                        cell.Text = item.Quantity + " " + item.MenuItem.Name;
                        cell.Detail = item.MenuItem.Price.ToString();
                        menuItemsSource.Add(cell);
                    }
                    var text = string.Empty;
                    foreach (var table in order.Tables.OrderBy(t => t.Number))
                    {
                        text += table.Number.ToString() + " ";
                    }
                    TablesText.Text = text;
                    DateText.Text = order.CreateDate.ToString(@"MM\/dd\/yyyy HH:mm");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", "Error while loading data", "OK");
                }
            });
        }

        private async void LoadNewOrder()
        {
            var itemDetails = await RestMenuItemService.GetMenuItem(item.MenuItemID);
            var newOrderItem = new OrderItem();
            newOrderItem.MenuItem = itemDetails;
            newOrderItem.Quantity = 1;
            newOrderItem.OrderItemID = --localId;
            order.OrderItems.Add(newOrderItem);

            item = null;

            var cell = new CustomTextCell();
            cell.ItemID = newOrderItem.OrderItemID;
            cell.Text = newOrderItem.Quantity + " " + newOrderItem.MenuItem.Name;
            cell.Detail = newOrderItem.MenuItem.Price.ToString();
            menuItemsSource.Add(cell);
        }

        async void AddMenuItem(object sender, EventArgs e)
        {
            item = new Data.Models.MenuItem();

            await Navigation.PushAsync(new MenuItemsPage(item));
        }

        void OnAdd(object sender, EventArgs e)
        {
            var menuItem = ((Xamarin.Forms.MenuItem)sender);
            var id = ((CustomTextCell)menuItem.CommandParameter).ItemID;
            var item = order.OrderItems.First(x => x.OrderItemID == id);
            item.Quantity ++;
            menuItemsSource.First(x => x.ItemID == id).Text = item.Quantity + " " + item.MenuItem.Name;
        }

        void OnSubstract(object sender, EventArgs e)
        {
            var menuItem = ((Xamarin.Forms.MenuItem)sender);
            var id = ((CustomTextCell)menuItem.CommandParameter).ItemID;
            var item = order.OrderItems.First(x => x.OrderItemID == id);
            item.Quantity--;
            if(item.Quantity == 0)
            {
                order.OrderItems.Remove(item);
                menuItemsSource.Remove(menuItemsSource.First(x => x.ItemID == id));
            }
            else
            {
                menuItemsSource.First(x => x.ItemID == id).Text = item.Quantity + " " + item.MenuItem.Name;
            }
        }

        void OnDelete(object sender, EventArgs e)
        {
            var menuItem = ((Xamarin.Forms.MenuItem)sender);
            var id = ((CustomTextCell)menuItem.CommandParameter).ItemID;
            order.OrderItems.Remove(order.OrderItems.First(x => x.OrderItemID == id));
            menuItemsSource.Remove(menuItemsSource.First(x => x.ItemID == id));
        }

        void SaveOrder(object sender, EventArgs e)
        {
            foreach(var item in order.OrderItems)
            {
                if(item.OrderItemID < 0)
                {
                    item.OrderItemID = 0;
                }
            }
            RestOrderService.Save(order);
        }

        void CloseOrder(object sender, EventArgs e)
        {
            foreach (var item in order.OrderItems)
            {
                if (item.OrderItemID < 0)
                {
                    item.OrderItemID = 0;
                }
            }
            order.Status = OrderStatus.Completed;
            RestOrderService.Save(order);
            
            Navigation.RemovePage(this);
        }
    }
}
