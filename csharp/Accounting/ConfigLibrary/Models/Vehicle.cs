namespace ConfigLibrary.Models
{
	public class Vehicle : IEquatable<Vehicle>
    {
		public string Name { get; set; } = string.Empty;
		public string Model { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
		public double BaseRate { get; set; }
		public int Passengers { get; set; }
		public double RegionRate { get; set; }
        public double SubTotalRent { get; set; } // this.baseRate  + relevant region(RegionRoute.regionDistance * this.regionRate/1000)
        public double TotalRent { get; set; }

        public bool Equals(Vehicle? other)
        {
            if (other == null)
                return false;
            return Name == other.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public double ComputeSubTotalRent(double regionDistance)
        {
            return SubTotalRent =  Math.Round((RegionRate / 1000 * regionDistance), 2); 
        }
    }
}

