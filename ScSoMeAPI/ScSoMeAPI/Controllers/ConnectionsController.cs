using Microsoft.AspNetCore.Mvc;
using ScSoMeAPI.Models;

namespace ScSoMeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConnectionsController
    {
        [HttpGet]
        [Route("AllConnections")]
        public List<Connection> GetAllConnections([FromQuery] string user)
        {
            using BachelordbContext ctx = new BachelordbContext();
            List<Connection> connections = new List<Connection>();
            try
            {
                foreach (var connection in ctx.Connections.Where(c => c.UsernameCon1 == user || c.UsernameCon2 == user))
                {
                    if (connection.UsernameCon2 == user)
                    {
                        connections.Add(new Connection
                        {
                            UsernameCon1 = user,
                            UsernameCon2 = connection.UsernameCon1,
                            CreatedDate = connection.CreatedDate
                        });
                    }
                    else
                    {
                        connections.Add(connection);
                    }
                }

            }
            catch (Exception e)
            {
                connections = new List<Connection>{ new Connection
                {
                   UsernameCon1 = "Error",
                   UsernameCon2 = e.Message,
                   CreatedDate = DateTime.Now

                } };

            }
            return connections;
        }
        [HttpPost]
        [Route("AddConnection")]
        public Connection PostConnection([FromBody] Connection con)
        {
            using BachelordbContext ctx = new BachelordbContext();

            try
            {
                
                var result = ctx.Connections.Add(con);
                ctx.SaveChanges();

            }
            catch (Exception e)
            {
                con = new Connection
                {
                    UsernameCon1 = "Error",
                    UsernameCon2 = e.Message,
                    CreatedDate = DateTime.Now

                };

            }
            return con;
        }
    }
}
