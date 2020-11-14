using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

using VatsimLibrary.VatsimClientV1;
using VatsimLibrary.VatsimDb;

// Name: Gerardo Herrera Gonzalez
// CIDM-3312

namespace api
{
    public class PilotsEndpoint
    {
        public static async Task CallsignEndpoint(HttpContext context)
        {
            string responseText = null;
            string callsign = context.Request.RouteValues["callsign"] as string;
            switch((callsign ?? "").ToLower())
            {
                case "aal1":
                    responseText = "Callsign: AAL1";
                    break;
                default:
                    responseText = "Callsign: INVALID";
                    break;
            }

            if(callsign != null)
            {
                await context.Response.WriteAsync($"{responseText} is the callsign");
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
            }
        }


        /* NOTE: All of these require that you first obtain a pilot and then search in Positions */
        public static async Task AltitudeEndpoint(HttpContext context)
        {
            //TO DO
            string responseText = null;
            string pilotCallsign = context.Request.RouteValues["callsign"] as string;

            using(var db = new VatsimDbContext()) 
            {
                if(pilotCallsign != null)
                {
                    int responseCounter = 0;
                    Console.WriteLine($"{pilotCallsign}");
                    var _pilots = await db.Positions.Where(p => p.Callsign == (pilotCallsign ?? "").ToUpper()).Select(x => new { x.Realname, x.Altitude, x.TimeStamp }).ToListAsync();
                    foreach (var pilot in _pilots) {
                    responseText = $"It is likely that {pilot.Realname} is flying at an altitude of {pilot.Altitude} ft on {pilot.TimeStamp}";
                    await context.Response.WriteAsync($"RESPONSE {++responseCounter}: {responseText} \n");
                    }
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                }
            }
        }

        public static async Task GroundspeedEndpoint(HttpContext context)
        {
            //TO DO
            string responseText = null;
            string pilotCallsign = context.Request.RouteValues["callsign"] as string;

            using(var db = new VatsimDbContext()) 
            {
               if(pilotCallsign != null)
                {
                    int responseCounter = 0;
                    Console.WriteLine($"{pilotCallsign}");
                    var _pilots = await db.Positions.Where(p => p.Callsign == (pilotCallsign ?? "").ToUpper()).Select(x => new { x.Realname, x.Groundspeed, x.TimeStamp }).ToListAsync();
                    foreach (var pilot in _pilots) {
                    responseText = $"It is likely that {pilot.Realname} is flying at an altitude of {pilot.Groundspeed} ft on {pilot.TimeStamp}";
                    await context.Response.WriteAsync($"RESPONSE {++responseCounter}: {responseText} \n");
                    }
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                }
            }
        }

        public static async Task LatitudeEndpoint(HttpContext context)        
        {
            //TO DO
            string responseText = null;
            string pilotCallsign = context.Request.RouteValues["callsign"] as string;

            using(var db = new VatsimDbContext()) 
            {
                if(pilotCallsign != null)
                {
                    int responseCounter = 0;
                    Console.WriteLine($"{pilotCallsign}");
                    var _pilots = await db.Positions.Where(p => p.Callsign == (pilotCallsign ?? "").ToUpper()).Select(x => new { x.Realname, x.Latitude, x.TimeStamp }).ToListAsync();
                    foreach (var pilot in _pilots) {
                    responseText = $"It is likely that {pilot.Realname}'s latitude is {pilot.Latitude} degrees on {pilot.TimeStamp}";
                    await context.Response.WriteAsync($"RESPONSE {++responseCounter}: {responseText} \n");
                    }
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                }
            }
        }

        public static async Task LongitudeEndpoint(HttpContext context)
        {
            //TO DO
            string responseText = null;
            string pilotCallsign = context.Request.RouteValues["callsign"] as string;

            using(var db = new VatsimDbContext()) 
            {
                if(pilotCallsign != null)
                {
                    int responseCounter = 0;
                    Console.WriteLine($"{pilotCallsign}");
                    var _pilots = await db.Positions.Where(p => p.Callsign == (pilotCallsign ?? "").ToUpper()).Select(x => new { x.Realname, x.Longitude, x.TimeStamp }).ToListAsync();
                    foreach (var pilot in _pilots) {
                    responseText = $"It is likely that {pilot.Realname}'s longitude is {pilot.Longitude} degrees on {pilot.TimeStamp}";
                    await context.Response.WriteAsync($"RESPONSE {++responseCounter}: {responseText} \n");
                    }
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                }
            }
        }
    }
}