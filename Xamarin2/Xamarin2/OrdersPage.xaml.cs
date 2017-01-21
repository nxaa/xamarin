using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin2.WebClient.Services;

namespace Xamarin2
{
    public partial class OrdersPage : ContentPage
    {
        private ObservableCollection<CustomTextCell> orderSource;
        public OrdersPage()
        {
            InitializeComponent();
            this.Title = "Orders";

            orderSource = new ObservableCollection<CustomTextCell>();
            listView.ItemsSource = orderSource;
            listView.ItemSelected += OnSelection;
            listView.IsPullToRefreshEnabled = true;
            listView.Refreshing += RefreshData;
        }

        protected override void OnAppearing ()
        {
            RefreshData(null, null);
        }

        async void OnSelection(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }
            await Navigation.PushAsync(new OrderDetailsPage(((CustomTextCell)e.SelectedItem).ItemID));
        }

        void RefreshData(object sender, EventArgs e)
        {
            Task.Run(async () =>
            {
                try
                {
                    var orders = await RestOrderService.GetOrders();

                    orderSource.Clear();

                    foreach (var order in orders)
                    {
                        var cell = new CustomTextCell();
                        cell.ItemID = order.OrderID;
                        cell.Detail = order.CreateDate.ToString(@"MM\/dd\/yyyy HH:mm");
                        if (order.Tables != null)
                        {
                            var text = string.Empty;
                            foreach (var table in order.Tables.OrderBy(t => t.Number))
                            {
                                text += table.Number.ToString() + " ";
                            }
                            cell.Text = text;
                        }
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            orderSource.Add(cell);
                        });
                    }
                }
                catch(Exception ex)
                {

                }
                finally
                {
                    listView.EndRefresh();
                }
            });
        }
    }
}
