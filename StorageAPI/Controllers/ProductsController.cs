using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StorageAPI.Data;
using StorageAPI.DTO;
using StorageAPI.Models;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly StorageAPIContext _context;
    public ProductsController(StorageAPIContext context)
    {
        _context = context;
    }

    // GET: api/Product
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CreateProductDto>>> GetProduct(
        [FromQuery] string? name, string? category)
    {
        List<CreateProductDto> productsToReturn = new List<CreateProductDto>();
        if (string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(category))
        {
            var products = await _context.Product.ToListAsync();
            foreach (var product in products)
                productsToReturn.Add(ProductConverter.ToProductDto(product));
        }
        else if (!string.IsNullOrWhiteSpace(name)) {
            var products = await _context.Product
                .Where(p => p.Name == name)
                .ToListAsync();
            foreach (var product in products)
                productsToReturn.Add(ProductConverter.ToProductDto(product));
        }
        else if (!string.IsNullOrWhiteSpace(category))
        {
            var products = await _context.Product
                .Where(p => p.Category == category)
                .ToListAsync();
            foreach (var product in products)
                productsToReturn.Add(ProductConverter.ToProductDto(product));
        }
        else {
            var products = await _context.Product
                .Where(p => p.Name == name)
                .Where(p => p.Category == category)
                .ToListAsync();
            foreach (var product in products)
            {
                productsToReturn.Add(ProductConverter.ToProductDto(product));
            }
        }
        return productsToReturn;
    }

    // GET: api/Product/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CreateProductDto>> GetProduct(int id)
    {
        var product = await _context.Product.FindAsync(id);

        if (product is null)
        {
            return NotFound();
        }

        return ProductConverter.ToProductDto(product);
    }

    // PUT: api/Product/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduct(int? id, Product product)
    {
        if (id != product.Id)
        {
            return BadRequest();
        }

        _context.Entry(product).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProductExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Product
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Product>> PostProduct(CreateProductDto product)
    {
        var productToAdd = ProductConverter.ToProduct(product);
        _context.Product.Add(productToAdd);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetProduct", new { id = productToAdd.Id }, productToAdd);
    }

    // DELETE: api/Product/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int? id)
    {
        var product = await _context.Product.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        _context.Product.Remove(product);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("stats")]
    public async Task<ActionResult<DataStats>> GetStats() {

        var products = _context.Product.ToList();

        int totalItemCount = products.Sum(p => p.Count);
        int totalItemPrice = products.Sum(p => p.Price * p.Count);
        decimal medianPrice = ((decimal) totalItemPrice) / (totalItemCount > 0 ? totalItemCount : 1);

        return new DataStats(totalItemCount, totalItemPrice, medianPrice);
    }

    private bool ProductExists(int? id)
    {
        return _context.Product.Any(e => e.Id == id);
    }
}
