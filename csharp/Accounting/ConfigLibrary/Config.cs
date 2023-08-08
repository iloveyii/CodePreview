using System;
namespace ConfigLibrary
{
	public class Config
	{
		public static string GetDataDir()
		{
            var cd = Directory.GetCurrentDirectory();
            var parent = Directory.GetParent(cd);
            if (parent == null)
                throw new FileNotFoundException();
            var dataDir = Environment.GetEnvironmentVariable("DATADIR");
            if (string.IsNullOrWhiteSpace(dataDir))
                dataDir = Path.Combine(parent.FullName, "Data");
            return dataDir;
        }
	}
}

