namespace MapsLibrary.Models
{
    public class Color
	{
		public List<string> Colors { get; set; } = new List<string>();
		private int index = 0;
		private static Color instance;

		public Color()
		{
			Colors = new List<string>()
			{
                "#0000FF",
                "#FF00FF",
                "#008080",
                "#FF0000",
                "#800000",
                "#40E0D0",
                "#000080",
                "#CCCCFF",
			};
        }

		public static string GetNextColor()
		{
			if (instance == null)
				instance = new Color();


            if (instance.index > instance.Colors.Count() - 1)
			{
				instance.index = 0;
			}
            var el = instance.Colors[instance.index++];

            return el;
		}
	}
}

