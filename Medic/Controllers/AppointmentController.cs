using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Medic;
using Medic.Models;

namespace Medic.Controllers
{
    public class AppointmentController : Controller
    {
        private WebMedicContext db = new WebMedicContext();

        // GET: Appointment
        public ActionResult Index(int pID, Specialization? id, DateTime? fromDate, DateTime? toDate)
        {
            var appointments = db.Appointments.Include(a => a.Doctor).Include(a => a.Patient);
            appointments = appointments.Where(a => a.PatientID == 1);
            if (id != null)
            {
                appointments = appointments.Where(s => s.Doctor.Specialization == id);
            }
            if (fromDate != null && toDate != null)
            {
                appointments = appointments.Where(s => s.Date >= fromDate).Where(s => s.Date <= toDate);
            }
            ViewBag.PatientID = pID;
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;

            return View(appointments.ToList());
        }

        // GET: Appointment/Create
        public ActionResult Create(int id)
        {
            ViewBag.DoctorID = db.Doctors.Select(p => new SelectListItem
            {
                Text = p.Name + " " + p.Surname,
                Value = p.DoctorID.ToString()
            });
            ViewBag.PatientID = db.Patients.Select(p => new SelectListItem
            {
                Text = p.Name + " " + p.Surname,
                Value = p.PatientID.ToString()
            });
            Doctor doctor = db.Doctors.Find(id);
            ViewBag.doctor = doctor.Name + " " + doctor.Surname;
            ViewBag.id = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AppointmentID,DoctorID,PatientID,Date,Place")] Appointment appointment)
        {

            if (ModelState.IsValid)
            {
                db.Appointments.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("Index", "Doctor", new { id = appointment.DoctorID });
            }

            ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "Name", appointment.DoctorID);
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "Name", appointment.PatientID);
            return View(appointment);
        }

        public ActionResult Summary(int patientid, int appointmentid)
        {
            Appointment appointment = db.Appointments.Find(appointmentid);
            appointment.PatientID = patientid;
            db.SaveChanges();
            ViewBag.PatientID = patientid;
            return View(appointment);
        }

        // GET: Appointment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "Name", appointment.DoctorID);
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "Name", appointment.PatientID);
            return View(appointment);
        }

        // POST: Appointment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AppointmentID,DoctorID,PatientID,Date,Place")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "Name", appointment.DoctorID);
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "Name", appointment.PatientID);
            return View(appointment);
        }

        // GET: Appointment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Appointment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appointment appointment = db.Appointments.Find(id);
            db.Appointments.Remove(appointment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
