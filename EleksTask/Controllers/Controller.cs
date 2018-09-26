using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EleksTask
{
    [Route("api/[controller]")]
    [ApiController]
    public class Controller:ControllerBase
    {
        private readonly ApllicationContext _context;
        public Controller(ApllicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersAsync()
        {
            return Ok(_context.Users.Select(x=>x.FireName).ToArray());
        }
    }
}
