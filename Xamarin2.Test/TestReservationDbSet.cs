using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin2.Data.Models;

namespace Xamarin2.Test
{
    class TestReservationDbSet : TestDbSet<Reservation>
    {
        public override Reservation Find(params object[] keyValues)
        {
            return this.SingleOrDefault(reservation => reservation.ReservationID == (int)keyValues.Single());
        }
    }
}
