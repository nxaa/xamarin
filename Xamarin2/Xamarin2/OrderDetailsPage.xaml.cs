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

        public OrderDetailsPage(int id)
        {
            InitializeComponent();

            menuItemsSource = new ObservableCollection<CustomTextCell>();
            listView.ItemsSource = menuItemsSource;
            this.id = id;

            LoadOrder();
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

        void AddMenuItem(object sender, EventArgs e)
        {

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
            RestOrderService.Save(order);
        }

        void CloseOrder(object sender, EventArgs e)
        {
            order.Status = OrderStatus.Completed;
            RestOrderService.Save(order);
            
            Navigation.RemovePage(this);
        }
    }
}
