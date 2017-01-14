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
        }

        async void OnOrders(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OrdersPage());
        }

        async void OnNewOrder(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OrdersPage());
        }

        async void OnQRCode(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OrdersPage());
        }

        async void OnReservations(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OrdersPage());
        }
    }
}
