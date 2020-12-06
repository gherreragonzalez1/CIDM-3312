using System;
using System.Linq;
using System.Collections.Generic;

using VatsimLibrary.VatsimDb;

namespace hw
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine($"{VatsimDbHelper.DATA_DIR}");

            using(var db = new VatsimDbContext())
            {

                Console.WriteLine($"The number of pilots records is: {db.Pilots.Count()} ");

                //find A319
                var _aircraft = db.Flights.Where(f => f.PlannedAircraft.Contains("A319"));
                Console.WriteLine($"It is likely that there are {_aircraft.Count()} A319s in the data");

                _aircraft = db.Flights.Where(f => f.PlannedAircraft.Contains("B738"));
                Console.WriteLine($"It is likely that there are {_aircraft.Count()} B738s in the data");

                var _depList = db.Flights.ToList();

                //departure most
                 var _dep = _depList.GroupBy(f => f.PlannedDepairport).OrderByDescending(g => g.Count());

                Console.WriteLine($"{_dep.ElementAt(0).Key} - {_dep.ElementAt(0).Count()}");

                // foreach(var flight in _dep)
                // {
                //     Console.WriteLine($"{flight.Key} - {flight.Count()}");
                // }
                
                Console.WriteLine();
                // QUERY 1 STARTS HERE
                Console.WriteLine("Query 1: Which pilot has been logged on the longest?");
                var lstPilots = db.Pilots.ToList();

                foreach (var _pilot in lstPilots)
                {
                   // Convert TimeLogon in lstPilots to DateTime
                   string year = _pilot.TimeLogon.Substring(0, 4);
                   string month = _pilot.TimeLogon.Substring(4, 2);
                   string day = _pilot.TimeLogon.Substring(6, 2);
                   string hours = _pilot.TimeLogon.Substring(8, 2);
                   string minutes = _pilot.TimeLogon.Substring(10, 2);
                   string seconds = _pilot.TimeLogon.Substring(12, 2);
                   DateTime dtTimeLogon = DateTime.Parse(year+"-"+month+"-"+day+" " + hours+":"+minutes+":"+seconds);

                   // Get the current time and store it in dtNow
                   DateTime dtNow = DateTime.Now;

                   // Extended the EF model inside library/VatsimClient/V1/VatsimClientPilotV1.cs to include double PilotDateDiff
                   _pilot.PilotDateDiff = (dtNow - dtTimeLogon).TotalMinutes;
                }

                var pilotMaxDiff = lstPilots.Max(x => x.PilotDateDiff);
                var loggedPilot = lstPilots.Where(x => x.PilotDateDiff == pilotMaxDiff).FirstOrDefault();
                Console.WriteLine($"{loggedPilot.Realname}, who has been logged on for {pilotMaxDiff:0.00} minutes.");
                Console.WriteLine();

                // QUERY 2 STARTS HERE
                Console.WriteLine("Query 2: Which controller has been logged on the longest?");
                var lstControllers = db.Controllers.ToList();

                foreach (var _controller in lstControllers)
                {
                   // Convert TimeLogon in lstControllers to DateTime
                   string year = _controller.TimeLogon.Substring(0, 4);
                   string month = _controller.TimeLogon.Substring(4, 2);
                   string day = _controller.TimeLogon.Substring(6, 2);
                   string hours = _controller.TimeLogon.Substring(8, 2);
                   string minutes = _controller.TimeLogon.Substring(10, 2);
                   string seconds = _controller.TimeLogon.Substring(12, 2);
                   DateTime dtTimeLogon = DateTime.Parse(year+"-"+month+"-"+day+" " + hours+":"+minutes+":"+seconds);

                   // Get DateDiff from Now
                   DateTime dtNow = DateTime.Now;

                   // Extended the EF model inside library/VatsimClient/V1/VatsimClientATCV1.cs to include double ATCDateDiff
                   _controller.ATCDateDiff = (dtNow - dtTimeLogon).TotalMinutes;
                }

                var controllerMaxDiff = lstControllers.Max(x => x.ATCDateDiff);
                var loggedController = lstControllers.Where(x => x.ATCDateDiff == controllerMaxDiff).FirstOrDefault();
                Console.WriteLine($"{loggedController.Realname}, who has been logged on for {controllerMaxDiff:0.00} minutes.");
                Console.WriteLine();

                // QUERY 3 STARTS HERE
                Console.WriteLine("Query 3: Which airport has the most departures?");
                var query3 = (from departures in db.Flights
                            group departures by departures.PlannedDepairport into p
                            select new 
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
                            select new
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
                                select new
                                {
                                    AirCType = flights.PlannedAircraft,
                                    PilotName = positions.Realname,
                                    Altitude = int.Parse(positions.Altitude)
                                }).ToList();

                var aircraft = query5.OrderByDescending(x => x.Altitude).FirstOrDefault();
                Console.WriteLine($"{aircraft.PilotName} is flying at the highest altitude ({aircraft.Altitude} ft) with the {aircraft.AirCType}.");
                Console.WriteLine();

                // QUERY 6 STARTS HERE
                Console.WriteLine("Query 6: Who is flying the slowest?" +
                   " (Hint: they can't be on the ground)");
                var query6 = (from positions in db.Positions
                                        select new
                                        {
                                            PilotName = positions.Realname,
                                            Groundspeed = int.Parse(positions.Groundspeed)
                                        }).ToList();
                var slowest = query6.Where(x => x.Groundspeed > 0).OrderBy(x => x.Groundspeed).FirstOrDefault();
                Console.WriteLine($"{slowest.PilotName}, who is flying at {slowest.Groundspeed} mph.");
                Console.WriteLine();
                
                // QUERY 7 STARTS HERE
                Console.WriteLine("Query 7: Which aircraft type is being used the most?");
                var query7 = (from aircraftType in db.Flights
                                group aircraftType by aircraftType.PlannedAircraft into p
                                select new
                                {
                                    AirCType = p.Key,
                                    AirCTypeCount = p.Count()
                                }).OrderByDescending(x => x.AirCTypeCount).FirstOrDefault();
                Console.WriteLine($"{query7.AirCType}, which has been used {query7.AirCTypeCount} times.");
                Console.WriteLine();

                // QUERY 8 STARTS HERE
                Console.WriteLine("Query 8: Who is flying the fastest?");
                var query8 = (from positions in db.Positions
                                        select new
                                        {
                                            PilotName = positions.Realname,
                                            Groundspeed = int.Parse(positions.Groundspeed)
                                        }).ToList();
                var fastest = query8.OrderByDescending(x => x.Groundspeed).FirstOrDefault();
                Console.WriteLine($"{fastest.PilotName}, who is flying at {fastest.Groundspeed} mph.");
                Console.WriteLine();

                // QUERY 9 STARTS HERE
                Console.WriteLine("Query 9: How many pilots are flying North (270 degrees to 90 degrees)?");                
                var query9 = db.Positions.Select(p => new { Heading = int.Parse(p.Heading) }).ToList();
                var headingNorth = query9.Where(x => x.Heading >= 90 && x.Heading <= 270).Count();
                Console.WriteLine($"{headingNorth} pilots are flying North.");
                Console.WriteLine();

                // QUERY 10 STARTS HERE
                // Console.WriteLine("Query 10: Which pilot has the longest remarks section of their flight?");                
                // var query10 = db.Flights
                //                 .OrderByDescending(p => p.PlannedRemarks.Length)
                //                 .Select(p => new { p.Realname, p.PlannedRemarks.Length }).FirstOrDefault();
                // Console.WriteLine($"{query10.Realname}, who has {query10.Length} characters in their remarks section.");

                QueryTen(db);

                // FindPilot(db, "1031301", "NFC20", "20201203043809");
            }            
        }

        public static void QueryTen(VatsimDbContext db) {
            Console.WriteLine("Query 10: Which pilot has the longest remarks section of their flight?");
            var query10 = db.Flights
                            .OrderByDescending(p => p.PlannedRemarks.Length)
                            .Select(p => new { p.Realname, p.PlannedRemarks.Length })
                            .FirstOrDefault();
            Console.WriteLine($"{query10.Realname}, who has {query10.Length} characters in their remarks section.");
        }

        public static void FindPilot(VatsimDbContext db, string cid, string callsign, string timelogon)
        {
            var _pilot = db.Pilots.Find(cid, callsign, timelogon);
            if(_pilot != null){
                Console.WriteLine($"Pilot found: {_pilot.Realname}");
            } else {
                Console.WriteLine("Pilot not found");
            }            
        }
    }
}
