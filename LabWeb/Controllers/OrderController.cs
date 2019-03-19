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
using LabWeb.Data;
using LabWeb.Models;

namespace LabWeb.Controllers
{
    public class OrderController : ApiController
    {
        private NorthwindEntities db = new NorthwindEntities();
        OrderDAO orderDAO = new OrderDAO();

        // GET: api/Order
        [HttpGet]
        public IHttpActionResult GetOrders()
        {
            return Ok(orderDAO.consultarPedidos());
        }

        // GET: api/Order/5
        [HttpGet]
        public IHttpActionResult GetOrders(int id)
        {
            return Ok(orderDAO.obtenerPedidosPorID(id));
        }

        // PUT: api/Order/5
        [HttpPut]
        public IHttpActionResult PutOrders(int id, Orders orders)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orders.OrderID)
            {
                return BadRequest();
            }

            db.Entry(orders).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdersExists(id))
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

        // POST: api/Order
        [HttpPost]
        public IHttpActionResult PostOrders(Orders orders)
        {
            return Ok(orderDAO.insertarPedido(orders));
        }

        // DELETE: api/Order/5
        [HttpDelete]
        public IHttpActionResult DeleteOrders(int id)
        {
            return Ok(orderDAO.eliminarPedido(id));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrdersExists(int id)
        {
            return db.Orders.Count(e => e.OrderID == id) > 0;
        }
    }
}