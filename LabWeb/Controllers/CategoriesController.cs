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
    public class CategoriesController : ApiController
    {
        private NorthwindEntities db = new NorthwindEntities();
        CategoriesDAO categoryDAO = new CategoriesDAO();

        // GET: api/Categories
        [HttpGet]
        public IHttpActionResult GetCategories()
        {
            return Ok(categoryDAO.consultarCategorias());
        }

        // GET: api/Categories/5
        [HttpGet]
        public IHttpActionResult GetCategories(int id)
        {
            return Ok(categoryDAO.obtenerCategoriasPorID(id));
        }

        // PUT: api/Categories/5
        [HttpPut]
        public IHttpActionResult PutCategories(int id, Categories categories)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != categories.CategoryID)
            {
                return BadRequest();
            }

            db.Entry(categories).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriesExists(id))
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

        // POST: api/Categories
        [ResponseType(typeof(Categories))]
        public IHttpActionResult PostCategories(Categories categories)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            categories.b_logiv = 0;//hhh
            db.Categories.Add(categories);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = categories.CategoryID }, categories);
        }

        // DELETE: api/Categories/5
        [ResponseType(typeof(Categories))]
        public IHttpActionResult DeleteCategories(int id)
        {
            return Ok(categoryDAO.eliminarCategoria(id));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoriesExists(int id)
        {
            return db.Categories.Count(e => e.CategoryID == id) > 0;
        }
    }
}