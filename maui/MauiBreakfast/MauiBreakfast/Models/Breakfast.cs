using System;


namespace MauiBreakfast.Models
{
	public class Breakfast
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime StartDateTime { get; set; }
		public DateTime EndDateTime { get; set; }
		public Uri Image { get; set; }
		public List<string> Savory { get; set; }
		public List<string> Sweet { get; set; }
	}
}

