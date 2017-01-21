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
    public partial class MenuItemsPage : ContentPage
    {
        private ObservableCollection<CustomTextCell> itemsSource;
        private Data.Models.MenuItem menuItem;

        public MenuItemsPage(Data.Models.MenuItem item)
        {
            InitializeComponent();
            this.Title = "Menu";

            this.menuItem = item;
            itemsSource = new ObservableCollection<CustomTextCell>();
            listView.ItemsSource = itemsSource;
            listView.ItemSelected += OnSelection;
        }

        protected override void OnAppearing()
        {
            RefreshData(null, null);
        }

        async void OnSelection(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }
            menuItem.MenuItemID = ((CustomTextCell)e.SelectedItem).ItemID;
            await Navigation.PopAsync();
        }

        void RefreshData(object sender, EventArgs e)
        {
            Task.Run(async () =>
            {
                try
                {
                    var menuItems = await RestMenuItemService.GetMenuItems();

                    itemsSource.Clear();

                    foreach (var item in menuItems)
                    {
                        var cell = new CustomTextCell();
                        cell.ItemID = item.MenuItemID;
                        cell.Detail = item.Price.ToString();
                        cell.Text = item.Name;

                        Device.BeginInvokeOnMainThread(() =>
                        {
                            itemsSource.Add(cell);
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
