using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VatsimLibrary.VatsimClientV1;
using VatsimLibrary.VatsimDb;

namespace VATSIMData.WebApp.Pages {
    public class PilotDetailModel : PageModel {
        private VatsimDbContext db;

        public VatsimClientPilotV1 Pilot { get; set; }

        public IEnumerable<VatsimClientPilotSnapshotV1> Positions { get; set; }

        public IEnumerable<VatsimClientPlannedFlightV1> Flights { get; set; }

        public string Fastest { get; set; }

        public string Highest { get; set; }

        public List<string> Easternmost { get; set; }

        public List<string> Southernmost { get; set; }

        public PilotDetailModel(VatsimDbContext db) {
            this.db = db;
        }

        public async Task<IActionResult> OnGetAsync(string cid, string callsign, string timelogon) {
            Pilot = await db.Pilots.FindAsync(cid, callsign, timelogon);
            if(Pilot == null) {
                return RedirectToPage("NotFound");
            }

            // ***FIRST TABLE***
            // Calling the Positions table, where the Positions Callsign matches the requested callsign
            // and the Positions TimeLogon matches the requested timelogon
            Positions = await db.Positions.Where(x => x.Callsign == (callsign) && x.TimeLogon == (timelogon)).ToListAsync();
            // Calling the Flights table, used for the Airport query that is no longer required
            Flights = await db.Flights.Where(x => x.Callsign == (callsign) && x.TimeLogon == (timelogon)).ToListAsync();

            // ***SECOND TABLE***
            // Fastest groundspeed, Highest altitude, Eaternmost position and Southernmost position queries
            Fastest = Positions.OrderByDescending(x => int.Parse(x.Groundspeed)).Select(x => x.Groundspeed).FirstOrDefault();
            Highest = Positions.OrderByDescending(x => int.Parse(x.Altitude)).Select(x => x.Altitude).FirstOrDefault();
            Easternmost = Positions.Where(x => double.Parse(x.Longitude) <= 180).OrderByDescending(x => double.Parse(x.Longitude)).Select(x => new List<string> { string.Format($"({x.Latitude}, {x.Longitude})") }).FirstOrDefault();
            Southernmost = Positions.Where(x => double.Parse(x.Latitude) >= -90).OrderBy(x => double.Parse(x.Latitude)).Select(x => new List<string> { string.Format($"({x.Latitude}, {x.Longitude})") }).FirstOrDefault();

            // Alternatively, we can do the queries using Razor Code Blocks (Freeman, Ch. 21).
            // First, we call the tables. Second, we do the queries inside a Razor Code Block in Detail.cshtml
                // Positions = db.Positions.ToList();
                // Flights = db.Flights.ToList();

            return Page();
        }
    }
}