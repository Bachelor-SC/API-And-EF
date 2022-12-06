using Microsoft.AspNetCore.Mvc;
using ScSoMeAPI.Models;
using System;
using System.Linq;

namespace ScSoMeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActivityController
    {

        [HttpGet]
        [Route("LatestActivity")]
        public Activity GetActivity([FromQuery] string user)
        {
            using BachelordbContext ctx = new BachelordbContext();
            Activity activity;
            try
            {
                activity = ctx.Activities.OrderByDescending(activity => activity.Date).First(a => a.Username == user);
            }
            catch (Exception e)
            {
                activity = new Activity
                {
                    Username = user,
                    Type = "Error",
                    Action = "throwing",
                    AdditionalInfo = e.Message,
                    Date = DateTime.Now
                };

            }
            return activity;
        }
        [HttpGet]
        [Route("AllActivities")]
        public List<Activity> GetAllActivities([FromQuery] string user)
        {
            using BachelordbContext ctx = new BachelordbContext();
            List<Activity> activities;
            try
            {
                activities = ctx.Activities.Where(a => a.Username == user).OrderByDescending(a => a.Date).ToList();
            }
            catch (Exception e)
            {
                activities = new List<Activity>{ new Activity
                {
                    Username = user,
                    Type = "Error",
                    Action = "throwing",
                    AdditionalInfo = e.Message,
                    Date = DateTime.Now
                } };

            }
            return activities;
        }
        [HttpPost]
        [Route("LogActivity")]
        public Activity PostActivity([FromQuery] string user, [FromBody] Activity activity)
        {
            using BachelordbContext ctx = new BachelordbContext();

            try
            {
                activity.Username = user;
                var result = ctx.Activities.Add(activity);
                ctx.SaveChanges();
                
            }
            catch (Exception e)
            {
                activity = new Activity
                {
                    Username = user,
                    Type = "Error",
                    Action = "throwing",
                    AdditionalInfo = e.Message,
                    Date = DateTime.Now
                };

            }
            return activity;
        }

    }
}
