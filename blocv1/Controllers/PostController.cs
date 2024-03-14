using blocv1.Models;
using blocv1.Models.dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace blocv1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly BlocContext _context;
        public PostController(BlocContext blocContext)
        {
            _context = blocContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var posts = await _context.Posts
                .AsNoTracking()
                .ToListAsync();

            return Ok( posts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var producto = await _context.Posts
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            return Ok(producto);
        }

        [HttpPost]
        public async Task<IActionResult> Add(PostDto invoiceDto)
        {
            var invoice = new Post
            {
                Body = invoiceDto.Body,
                Title = invoiceDto.Title,
                AuthorId = invoiceDto.AutorId,
                CategoryId = invoiceDto.Category,
                IsFeatured = invoiceDto.IsFeatured,
                DateTime = DateTime.Now,
                Thumbnail = null //Ingresar a futuro el almacenamiento de la imagen
            };
            try
            {
                _context.Posts.Add(invoice);
                var isProduct = await _context.SaveChangesAsync();
                return Ok(isProduct > 0);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Ok(false);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var invoice = _context.Posts.FirstOrDefault(m => m.Id == id);

                _context.Posts.Remove(invoice);
                var isSave = await _context.SaveChangesAsync();

                return Ok(isSave > 0);

            }
            catch (DbUpdateConcurrencyException)
            {
                return Ok(false);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update(PostDto invoiceDto)
        {
            var invoice = _context.Posts.FirstOrDefault(m => m.Id == invoiceDto.id);

            if (invoice == null)
                return Ok(false);

            try
            {
                invoice.Title = invoiceDto.Title;
                invoice.Body = invoiceDto.Body;
                invoice.CategoryId = invoiceDto.Category;
                invoice.IsFeatured = invoiceDto.IsFeatured;
               

                _context.Posts.Update(invoice);

                var isSave = await _context.SaveChangesAsync();
                return Ok( isSave > 0);

            }
            catch (DbUpdateConcurrencyException)
            {
                return Ok(false);
            }

        }
    }
}
