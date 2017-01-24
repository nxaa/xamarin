using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin2.Data.Models;
using Xamarin2.WebClient.Services;

namespace Xamarin2
{
    public partial class ReservationDetailsPage : ContentPage
    {
        private int id;
        private Reservation reservation;
        public ReservationDetailsPage(int id)
        {
            InitializeComponent();
            this.Title = "Reservation Details";

            this.id = id;

            LoadReservation();
        }

        void LoadReservation()
        {
            Task.Run(async () =>
            {
                try
                {
                    reservation = await RestReservationService.GetReservation(id);
                    
                    var text = string.Empty;
                    foreach (var table in reservation.Tables.OrderBy(t => t.Number))
                    {
                        text += table.Number.ToString() + " ";
                    }

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        TablesText.Text = text;
                        DateText.Text = reservation.Date.ToString(@"MM\/dd\/yyyy HH:mm");
                        NumberOfPeopleText.Text = reservation.NumberOfPeople.ToString();
                        EmailText.Text = reservation.Email;
                        PhoneNumberText.Text = reservation.PhoneNumber.ToString();
                    });
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", "Error while loading data", "OK");
                }
            });
        }

        async void CreateOrder(object sender, EventArgs e)
        {
            Order order = new Order();
            order.CreateDate = DateTime.Now;
            order.Status = OrderStatus.InProgress;
            order.Tables = reservation.Tables;
            order.Reservation = reservation;
            
            var id = await RestOrderService.Add(order);

            reservation.Status = ReservationStatus.Completed;
            RestReservationService.Save(reservation);

            await Navigation.PushAsync(new OrderDetailsPage(id));

            Navigation.RemovePage(this);
        }

        void CancelReservation(object sender, EventArgs e)
        {
            reservation.Status = ReservationStatus.Cancelled;
            RestReservationService.Save(reservation);
        }
    }
}
