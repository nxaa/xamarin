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
    public partial class OrderCreatePage : ContentPage
    {
        private ObservableCollection<CustomSwitchCell> orderSource;
        private Order order;
        public OrderCreatePage()
        {
            InitializeComponent();

            order = new Order();
            orderSource = new ObservableCollection<CustomSwitchCell>();
            listView.ItemsSource = orderSource;

            LoadTables();
        }

        private void LoadTables()
        {
            Task.Run(async () =>
            {
                try
                {
                    var tables = await RestTableService.GetFreeTables();

                    orderSource.Clear();

                    foreach (var table in tables)
                    {
                        var cell = new CustomSwitchCell();
                        cell.ItemID = table.TableID;
                        cell.Number = table.Number;
                        cell.NumberOfPeople = table.NumberOfPeople;
                        cell.Selected = false;
                        cell.Text = table.Number.ToString() + " - " + table.NumberOfPeople.ToString() + " people.";
                        
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            orderSource.Add(cell);
                        });
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    listView.EndRefresh();
                }
            });
        }

        async void CreateOrder(object sender, EventArgs e)
        {
            order.CreateDate = DateTime.Now;
            order.Status = OrderStatus.InProgress;
            order.Tables = new List<Table>(orderSource.Where(o => o.Selected)
                .Select(o => new Table() { TableID = o.ItemID, Number = o.Number, NumberOfPeople = o.NumberOfPeople }));

            var id = await RestOrderService.Add(order);

            await Navigation.PushAsync(new OrderDetailsPage(id));

            Navigation.RemovePage(this);
        }
    }
}
