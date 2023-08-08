using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text;
using ConfigLibrary.Models;
using ConfigLibrary;

namespace ConfigLibrary;

public class VehicleConfig
{
    private readonly string jsonPath = string.Empty;
    public List<RegionVehicle> regions = new List<RegionVehicle>();
    private List<Vehicle> vehicles = new List<Vehicle>();


    public VehicleConfig()
    {
        try
        {
            var dataDir = Config.GetDataDir(); 
            jsonPath = Path.Combine(dataDir, "Config", "vehicles.json");

        } catch(Exception ex)
        {
            var msg = ex.Message;
        }
        
    }

    public List<RegionVehicle> GetRegionVehicles()
    {
        ReadJson();
        return regions;
    }

    private void ReadJson()
    {
        try
        {
            dynamic jObject = JsonConvert.DeserializeObject(File.ReadAllText(jsonPath));
            JToken _regions = jObject.SelectToken("regions");
            foreach(var _region in _regions)
            {
                RegionVehicle region = _region.ToObject<RegionVehicle>();
                regions.Add(region);
            }

            var _vehicles = jObject.SelectToken("vehicles");
            foreach(var _vehicle in _vehicles)
            {
                var vehicle = _vehicle.ToObject<Vehicle>();
                vehicles.Add(vehicle);
            }

            // Assign the vehicles to regions as per json Reegion.Rates object
            AssignVehiclesToRegions();
            
        } catch (Exception ex)
        {
            var msg = ex.Message;
        }
    }

    private void AssignVehiclesToRegions()
    {
        foreach(var region in regions)
        {
            foreach(var item in region.Rates)
            {
                Vehicle vehicle = GetVehicleByName(item.Key);
                vehicle.RegionRate = item.Value;
                region.Vehicles.Add(vehicle);
            }
        }
    }

    private Vehicle GetVehicleByName(string Name)
    {
        if (string.IsNullOrWhiteSpace(Name))
            return new Vehicle();

        var v = vehicles.Find(x => x.Name == Name);
        return new Vehicle()
        {
            Name = v.Name,
            Model = v.Model,
            Image = v.Image,
            BaseRate = v.BaseRate,
            Passengers = v.Passengers,
            RegionRate = 0.0
        };
    }
}

