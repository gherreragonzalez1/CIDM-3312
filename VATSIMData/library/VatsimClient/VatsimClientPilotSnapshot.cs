using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VatsimLibrary.VatsimClient
{
    public class VatsimClientPilotSnapshot : VatsimClient
    {
        public DateTime TimeStamp { get; set; }
        public string Altitude { get; set; }
        public string Groundspeed { get; set; }
        public string Transponder { get; set; }
        public string Heading { get; set; }
        public string QNH_iHg { get; set; }
        public string QNH_Mb { get; set; }

        //Extending Entity Framework Model to include new properties.
        [NotMapped]
        public string PilotName { get; set; }
        [NotMapped]
        public string AirCType { get; set; }
        [NotMapped]
        public int intAltitude { get; set; }
        [NotMapped]
        public string strAltitude { get; set; }
        [NotMapped]
        public int intGroundspeed { get; set; }
        [NotMapped]
        public string strGroundspeed { get; set; }
        [NotMapped] 
        public int intHeading { get; set; }
        [NotMapped]
        public string strHeading { get; set; }
        [NotMapped]
        public int PilotCount { get; set; }

        public override string ToString()
        {
            return $"{this.Cid} - {this.Callsign} - {this.Latitude} - {this.Longitude}";
        }


    }
}