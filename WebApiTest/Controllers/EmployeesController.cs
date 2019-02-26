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
using WebApiTest.Models;

namespace WebApiTest.Controllers
{
    
    public class EmployeesController : ApiController
    {
        private MYEmployeeDBEntities db = new MYEmployeeDBEntities();

        // GET: api/Employees
        public IQueryable<Employee> GetEmployees()
        {
            return db.Employees;
        }
        //public IHttpActionResult Get(string salary = "1")
        //{
        //    try
        //    {
        //        int sal = Convert.ToInt32(salary);
        //        var employeesWithSalary = db.Employees.Where(emp =>  emp.Salary >= sal).ToList();
        //        if (employeesWithSalary != null || employeesWithSalary.Any())
        //            return Ok(employeesWithSalary);
        //        else
        //            return NotFound();
        //    }
        //    catch (Exception ex) {
        //        return BadRequest("Request parameter sent are not valid.");
        //    }
        //}

        // GET: api/Employees/5
        [ResponseType(typeof(Employee))]
        public IHttpActionResult GetEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // PUT: api/Employees/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployee(int id, Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employee.Id)
            {
                return BadRequest();
            }

            db.Entry(employee).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        // POST: api/Employees
        [ResponseType(typeof(Employee))]
        public IHttpActionResult PostEmployee(Employee employee)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.Employees.Add(employee);
                db.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { id = employee.Id }, employee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //HTTPResponseMessage is raw and can be created by user with rest recommendations However using IHTTPActionResult these things are resolved and you can use methods like 
        //NotFOund(), BadRequest, CreatedAtROute
        // DELETE: api/Employees/5
        [ResponseType(typeof(Employee))]
        public IHttpActionResult DeleteEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            db.Employees.Remove(employee);
            db.SaveChanges();

            return Ok(employee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeExists(int id)
        {
            return db.Employees.Count(e => e.Id == id) > 0;
        }
    }
}