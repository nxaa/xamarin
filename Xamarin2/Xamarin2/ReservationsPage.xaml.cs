using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin2.WebClient.Services;

namespace Xamarin2
{
    public partial class ReservationsPage : ContentPage
    {
        private ObservableCollection<CustomTextCell> reservationSource;
        public ReservationsPage()
        {
            InitializeComponent();
            this.Title = "Reservations";

            reservationSource = new ObservableCollection<CustomTextCell>();
            listView.ItemsSource = reservationSource;
            listView.ItemSelected += OnSelection;
            listView.IsPullToRefreshEnabled = true;
            listView.Refreshing += RefreshData;
        }

        protected override void OnAppearing()
        {
            RefreshData(null, null);
        }

        async void OnSelection(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            await Navigation.PushAsync(new ReservationDetailsPage(((CustomTextCell)e.SelectedItem).ItemID));
        }

        void RefreshData(object sender, EventArgs e)
        {
            Task.Run(async () =>
            {
                try
                {
                    var reservations = await RestReservationService.GetReservations();

                    reservationSource.Clear();

                    foreach (var reservation in reservations)
                    {
                        var cell = new CustomTextCell();
                        cell.ItemID = reservation.ReservationID;
                        cell.Detail = reservation.Date.ToString(@"MM\/dd\/yyyy HH:mm");
                        if (reservation.Tables != null)
                        {
                            var text = string.Empty;
                            foreach (var table in reservation.Tables.OrderBy(t => t.Number))
                            {
                                text += table.Number.ToString() + " ";
                            }
                            cell.Text = text;
                        }
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            reservationSource.Add(cell);
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
    }
}
