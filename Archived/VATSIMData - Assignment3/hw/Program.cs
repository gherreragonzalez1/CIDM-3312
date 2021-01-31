using System;
using System.IO;
using System.Linq;

using VatsimLibrary.VatsimClient;
using VatsimLibrary.VatsimDb;

// Name: Gerardo Herrera Gonzalez
// CIDM-3312, Assignment 3

namespace hw
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine($"{VatsimDbHepler.DATA_DIR}");

            using(var db = new VatsimDbContext())
            {
                // QUERY 1 STARTS HERE
                Console.WriteLine("Query 1: Which pilot has been logged on the longest?");
                var lstPilots = db.Pilots.ToList();
                foreach (var item in lstPilots)
                {
                   // Convert TimeLogon in lstPilots to DateTime
                   string year = item.TimeLogon.Substring(0, 4);
                   string month = item.TimeLogon.Substring(4, 2);
                   string day = item.TimeLogon.Substring(6, 2);
                   string hours = item.TimeLogon.Substring(8, 2);
                   string minutes = item.TimeLogon.Substring(10, 2);
                   string seconds = item.TimeLogon.Substring(12, 2);
                   item.dtTimeLogon = DateTime.Parse(year+"-"+month+"-"+day+" " + hours+":"+minutes+":"+seconds);
                   // Get DateDiff from Now
                   DateTime dtNow = DateTime.Now;
                   item.DateDiff = (dtNow - item.dtTimeLogon).TotalMinutes;
                }
                double pilotDiff = lstPilots.Max(x => x.DateDiff);
                var pilot = lstPilots.Where(x=>x.DateDiff == pilotDiff).FirstOrDefault();
                Console.WriteLine($"{pilot.Realname}, who has been logged on for {pilotDiff:0.00} minutes.");
                Console.WriteLine();

                // QUERY 2 STARTS HERE
                Console.WriteLine("Query 2: Which controller has been logged on the longest?");
                var lstControllers = db.Controllers.ToList();
                foreach (var item in lstControllers)
                {
                   // Convert TimeLogon in lstControllers to DateTime
                   string year = item.TimeLogon.Substring(0, 4);
                   string month = item.TimeLogon.Substring(4, 2);
                   string day = item.TimeLogon.Substring(6, 2);
                   string hours = item.TimeLogon.Substring(8, 2);
                   string minutes = item.TimeLogon.Substring(10, 2);
                   string seconds = item.TimeLogon.Substring(12, 2);
                   item.dtTimeLogon = DateTime.Parse(year+"-"+month+"-"+day+" " + hours+":"+minutes+":"+seconds);
                   // Get DateDiff from Now
                   DateTime dtNow = DateTime.Now;
                   item.DateDiff = (dtNow - item.dtTimeLogon).TotalMinutes;
                }
                double controllerDiff = lstControllers.Max(x => x.DateDiff);
                var controller = lstControllers.Where(x=>x.DateDiff == controllerDiff).FirstOrDefault();
                Console.WriteLine($"{controller.Realname}, who has been logged on for {controllerDiff:0.00} minutes.");
                Console.WriteLine();

                // QUERY 3 STARTS HERE
                Console.WriteLine("Query 3: Which airport has the most departures?");
                var query3 = (from departures in db.Flights
                            group departures by departures.PlannedDepairport into p
                            select new VatsimClientPlannedFlight
                            {
                                AirportName = p.Key,
                                Departures = p.Count()
                            }).OrderByDescending(x => x.Departures).FirstOrDefault();
                Console.WriteLine($"{query3.AirportName}, which has {query3.Departures} departures.");
                Console.WriteLine();

                // QUERY 4 STARTS HERE 
                Console.WriteLine("Query 4: Which airport has the most arrivals?");
                var query4 = (from arrivals in db.Flights
                            group arrivals by arrivals.PlannedDestairport into p
                            select new VatsimClientPlannedFlight
                            {
                                AirportName = p.Key,
                                Arrivals = p.Count()
                            }).OrderByDescending(x => x.Arrivals).FirstOrDefault();
                Console.WriteLine($"{query4.AirportName}, which has {query4.Arrivals} arrivals.");
                Console.WriteLine();

                // QUERY 5 STARTS HERE
                Console.WriteLine("Query 5: Who is flying at the highest altitude" +
                   " and what kind of plane are they flying?");
                var query5 = (from positions in db.Positions
                                join flights in db.Flights
                                on positions.Cid equals flights.Cid
                                select new VatsimClientPilotSnapshot
                                {
                                    AirCType = flights.PlannedAircraft,
                                    PilotName = positions.Realname,
                                    strAltitude = positions.Altitude
                                }).ToList();
                foreach (var item in query5)
                {
                    // Parse string to return integer
                   item.intAltitude = int.Parse(item.strAltitude);
                }
                var aircraft = query5.OrderByDescending(x => x.intAltitude).FirstOrDefault();
                Console.WriteLine($"{aircraft.PilotName} is flying at the highest altitude ({aircraft.intAltitude} ft) with the {aircraft.AirCType}.");
                Console.WriteLine();

                // QUERY 6 STARTS HERE
                Console.WriteLine("Query 6: Who is flying the slowest?" +
                   " (Hint: they can't be on the ground)");
                var query6 = (from positions in db.Positions
                                        select new VatsimClientPilotSnapshot
                                        {
                                            PilotName = positions.Realname,
                                            strGroundspeed = positions.Groundspeed
                                        }).ToList();
                foreach (var item in query6)
                {
                    // Parse string to return integer
                   item.intGroundspeed = int.Parse(item.strGroundspeed);
                }
                var slowest = query6.Where(x => x.intGroundspeed > 0).OrderBy(x => x.intGroundspeed).FirstOrDefault();
                Console.WriteLine($"{slowest.PilotName}, who is flying at {slowest.intGroundspeed} mph.");
                Console.WriteLine();
                
                // QUERY 7 STARTS HERE
                Console.WriteLine("Query 7: Which aircraft type is being used the most?");
                var query7 = (from aircraftType in db.Flights
                                group aircraftType by aircraftType.PlannedAircraft into p
                                select new VatsimClientPlannedFlight
                                {
                                    AirCType = p.Key,
                                    AirCTypeCount = p.Count()
                                }).OrderByDescending(x => x.AirCTypeCount).FirstOrDefault();
                Console.WriteLine($"{query7.AirCType}, which has been used {query7.AirCTypeCount} times.");
                Console.WriteLine();

                // QUERY 8 STARTS HERE
                Console.WriteLine("Query 8: Who is flying the fastest?");
                var query8 = (from positions in db.Positions
                                        select new VatsimClientPilotSnapshot
                                        {
                                            PilotName = positions.Realname,
                                            strGroundspeed = positions.Groundspeed
                                        }).ToList();
                foreach (var item in query8)
                {
                    // Parse string to return integer
                   item.intGroundspeed = int.Parse(item.strGroundspeed);
                }
                var fastest = query8.OrderByDescending(x => x.intGroundspeed).FirstOrDefault();
                Console.WriteLine($"{fastest.PilotName}, who is flying at {fastest.intGroundspeed} mph.");
                Console.WriteLine();

                // QUERY 9 STARTS HERE
                Console.WriteLine("Query 9: How many pilots are flying North (270 degrees to 90 degrees)?");                
                var query9 = db.Positions.Select(p => new VatsimClientPilotSnapshot { strHeading = p.Heading }).ToList();
                foreach (var item in query9) {
                    // Parse string to return integer
                    item.intHeading = int.Parse(item.strHeading);
                }
                var headingNorth = query9.Where(x => x.intHeading >= 90 && x.intHeading <= 270).Count();
                Console.WriteLine($"{headingNorth} pilots are flying North.");
                Console.WriteLine();

                // QUERY 10 STARTS HERE
                Console.WriteLine("Query 10: Which pilot has the longest remarks section of their flight?");                
                var query10 = db.Flights
                                .OrderByDescending(p => p.PlannedRemarks.Length)
                                .Select(p => new { p.Realname, p.PlannedRemarks.Length }).FirstOrDefault();
                Console.WriteLine($"{query10.Realname}, who has {query10.Length} characters in their remarks section.");
            }            
        }
    }
}
