﻿using Hospital.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly HospitalContext _db;

        public AppointmentsController(HospitalContext context)
        {
            _db = context;
        }

        // READ -GET /Appointments
        public IActionResult Index()
        {
            var Appointments = _db.Appointments.ToList();
            ViewData["Appointments"] = Appointments;
            return View(Appointments);

        }

        // GET: /Appointments/id
        public IActionResult Details(int? id)
        {
            var details = _db.Appointments.ToList().Find(details => details.Id == id);
            if (id == null || details == null)
            {
                return View("_NotFound");
            }
            ViewData["Details"] = details;
            return View(details);
        }

        //GET - /Appointments/create
        public IActionResult Create()
        {
            return View();
        }

        //POST - /Appoinments/create
        [HttpPost]
        public IActionResult Create([Bind("Id", "PatientName", "DoctorName", "Speciality", "Date", "Time")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _db.Appointments.Add(appointment);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Create");
        }

        //GET - /Appointments/edit/id
        public IActionResult Edit(int? id)
        {
            var Appointment = _db.Appointments.ToList().Find(a => a.Id == id);
            if (id == null || Appointment == null)
            {
                return View("_NotFound");
            }
            ViewData["Appointment"] = Appointment;
            return View();
        }

        //POST - /Appointments/edit/id
        [HttpPost]
        public IActionResult Edit([Bind("Id", "PatientName", "DoctorName", "Speciality", "Date", "Time")] Appointment app)
        {
            _db.Appointments.Update(app);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        // POST - /Appointments/delete/id
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var appointment = _db.Appointments.ToList().Find(a => a.Id == id);
            _db.Appointments.Remove(appointment);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
