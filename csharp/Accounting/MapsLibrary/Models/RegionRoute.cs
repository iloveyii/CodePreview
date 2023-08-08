namespace MapsLibrary.Models
{
	public class RegionRoute 
	{
        public string RegionName { get; set; }
		public string RegionColor { get; set; }
		public double RegionDistance { get; set; }
		public int RegionPercentileDistance { get; set; }
		public string RegionDistanceString { get; set; } = string.Empty;

        public RegionRoute(string regionName, string regionColor, double regionDistance)
		{
			RegionName = regionName;
			RegionColor = regionColor;
			RegionDistance = regionDistance;
		}

    }
}

