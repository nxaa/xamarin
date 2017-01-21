using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Xamarin2.Data;
using Xamarin2.Data.Models;

namespace Xamarin2.Web.Controllers
{
    public class ReservationsController : ApiController
    {
        private Model db = new Model();

        // GET: api/Reservations
        public IQueryable<Reservation> GetReservations()
        {
            return db.Reservations.Include(o => o.Tables).Where(r => r.Status == ReservationStatus.New)
                .OrderBy(r => r.Date).ToList().AsQueryable();
        }

        // GET: api/Reservations/5
        [ResponseType(typeof(Reservation))]
        public Reservation GetReservation(int id)
        {
            Reservation reservation = db.Reservations.Find(id);

            return reservation;
        }

        // PUT: api/Reservations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutReservation(int id, Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reservation.ReservationID)
            {
                return BadRequest();
            }

            var currentReservation = db.Reservations.Find(id);
            currentReservation.Status = reservation.Status;
            currentReservation.Date = reservation.Date;
            currentReservation.Email = reservation.Email;
            currentReservation.NumberOfPeople = reservation.NumberOfPeople;
            currentReservation.PhoneNumber = reservation.PhoneNumber;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReservationExists(int id)
        {
            return db.Reservations.Count(e => e.ReservationID == id) > 0;
        }
    }
}