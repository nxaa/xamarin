using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Xamarin2.Data.Models;
using Xamarin2.Web.Models;

namespace Xamarin2.Web.Controllers
{
    public class ReservationController : BaseController
    {
        [HttpGet]
        // GET: Reservation
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ReservationViewModel vm)
        {
            ValidateReservationViewModel(vm);

            var freeTables = GetFreeTables(vm.Date);
            var tables = GetTablesForPeople(vm.NumberOfPeople, freeTables);
            if(tables == null)
            {
                ModelState.AddModelError("NumberOfPeople", "There is no enough tables for this number of people.");
            }

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var reservation = new Reservation(); 
            reservation.Email = vm.Email;
            reservation.NumberOfPeople = vm.NumberOfPeople;
            reservation.Date = vm.Date;
            reservation.PhoneNumber = vm.PhoneNumber;
            reservation.Status = ReservationStatus.New;
            reservation.Tables = tables.ToList();

            db.Reservations.Add(reservation);

            db.SaveChanges();

            return RedirectToAction("Details", new { id = reservation.ReservationID } );
        }

        public ActionResult Details(int id)
        {
            var reservation = db.Reservations.Find(id);

            if (reservation == null)
            {
                return RedirectToAction("Error");
            }

            var vm = new ReservationViewModel()
            {
                Date = reservation.Date,
                Email = reservation.Email,
                NumberOfPeople = reservation.NumberOfPeople,
                PhoneNumber = reservation.PhoneNumber,
                Tables = reservation.Tables.Select(i => i.Number).ToList(),
            };

            return View(vm);
        }

        public ActionResult Error()
        {
            return View();
        }
        
        public IEnumerable<Table> GetFreeTables(DateTime date)
        {
            var dateFrom = date.AddHours(-1);
            var dateTo = date.AddHours(1);

            var reservations = db.Reservations.Where(r => r.Date >= dateFrom && r.Date <= dateTo && r.Status == ReservationStatus.New).Select(r => r.Tables);
            var tablesTaken = new List<int>();
            foreach(var tabletaken in reservations)
            {
                tablesTaken.AddRange(tabletaken.Select(t => t.TableID));
            }

            var tables = db.Tables.Where(t => !tablesTaken.Contains(t.TableID));

            return tables;
        }

        private IEnumerable<Table> GetTablesForPeople(int numberOfPeople, IEnumerable<Table> freeTables)
        {
            var number = numberOfPeople;
            var tables = new List<Table>();

            foreach (var table in freeTables)
            {
                tables.Add(table);
                number -= table.NumberOfPeople;
                if (number <= 0)
                {
                    break;
                }
            }

            if (number <= 0)
            {
                return tables;
            }
            else
            {
                return null;
            }
        }

        private void ValidateReservationViewModel(ReservationViewModel vm)
        {
            if (vm.NumberOfPeople <= 0)
            {
                ModelState.AddModelError("NumberOfPeople", "You must pick more than 0 people.");
            }
            try
            {
                vm.Date = DateTime.ParseExact(vm.DateString, "yyyy/MM/dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                if (vm.Date <= DateTime.Now.AddHours(2))
                {
                    ModelState.AddModelError("DateString", "You must pick date later than two hours from now.");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("DateString", "Date has invalid format.");
            }

            if (!string.IsNullOrWhiteSpace(vm.PhoneNumberString))
            {
                try
                {
                    vm.PhoneNumber = int.Parse(Regex.Replace(vm.PhoneNumberString.Replace('-', ' '), @"\s+", ""));
                    vm.PhoneNumber %= 1000000000;
                    if (vm.PhoneNumber < 100000000)
                    {
                        ModelState.AddModelError("PhoneNumberString", "Invalid number.");
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("PhoneNumberString", "Invalid number.");
                }
            }
        }
    }
}