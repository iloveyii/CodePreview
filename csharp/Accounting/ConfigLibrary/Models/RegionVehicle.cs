using System.Text.Json.Serialization;


namespace ConfigLibrary.Models
{
	public class RegionVehicle
	{
		public string Id { get; set; } = string.Empty;
		[JsonIgnore]
		public Dictionary<string, double> Rates { get; set; } = new Dictionary<string, double>();
		public List<Vehicle> Vehicles { get; set; } = new List<Vehicle>();

		public Vehicle? GetVehicleByName(string name)
		{
			var v = Vehicles.Find(x => x.Name == name);
			if (v == null)
				return null;
			return v;

		}

		public List<Vehicle> GetVehiclesByNames(List<Vehicle> vehicles, double regionRouteDistance = 0.0)
		{
            List<Vehicle> _vehicles = new List<Vehicle>();
			foreach(var vehicle in vehicles)
			{
				var v = GetVehicleByName(vehicle.Name);
				if (v != null)
				{
					v.ComputeSubTotalRent(regionRouteDistance);
					_vehicles.Add(v);
				}
			}
			return _vehicles;
        }

		public List<Vehicle> GetVehiclesByNamesWithTotalRent(List<Vehicle> vehicles, double regionRouteDistance)
		{
            List<Vehicle> _vehicles = GetVehiclesByNames(vehicles, regionRouteDistance);
            return _vehicles;
        }
    }
}

