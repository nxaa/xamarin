using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Xamarin2
{
    public partial class QRScannerPage : ContentPage
    {
        private int? id;
        public QRScannerPage()
        {
            InitializeComponent();
            this.Title = "QR Code Scanner";
        }

        async void GetQR(object sender, EventArgs e)
        {
            try
            {
                var scanner = new ZXing.Mobile.MobileBarcodeScanner();
                var result = await scanner.Scan();

                if (result != null)
                {
                    int id;
                    var parseable = int.TryParse(result.Text, out id);
                    if (parseable)
                    {
                        this.id = id;
                    }
                    else
                    {
                        this.QRText.Text = "Invalid QR Code";
                    }
                }
            }
            catch (Exception ex)
            {
                this.QRText.Text = "Error while scanning";
            }

            if(id.HasValue)
            {
                await Task.Delay(2000);

                await Navigation.PushAsync(new ReservationDetailsPage(id.Value));
            }
        }
    }
}
