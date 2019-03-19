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
using LabWeb.Models;
using LabWeb.Data;

namespace LabWeb.Controllers
{
    public class CustomerController : ApiController
    {
        private NorthwindEntities db = new NorthwindEntities();
        CustomerDAO customerDAO = new CustomerDAO();

        // GET: api/Customer
        [HttpGet]
        public IHttpActionResult GetCustomers()
        {
            return Ok(customerDAO.consultarClientes());
        }

        // GET: api/Customer/5
        [HttpGet]
        public IHttpActionResult GetCustomers(string id)
        {
            return Ok(customerDAO.obtenerClientePorID(id));
        }

        // PUT: api/Customer/5
        [HttpPut]
        public IHttpActionResult PutCustomers(string id, Customers customers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            /*  if (id != customers.CustomerID)
              {
                  return BadRequest();
              }*/

            customers.b_logiv = 0;

            db.Entry(customers).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomersExists(id))
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

        // POST: api/Customer
        [ResponseType(typeof(Customers))]
        public IHttpActionResult PostCustomers(Customers customers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            customers.b_logiv = 0;

            db.Customers.Add(customers);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CustomersExists(customers.CustomerID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = customers.CustomerID }, customers);
        }

        // DELETE: api/Customer/5
        [ResponseType(typeof(Customers))]
        public IHttpActionResult DeleteCustomers(string id)
        {
            return Ok(customerDAO.eliminarCliente(id));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomersExists(string id)
        {
            return db.Customers.Count(e => e.CustomerID == id) > 0;
        }
    }
}