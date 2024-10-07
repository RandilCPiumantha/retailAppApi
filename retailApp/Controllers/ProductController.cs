using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using retailApp.Data;
using retailApp.Model;

namespace retailApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductContext _context;

        public ProductController(ProductContext context)
        {
            _context = context;
        }

        // POST: api/product
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product, string? disqualificationReason = null)
        {
            // Add the product to the context
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // If the product is disqualified, insert the disqualification reason
            if (!product.IsQualified && !string.IsNullOrEmpty(disqualificationReason))
            {
                var reason = new DisqualificationReason
                {
                    ProductId = product.ProductId,
                    Reason = disqualificationReason
                };

                _context.DisqualificationReasons.Add(reason);
                await _context.SaveChangesAsync();
            }

            // Return the created product with the disqualification reason if disqualified
            return CreatedAtAction(nameof(GetProducts), new { id = product.ProductId }, product);
        }

        // GET: api/product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            // Fetch all products along with their disqualification reasons
            var products = await _context.Products
                .Include(p => p.DisqualificationReasonEntity)
                .ToListAsync();

            // Attach disqualification reasons dynamically in the response
            foreach (var product in products)
            {
                product.DisqualificationReason = product.GetDisqualificationReason();
            }

            return products;
        }
    }
}
