using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using VatsimLibrary.VatsimData;

namespace VatsimLibrary.VatsimClientV1
{
    public class VatsimClientPilotV1 : VatsimClientV1
    {
        //Extending Entity Framework Model to include new properties.
        [NotMapped]
        public DateTime dtTimeLogon { get; set; }
        
        [NotMapped]
        public double DateDiff { get; set; }
        public void Update(VatsimClientPilotV1 pilot)
        {
            this.Callsign = pilot.Callsign;
            this.Cid = pilot.Cid;
            this.Clienttype = pilot.Clienttype;
            this.Latitude = pilot.Latitude;
            this.Longitude = pilot.Longitude;
            this.Protrevision = pilot.Protrevision;
            this.Realname = pilot.Realname;
            this.Server = pilot.Server;
            this.TimeLastAtisReceived = pilot.TimeLastAtisReceived;
            this.TimeLogon = pilot.TimeLogon;
        }

        public override string ToString()
        {
            return $"{this.Cid} - {this.Callsign} - {this.Realname}";
        }
    }
}