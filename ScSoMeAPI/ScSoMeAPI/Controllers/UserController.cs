using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScSoMeAPI.Data;
using ScSoMeAPI.Models;
using ScSoMeAPI.Models.UserData;
using System.Diagnostics;

namespace ScSoMeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {

        [Route("Login")]
        [HttpGet]
        public async Task<ActionResult<User>> GetValidatedUser([FromQuery] string username, [FromQuery] string password)
        {
            using BachelordbContext ctx = new BachelordbContext();


            User first = ctx.Users.First(user => user.Username.Equals(username));
            if (first == null)
            {
                return StatusCode(500, "Something went wrong");
            }
            if (!first.HashedPassword.Equals(password))
            {
                return StatusCode(500, "Password incorrect");
            }

            return Ok(first);

        }

        /*
        [Route("Signup-User")]
        [HttpPost]
        public async Task<ActionResult<User>> PostCreateUser([FromBody] User user)
        {
            UserInfoHandler handler = new UserInfoHandler();

            return await handler.PostCreateUser(user);

        }
        */

        [Route("Signup")]
        [HttpPost]
        public async Task<ActionResult<UserInfo>> PostCreateUserInfo([FromBody] UserInfo userInfo)
        {
            try
            {
                UserInfoHandler handler = new UserInfoHandler();

                UserInfo userToBeAdded = await handler.PostCreateUserInfo(userInfo);
                return Created($"/{userToBeAdded.Username}", userToBeAdded);
            }
            catch (Exception e)
            {
                return StatusCode(201, e.Message); //Username taken
            }


        }

        [Route("Info")]
        [HttpGet]
        public async Task<ActionResult<UserInfo>> GetUserInfo([FromQuery] string username)
        {
            UserInfoHandler handler = new UserInfoHandler();

            try
            {
                UserInfo user = await handler.GetUserInfo(username);
                return Ok(user);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Route("Patch")]
        [HttpPatch]
        public async Task<ActionResult<UserInfo>> PatchUserInfo([FromBody] UserInfo userInfo)
        {
            UserInfoHandler handler = new UserInfoHandler();
            try
            {
                UserInfo userToBeAdded = await handler.PatchUserInfo(userInfo);
                return Ok(userToBeAdded);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("{username}")]
        public async Task<ActionResult> DeleteUserAsync([FromRoute] string username)
        {
            try
            {
                UserInfoHandler handler = new UserInfoHandler();

                await handler.DeleteUser(username);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        /*   Task BlockUser(string username, string toBeBlockedUser); */

    }
}
