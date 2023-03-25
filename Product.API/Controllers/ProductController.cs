using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product.API.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly ProductsDbContext context;
        //Construstor Injection
        public ProductsController(ProductsDbContext context)
        {
            this.context = context;
        }


        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Product>>> ProductList()
        {
            List<Models.Product> ProductList = await context.Products.ToListAsync();
            return ProductList;
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Product>> GetProduct(int id)
        {
            //Veri tabanında ==> select * from where Product ID=id //komutunu çalıştıracak
            var Product = await context.Products.FindAsync(id);
            if (Product == null)
            {
                return NotFound();
            }
            return Product;
        }

        // POST api/<ProductsController>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Models.Product>>> AddProduct(Models.Product Product)
        {
            //insert into Product() values(Product.ID,Product.Name)
            context.Products.Add(Product);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return CreatedAtAction("GetProduct", new { id = Product.ID }, Product);
            //eklenen değeri detayını getirecek olan kod
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<Models.Product>>> ProductUpdate(Models.Product Product)
        {
            context.Entry(Product).State = EntityState.Modified;
            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return CreatedAtAction("GetProduct", new { id = Product.ID }, Product);
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Models.Product>> ProductDelete(int id)
        {
            var Product = await context.Products.FindAsync(id);
            if (Product == null) { return NotFound(); }
            context.Products.Remove(Product);
            await context.SaveChangesAsync();
            return Product;
        }
    }
}
