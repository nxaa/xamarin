using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Xamarin2
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            this.Title = "My Restaurant";
        }

        async void OnOrders(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OrdersPage());
        }

        async void OnNewOrder(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OrderCreatePage());
        }

        async void OnQRCode(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new QRScannerPage());
        }

        async void OnReservations(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ReservationsPage());
        }
    }
}
