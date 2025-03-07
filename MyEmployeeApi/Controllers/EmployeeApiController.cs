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
using MyEmployeeApi.Models;

namespace MyEmployeeApi.Controllers
{
    public class EmployeeApiController : ApiController
    {
        private EmployeeDbEntities db = new EmployeeDbEntities();

        // GET: api/EmployeeApi
        public IQueryable<tbl_Employees> Gettbl_Employees()
        {
            return db.tbl_Employees;
        }

        // GET: api/EmployeeApi/5
        [ResponseType(typeof(tbl_Employees))]
        public IHttpActionResult Gettbl_Employees(int id)
        {
            tbl_Employees tbl_Employees = db.tbl_Employees.Find(id);
            if (tbl_Employees == null)
            {
                return NotFound();
            }

            return Ok(tbl_Employees);
        }

        // PUT: api/EmployeeApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_Employees(int id, tbl_Employees tbl_Employees)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_Employees.EmpId)
            {
                return BadRequest();
            }

            db.Entry(tbl_Employees).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_EmployeesExists(id))
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

        // POST: api/EmployeeApi
        [ResponseType(typeof(tbl_Employees))]
        public IHttpActionResult Posttbl_Employees(tbl_Employees tbl_Employees)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_Employees.Add(tbl_Employees);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_Employees.EmpId }, tbl_Employees);
        }

        // DELETE: api/EmployeeApi/5
        [ResponseType(typeof(tbl_Employees))]
        public IHttpActionResult Deletetbl_Employees(int id)
        {
            tbl_Employees tbl_Employees = db.tbl_Employees.Find(id);
            if (tbl_Employees == null)
            {
                return NotFound();
            }

            db.tbl_Employees.Remove(tbl_Employees);
            db.SaveChanges();

            return Ok(tbl_Employees);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_EmployeesExists(int id)
        {
            return db.tbl_Employees.Count(e => e.EmpId == id) > 0;
        }
    }
}