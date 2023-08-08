using GoogleMapsApi.Entities.Directions.Response;

namespace MapsLibrary.Models
{
    public class StepWise
    {
        private double SubStepLength = 100.0; // m
        public List<Step> subSteps = new List<Step>();
        private Step step;
        public StepWise(Step step) 
        {
            this.step = step;
            MyPoint startPoint = new MyPoint(step.StartLocation.Latitude, step.StartLocation.Longitude);
            double stepLength = 1000 *  startPoint.DistanceBetween(new MyPoint(step.EndLocation.Latitude, step.EndLocation.Longitude));
            int totalSubSteps = TotalSubsteps(stepLength);
            Step previousStep = step;
            for(int i = 0; i < totalSubSteps; i++)
            {
                MyPoint point = NextSubStepEndPoint(previousStep);
                Step subStep = CreateSubStep(previousStep, point);
                subSteps.Add(subStep);
                previousStep.StartLocation = subStep.EndLocation;
                previousStep.EndLocation = step.EndLocation;
            }
        
        }

        private int TotalSubsteps(double stepLength)
        {
            int count = Convert.ToInt32( stepLength / SubStepLength );

            if((stepLength / SubStepLength) > 0)
            {
                count++;
            }
            return count + 5;
        }

        private void AddStep(Step step) { }
        public Step CreateSubStep(Step step, MyPoint point) 
        {
            Step subStep = new Step()
            {
                StartLocation = step.StartLocation,
                EndLocation = new GoogleMapsApi.Entities.Common.Location(point.Lat, point.Lng),
            };
            return subStep;
        }

        public MyPoint NextSubStepEndPoint(Step step)
        {
            var lat1 = step.StartLocation.Latitude;
            var lng1 = step.StartLocation.Longitude;
            var lat2 = step.EndLocation.Latitude;
            var lng2 = step.EndLocation.Longitude;

            var point1 = new MyPoint(lat1, lng1);
            var point2 = new MyPoint(lat2, lng2);

            var mid = point1.MidPoint(point2);
            var distance = point1.DistanceBetween(mid);

            if ((distance * 1000) > SubStepLength)
            {
                var newStep = new Step()
                {
                    StartLocation = step.StartLocation,
                    EndLocation = new GoogleMapsApi.Entities.Common.Location(mid.Lat, mid.Lng)
                };
                return NextSubStepEndPoint(newStep);
            }
            return mid;
        }

        public void Show()
        {
            foreach (var step in subSteps)
            {
                Console.WriteLine($"{step.StartLocation.Latitude},{step.StartLocation.Longitude}");
            }
        }
    }
}
