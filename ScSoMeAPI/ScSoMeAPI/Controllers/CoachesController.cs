using Microsoft.AspNetCore.Mvc;
using ScSoMeAPI.Models;
using System;
using System.Linq;

namespace ScSoMeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoachesController
    {

        [HttpGet]
        [Route("Coach")]
        public Coach GetCoach([FromQuery] string coachToBeFound)
        {
            using BachelordbContext ctx = new BachelordbContext();
            Coach coach;
            try
            {
                coach = ctx.Coaches.First(a => a.username == coachToBeFound);
            }
            catch (Exception e)
            {
                coach = new Coach
                {
                    username = coachToBeFound,
                    name = "Error",
                    shortDesc = "an error occurred",
                    content = e.Message,
                    picture = ""
                };

            }
            return coach;
        }
        [HttpGet]
        [Route("AllCoaches")]
        public List<Coach> GetAllCoaches()
        {
            using BachelordbContext ctx = new BachelordbContext();
            List<Coach> activities;
            try
            {
                activities = ctx.Coaches.ToList();
            }
            catch (Exception e)
            {
                activities = new List<Coach>{ new Coach
                {
                    username = "No Coaches",
                    name = "Error",
                    shortDesc = "an error occurred",
                    content = e.Message,
                    picture = ""
                } };

            }
            return activities;
        }
        [HttpPost]
        [Route("registerCoach")]
        public Coach PostCoach([FromBody] Coach coach)
        {
            using BachelordbContext ctx = new BachelordbContext();

            try
            {
                var result = ctx.Coaches.Add(coach);
                ctx.SaveChanges();
                
            }
            catch (Exception e)
            {
                coach = new Coach
                {
                    username = "Error",
                    name = "Error",
                    shortDesc = "an error occurred",
                    content = e.Message,
                    picture = ""
                };

            }
            return coach;
        }

    }
}
