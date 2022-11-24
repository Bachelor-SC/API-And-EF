using Microsoft.AspNetCore.Mvc;
using ScSoMeAPI.UserDB;
using ScSoMeAPI.Models.User;

namespace ScSoMeAPI.Controllers.UserController
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private DBContext dbContext = new DBContext();

        [Route("Login")]
        [HttpGet]
        public async Task<ActionResult<User>> GetValidatedUser([FromQuery] string username, [FromQuery] string password)
        {
            try
            {
                User user = await dbContext.GetValidatedUser(username, password);
                
                //return user; 
                return Ok(user);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
                // TODO Add more exceptions? 404?
            }
        }

    }
}
