using ConfigLibrary.Models;


namespace MapsLibrary.Models
{
	public class Result
	{
		public List<MyPoint> ColorPoints { get; set; } = new List<MyPoint>();
		// Route from origin to destination with regions on way - plus distance in each region
		public List<RegionRoute> RegionRoutes { get; set; } = new List<RegionRoute>();
		// Region id and list of vehicles working in this region
		public List<RegionVehicle> RegionVehicles { get; set; } = new List<RegionVehicle>();
    }
}

