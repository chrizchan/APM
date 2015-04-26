using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using System.Web.OData;
using APM.WebAPI.Models;

namespace APM.WebAPI.Controllers
{
    [EnableCorsAttribute("http://localhost:38289", "*", "*")]
    public class ProductsController : ApiController
    {
        // GET: api/Products
        [EnableQuery]
        [ResponseType(typeof(Product))]
        public IHttpActionResult Get()
        {
            try
            {
                var procuctRepository = new ProductRepository();
                return Ok(procuctRepository.Retrieve().AsQueryable());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        //public IEnumerable<Product> Get(string search)
        //{
        //    var procuctRepository = new ProductRepository();
        //    var products = procuctRepository.Retrieve();

        //    return products.Where(x => x.ProductCode.Contains(search));
        //}

        // GET: api/Products/5
        [Authorize]
        [ResponseType(typeof(Product))]
        public IHttpActionResult Get(int id)
        {
            try
            {
                //throw new ArgumentException("This is a test");
                Product product;
                var productRepository = new ProductRepository();

                if (id > 0)
                {
                    var products = productRepository.Retrieve();
                    product = products.FirstOrDefault(x => x.ProductId == id);

                    if (product == null)
                        return NotFound();
                }
                else
                {
                    product = productRepository.Create();
                }

                return Ok(product);

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            
        }

        // POST: api/Products
        [ResponseType(typeof(Product))]
        public IHttpActionResult Post([FromBody]Product product)
        {
            try
            {
                if (product == null)
                    return BadRequest("Product cannot be null");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var productRepository = new ProductRepository();
                var newProduct = productRepository.Save(product);

                if (newProduct == null)
                    return Conflict();

                return Created<Product>(Request.RequestUri + newProduct.ProductId.ToString(), newProduct);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        // PUT: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult Put(int id, [FromBody]Product product)
        {
            try
            {
                if (product == null)
                    return BadRequest("Product cannot be null");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var productRepository = new ProductRepository();
                var updateProdcut = productRepository.Save(id,product);

                if (updateProdcut == null)
                    return NotFound();

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        // DELETE: api/Products/5
        public void Delete(int id)
        {
        }
    }
}
