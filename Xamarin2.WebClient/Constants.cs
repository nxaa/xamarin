using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xamarin2.WebClient
{
    static class Constants
    {
        private static string Domain = "http://DESKTOP-II58E4Q:7183/";

        public static string RestUrlOrders
        {
            get
            {
                return Domain + "api/Orders/{0}";
            }
        }

        public static string RestUrlReservations
        {
            get
            {
                return Domain + "api/Reservations/{0}";
            }
        }

        public static string RestUrlFreeTables
        {
            get
            {
                return Domain + "api/FreeTables";
            }
        }
        public static string RestUrlTables
        {
            get
            {
                return Domain + "api/Tables/{0}";
            }
        }
        public static string RestUrlMenuItems
        {
            get
            {
                return Domain + "api/MenuItems/{0}";
            }
        }
    }
}
