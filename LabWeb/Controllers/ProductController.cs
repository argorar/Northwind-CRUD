using LabWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LabWeb.Controllers
{
    public class ProductController : ApiController
    {
        ProductDAO productoDAO;

        public ProductController()
        {
            productoDAO = new ProductDAO();
        }
        //api/Product
        [HttpGet]
        public IHttpActionResult getProductos()
        {
            return Ok(productoDAO.getProductos());
        }
    }
}
