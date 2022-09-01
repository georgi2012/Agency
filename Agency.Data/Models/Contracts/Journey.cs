using Agency.Data.DB;
using Agency.Data.Models.Vehicles.Contracts;
using Agency.Data.Models.Vehicles.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Agency.Data.Models.Contracts
{
    public class Journey : IJourney
    {
        //private readonly AgencyDBContext _dbcontext;
        //private readonly Guid ID;
        [Key]
       // [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public virtual Guid JourneyID { get; set; }
        public virtual string Destination { get;  set; }
        public virtual string StartLocation { get;  set; }
        public virtual int Distance { get;  set; }



        //Navigation prroperties
       
        public virtual int VehicleID { get;  set; }
       // [NotMapped]
        public virtual Vehicle Vehicle { get; set; }

        public Journey()
        { }

        public Journey(string dest, int dist, string startLoc, Vehicle vehicle)
        {
            StartLocation = startLoc;
            Destination = dest;
            Distance = dist;
            VehicleID = vehicle.VehicleID;
            Vehicle = vehicle;
        }


       
        public decimal CalculateTravelCosts()
        {
            return Vehicle.PricePerKilometer * Distance;
        }

        public override string ToString()
        {
            return "Journey ----\n" +
                   $"Start location: {StartLocation}\n" +
                   $"Destination: {Destination}\n" +
                   $"Distance: {Distance}\n" +
                   $"Vehicle type: {Vehicle.Type}\n" +
                   $"Travel costs: {CalculateTravelCosts()}";
        }
    }
}
