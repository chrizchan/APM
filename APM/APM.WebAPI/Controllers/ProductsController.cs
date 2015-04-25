using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.OData;
using APM.WebAPI.Models;

namespace APM.WebAPI.Controllers
{
    [EnableCorsAttribute("http://localhost:38289", "*", "*")]
    public class ProductsController : ApiController
    {
        // GET: api/Products
        [EnableQuery]
        public IQueryable<Product> Get()
        {
            var procuctRepository = new ProductRepository();
            return procuctRepository.Retrieve().AsQueryable();
        }

        //public IEnumerable<Product> Get(string search)
        //{
        //    var procuctRepository = new ProductRepository();
        //    var products = procuctRepository.Retrieve();

        //    return products.Where(x => x.ProductCode.Contains(search));
        //}

        // GET: api/Products/5
        public Product Get(int id)
        {
            Product product;
            var productRepository = new ProductRepository();

            if (id > 0)
            {
                var products = productRepository.Retrieve();
                product = products.FirstOrDefault(x => x.ProductId == id);
            }
            else
            {
                product = productRepository.Create();
            }

            return product;

        }

        // POST: api/Products
        public void Post([FromBody]Product product)
        {
            var productRepository = new ProductRepository();
            productRepository.Save(product);
        }

        // PUT: api/Products/5
        public void Put(int id, [FromBody]Product product)
        {
            var productRepository = new ProductRepository();
            productRepository.Save(id,product);
        }

        // DELETE: api/Products/5
        public void Delete(int id)
        {
        }
    }
}
