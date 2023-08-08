using GoogleMapsApi.Entities.Directions.Response;


namespace MapsLibrary.Models
{
    public class UserTrip
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Created { get; set; } = DateTime.Now;
        public MyPoint Origin { get; set; } = new MyPoint();
        public MyPoint Destination { get; set; } = new MyPoint();
        public List<Region> Regions { get; set; } = new List<Region>();
        public List<MyPoint> coloredPoints = new();


        public UserTrip(string area)
        {
            Dictionary<string, FileStream> areaPolygonStreams = Area.ReadAllZones(area);
            foreach (var areaPolygonStream in areaPolygonStreams)
            {
                Region r = new Region(areaPolygonStream.Key, areaPolygonStream.Value);
                Regions.Add(r);
            }

            Region whiteRegion = new Region("White-Region");
            Regions.Add(whiteRegion);
    
        }

        public void AddOrigin(double lat, double lng)
        {
            MyPoint point = new MyPoint()
            {
                Lat = lat,
                Lng = lng
            };
            Origin = point;
        }

        public void AddDestination(double lat, double lng)
        {
            MyPoint point = new MyPoint()
            {
                Lat = lat,
                Lng = lng
            };
            Destination = point;
        }

        public async Task<List<MyPoint>> FindRoute()
        {
            var googleApi = new Google(Origin.ToString(), Destination.ToString());
            List<Step> routeSteps = await googleApi.GetSteps();
            MakeStepsRegionWise(routeSteps);
            return coloredPoints;
        }

        private void MakeStepsRegionWise(List<Step> routeSteps)
        {
            foreach (var step in routeSteps)
            {
                OverviewPolyline polylines = step.PolyLine;
                double distancePart = step.Distance.Value  / (double)step.PolyLine.Points.ToArray().Length;
                foreach(var loc in polylines.Points)
                {
                    AssignRegion(loc, distancePart);
                }
            }
        }

        private bool AssignRegion(GoogleMapsApi.Entities.Common.Location point, double distancePart)
        {
            MyPoint _point = new MyPoint(point.Latitude, point.Longitude);
            _point.DistancePart = distancePart;

            foreach(var region in  Regions)
            {
                if(region.IsPointInsideRegion(_point))
                {
                    _point.Color = region.BgColor;
                    region.InsidePoints.Add(_point);
                    coloredPoints.Add(_point);
                    return true;
                }
            }

            Region whiteRegion = Regions.FirstOrDefault(x => x.Name == "White-Region");
            if (whiteRegion != null)
            {
                _point.Color = whiteRegion.BgColor;
                whiteRegion.InsidePoints.Add(_point);
                coloredPoints.Add(_point);
            }
            return false;
        }

        public List<RegionRoute> GetRegionRoutes()
        {
            List<RegionRoute> RegionStats = new();
            foreach(var region in Regions)
            {
                RegionStats.Add(region.GetRegionStats());
            }
            double totolDistance = RegionStats.Aggregate(0.0, (acc, x) => acc + x.RegionDistance);
            foreach(var regionMarker in RegionStats)
            {
                int pDistance = Convert.ToInt32(regionMarker.RegionDistance / totolDistance * 100);
                regionMarker.RegionPercentileDistance = pDistance;
                if(regionMarker.RegionDistance < 1000.0)
                {
                    regionMarker.RegionDistanceString = String.Format("{0:.#} m", regionMarker.RegionDistance);

                } else
                {
                    regionMarker.RegionDistanceString = String.Format("{0:.#} km", regionMarker.RegionDistance / 1000);

                }
            }
            return RegionStats.Where(x => x.RegionDistance > 0.0).ToList().OrderByDescending(x => x.RegionDistance).ToList();
        }
    }
}
