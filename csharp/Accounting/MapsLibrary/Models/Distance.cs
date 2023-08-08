using GoogleMapsApi;
using GoogleMapsApi.Entities.Directions.Request;
using GoogleMapsApi.Entities.Directions.Response;
using System.Text.RegularExpressions;

namespace MapsLibrary.Models
{
    public class Distance
    {
        private string key = "AIzaSyAUlNSN4gw2Ja-Wuaz_EjePLm0z0Nx1tcc";
        DirectionsRequest directionsRequest = new DirectionsRequest();
        private Dictionary<string, List<Step>> zoneSteps = new Dictionary<string, List<Step>>();
        private double SubStepLength = 100.0; // m

        public Distance(string origin, string destination)
        {
            directionsRequest = new DirectionsRequest()
            {
                Origin = origin,
                Destination = destination,
                ApiKey = key,
                TravelMode = TravelMode.Driving,
            };

        }

        private void AddStepToZone(string zoneName, Step step)
        {
            if(zoneSteps.ContainsKey(zoneName))
            {
                zoneSteps[zoneName].Add(step);
            } else
            {
                var listStep = new List<Step>();
                listStep.Add(step);
                zoneSteps.Add(zoneName, listStep);
            }

        }

        public void ShowSteps()
        {
            foreach(KeyValuePair<string, List<Step>> stepsInZome in zoneSteps) {
                Console.WriteLine(stepsInZome.Key + ":");
                foreach(var step in stepsInZome.Value)
                {
                    showFullStep(step);
                }
            }
        }

        public async Task<IEnumerable<Step>> GetSteps()
        {
            IEnumerable<Step> steps = new List<Step>();
            try
            {
                DirectionsResponse directions = await GoogleMaps.Directions.QueryAsync(directionsRequest);
                steps = directions.Routes.First().Legs.First().Steps;
            
            } catch (Exception ex)
            {

            }

            return steps;
        }

        private bool IsStepInsidePolygon(Step step, Polygon polygon)
        {
            // The start is inside
            MyPoint point = new MyPoint(step.StartLocation.Latitude, step.StartLocation.Longitude);
            if(polygon.IsPointInsidePolygon(point))
            {
                AddStepToZone(polygon.Name, step);
            }

            // How far is the step inside
            point = new MyPoint(step.EndLocation.Latitude, step.EndLocation.Longitude);
            if(polygon.IsPointInsidePolygon(point)) // the whole step
            {
                AddStepToZone(polygon.Name, step);
                return true;
            } else // Some part
            {

            }

            return false;
        }

        public MyPoint NextSubStep(Step step)
        {
            var lat1 = step.StartLocation.Latitude;
            var lng1 = step.StartLocation.Longitude;
            var lat2 = step.EndLocation.Latitude;
            var lng2 = step.EndLocation.Longitude;

            var point1 = new MyPoint(lat1, lng1);
            var point2 = new MyPoint(lat2, lng2);

            var mid = MidMpoint(point1, point2);
            var distance = DistanceBetween(point1, mid);
            if((distance * 1000) > SubStepLength)
            {
                var newStep = new Step()
                {
                    StartLocation = step.StartLocation,
                    EndLocation = new GoogleMapsApi.Entities.Common.Location(mid.Lat, mid.Lng)
                };
                return NextSubStep(newStep);
            }
            return mid;
        }

        private bool IsAnyPointOnStepInsidePolygon(Step step, Polygon polygon)
        {
            return false;
        }

        public double DistanceBetween(MyPoint p1, MyPoint p2)
        {
            MyPoint _p1 = new MapsLibrary.Models.MyPoint(ToRadians(p1.Lng), ToRadians(p1.Lat));
            MyPoint _p2 = new MapsLibrary.Models.MyPoint(ToRadians(p2.Lng), ToRadians(p2.Lat));
            // Harvestine formula
            double dlng = _p2.Lng - _p1.Lng;
            double dlat = _p2.Lat - _p1.Lat;
            double a = Math.Pow(Math.Sin(dlat / 2), 2) +
                        Math.Cos(_p1.Lat) * Math.Cos(_p2.Lat) *
                        Math.Pow(Math.Sin(dlng / 2), 2);
            double c = 2 * Math.Asin(Math.Sqrt(a));
            // miles: 3958.8
            // or km
            //double r = 6371;
            double r = 3958.8;
            return (c * r); // meters
        }

        private double ToRadians(double x) { return (x * Math.PI / 180);  }
        private double ToDegrees(double x) { return (x * 180 / Math.PI); }
        public MyPoint MidMpoint(MyPoint p1, MyPoint p2)
        {
            double dlng = MyPoint.ToRadians(p2.Lng - p1.Lng);
            MyPoint _p1 = MyPoint.ToRadians(p1);
            MyPoint _p2 = MyPoint.ToRadians(p2);
            double Bx = Math.Cos(_p2.Lat) * Math.Cos(dlng);
            double By = Math.Cos(_p2.Lat) * Math.Sin(dlng);
            double lat3 = Math.Atan2(Math.Sin(_p1.Lat) + Math.Sin(_p2.Lat), Math.Sqrt((Math.Cos(_p1.Lat) + Bx) * (Math.Cos(_p1.Lat) + Bx) + By * By));
            double lng3 = _p1.Lng + Math.Atan2(By, Math.Cos(_p1.Lat) + Bx);

            return new MyPoint( ToDegrees(  lat3), ToDegrees( lng3));
        }

        public double CalculateDistance(IEnumerable<Step> steps, Polygon polygon)
        {
            if(steps == null)
            {
                throw new ArgumentNullException(nameof(steps));
            }
            if(polygon == null)
            {
                throw new ArgumentNullException(nameof(polygon));
            }
            double innerDistance = 0.0;
            int divider = 0;

            foreach (Step step in steps)
            {
                divider = 0;
                MyPoint point = new MyPoint(step.StartLocation.Latitude, step.StartLocation.Longitude);
                if(polygon.IsPointInsidePolygon(point))
                {
                    divider = 2;
                }
                point = new MyPoint(step.EndLocation.Latitude, step.EndLocation.Longitude);
                if (polygon.IsPointInsidePolygon(point))
                {
                    divider = 1;
                }

                if(divider > 0)
                {
                    innerDistance += step.Distance.Value;
                    Console.WriteLine($"IN - Distance:{step.Distance.Text} Time:{step.Duration.Text} Start:{step.StartLocation.Latitude},{step.StartLocation.Longitude} End:{step.EndLocation.Latitude},{step.EndLocation.Longitude} - { Regex.Replace( step.HtmlInstructions, "<.*?>", String.Empty )}");
                    showFullStep(step);
                } else
                {
                    Console.WriteLine($"OUT - Distance:{step.Distance.Text} Time:{step.Duration.Text} Start:{step.StartLocation.Latitude},{step.StartLocation.Longitude} End:{step.EndLocation.Latitude},{step.EndLocation.Longitude} - {Regex.Replace(step.HtmlInstructions, "<.*?>", String.Empty)}");
                    showFullStep(step);
                }

            }
            return innerDistance;
        }

        private void showFullStep(Step step)
        {
            Console.Write($"value: {step.Distance.Value}, text: {step.Distance.Text}");
            Console.WriteLine($"value: {step.Duration.Value}, text: {step.Duration.Text}");
        }


    }
}
