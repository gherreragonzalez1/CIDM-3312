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
            string pilotName = context.Request.RouteValues["name"] as string;

            using(var db = new VatsimDbContext()) 
            {
                if(pilotName != null)
                {
                    int responseCounter = 0;
                    Console.WriteLine($"{pilotName}");
                    /* .StartsWith() helps to look up a pilot without having to type the whole exact name.
                        E.g., if you want to look up "Christian Glaeser LOWW", you can just type
                        "Christian Glaeser" in the URL and still get a response.
                        Or type "Christian Glaeser LOWW" if you want the exact match.
                        In the same manner, if there are multiple pilots named Christian in the table, you can
                        just type "Christian" in the URL and get all of the pilots named Christian.
                        E.g.,
                        Christian Iturrieta SCEL
                        Christian Wopp EDDF
                        Christian Gullneritz, and so on...
                    */
                    var _pilots = await db.Positions.Where(p => p.Realname.StartsWith(pilotName ?? "")).Select(x => new { x.Realname, x.Altitude, x.TimeStamp }).ToListAsync();
                    foreach (var pilot in _pilots ) {
                    responseText = $"It is likely that {pilot.Realname} is flying at an altitude of {pilot.Altitude} ft on {pilot.TimeStamp}";
                    await context.Response.WriteAsync($"RESPONSE {++responseCounter}: {responseText} \n");
                    /* In the web browser, %20 represents a space in the URL for names, e.g. "dave%20s" = "dave s" */
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
            string pilotName = context.Request.RouteValues["name"] as string;

            using(var db = new VatsimDbContext()) 
            {
                if(pilotName != null)
                {
                    int responseCounter = 0;
                    Console.WriteLine($"{pilotName}");
                    var _pilots = await db.Positions.Where(p => p.Realname.StartsWith(pilotName ?? "")).Select(x => new { x.Realname, x.Groundspeed, x.TimeStamp }).ToListAsync();
                    foreach (var pilot in _pilots) {
                        responseText = $"It is likely that {pilot.Realname} is flying at {pilot.Groundspeed} mph on {pilot.TimeStamp}";
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
            string pilotName = context.Request.RouteValues["name"] as string;

            using(var db = new VatsimDbContext()) 
            {
                if(pilotName != null)
                {
                    int responseCounter = 0;
                    Console.WriteLine($"{pilotName}");
                    var _pilots = await db.Positions.Where(p => p.Realname.StartsWith(pilotName ?? "")).Select(x => new { x.Realname, x.Latitude, x.TimeStamp }).ToListAsync();
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
            string pilotName = context.Request.RouteValues["name"] as string;

            using(var db = new VatsimDbContext()) 
            {
                if(pilotName != null)
                {
                    int responseCounter = 0;
                    Console.WriteLine($"{pilotName}");
                    var _pilots = await db.Positions.Where(p => p.Realname.StartsWith(pilotName ?? "")).Select(x => new { x.Realname, x.Longitude, x.TimeStamp }).ToListAsync();
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