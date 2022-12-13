using Microsoft.AspNetCore.Mvc;
using ScSoMeAPI.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Connection = ScSoMeAPI.Models.Connection;

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

        [HttpGet]
        [Route("CheckIfAlreadyConnected")]
        public bool CheckIfAlreadyConnected([FromQuery] string user, string connectionUsername)
        {
            using BachelordbContext ctx = new BachelordbContext();
            bool connected = false;

            foreach (var connection in ctx.Connections.Where(c => c.UsernameCon1 == user || c.UsernameCon2 == user))
            {
                if (connection.UsernameCon1 == user && connection.UsernameCon2 == connectionUsername || connection.UsernameCon1 == connectionUsername && connection.UsernameCon2 == user)
                {
                    connected = true;
                }
            }
            return connected;
        }
        [HttpDelete]
        [Route("DeleteConnection")]
        public bool DeleteConnection([FromQuery] string user, string connectionUsername)
        {
            using BachelordbContext ctx = new BachelordbContext();
            bool deleted = false;
            Connection conToBeDeleted = null;
            var cl = ctx.Connections.Where(c => c.UsernameCon1 == user || c.UsernameCon2 == user);
            foreach (var connection in ctx.Connections.Where(c => c.UsernameCon1 == user || c.UsernameCon2 == user))
            {
                if (connection.UsernameCon1 == user && connection.UsernameCon2 == connectionUsername || connection.UsernameCon1 == connectionUsername && connection.UsernameCon2 == user)
                {
                    conToBeDeleted= connection;
                    deleted = true;

                }

            }
            if (deleted)
            {
                ctx.Connections.Remove(conToBeDeleted);
                ctx.SaveChanges();
            }
            return deleted;
        }

        //BLOCK FUNCTIONALITY
        [HttpGet]
        [Route("BlocksForUser")]
        public List<Block> GetBlocksForUser([FromQuery] string user)
        {
            using BachelordbContext ctx = new BachelordbContext();
            List<Block> blocks = new List<Block>();
            try
            {
                foreach (Block connection in ctx.Blocks.Where(c => c.UsernameCon1 == user || c.UsernameCon2 == user))
                {

                    blocks.Add(connection);

                }

            }
            catch (Exception e)
            {
                blocks = new List<Block>{ new Block
                {
                   UsernameCon1 = "Error",
                   UsernameCon2 = e.Message,
                   CreatedDate = DateTime.Now

                } };

            }
            return blocks;
        }
        [HttpPost]
        [Route("AddBlock")]
        public Block Postblock([FromBody] Block con)
        {
            using BachelordbContext ctx = new BachelordbContext();

            try
            {

                var result = ctx.Blocks.Add(con);
                ctx.SaveChanges();

            }
            catch (Exception e)
            {
                con = new Block
                {
                    UsernameCon1 = "Error",
                    UsernameCon2 = e.Message,
                    CreatedDate = DateTime.Now

                };

            }
            return con;
        }

        [HttpGet]
        [Route("CheckIfBlocked")]
        public string CheckIfBlocked([FromQuery] string user, string connectionUsername)
        {
            using BachelordbContext ctx = new BachelordbContext();
            string blockState = "not blocked";

            foreach (var connection in ctx.Blocks.Where(c => c.UsernameCon1 == user || c.UsernameCon2 == user))
            {
                if (connection.UsernameCon1 == user && connection.UsernameCon2 == connectionUsername)
                {
                    blockState = "you blocked";
                }
                else if (connection.UsernameCon1 == connectionUsername && connection.UsernameCon2 == user)
                {
                    blockState = "blocked you";
                }
            }
            return blockState;
        }
        [HttpDelete]
        [Route("DeleteBlock")]
        public bool DeleteBlock([FromQuery] string user, string connectionUsername)
        {
            using BachelordbContext ctx = new BachelordbContext();
            bool deleted = false;
            Block conToBeDeleted = null;
            foreach (var connection in ctx.Blocks.Where(c => c.UsernameCon1 == user || c.UsernameCon2 == user))
            {
                if (connection.UsernameCon1 == user && connection.UsernameCon2 == connectionUsername || connection.UsernameCon1 == connectionUsername && connection.UsernameCon2 == user)
                {
                    conToBeDeleted = connection;
                    deleted = true;
                    break;
                }
            }

            if (deleted)
            {
                ctx.Blocks.Remove(conToBeDeleted);

                ctx.SaveChanges();
            }

            return deleted;
        }
    }
}
