using blocv1.Models;
using blocv1.Models.dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace blocv1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly BlocContext _context;
        public UserController( BlocContext blocContext)
        {
            _context = blocContext;
            
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var list = await _context.Users.AsNoTracking().ToListAsync();

            return Ok(list);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserDto request)
        {

            if (request == null || request.correo == null || request.pass == null)
                return BadRequest(request);
            try
            {
                var user = _context.Users.First(x => x.Email == request.correo);


                if (user.Password != request.pass)
                    return BadRequest(false);

                return StatusCode(StatusCodes.Status200OK, new { user });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { user = false });

            }

        }

    }
}
