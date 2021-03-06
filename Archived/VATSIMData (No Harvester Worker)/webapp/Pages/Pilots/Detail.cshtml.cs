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

        public IEnumerable<VatsimClientPilotSnapshotV1> Position { get; set; }

        public IEnumerable<VatsimClientPlannedFlightV1> Flight { get; set; }

        // public int intGroundspeed { get; set; }

        public PilotDetailModel(VatsimDbContext db) {
            this.db = db;
        }

        public async Task<IActionResult> OnGetAsync(string cid, string callsign, string timelogon) {
            Pilot = await db.Pilots.FindAsync(cid, callsign, timelogon);
            if(Pilot == null) {
                return RedirectToPage("NotFound");
            }

            Position = db.Positions.ToList();

            Flight = db.Flights.ToList();

            return Page();
        }
    }
}