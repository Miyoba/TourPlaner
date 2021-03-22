using System;
using System.Collections.Generic;
using System.Text;
using TourPlanner.Model;

namespace TourPlanner.DesignViewModel
{
    class DesignMainViewModel: MainViewModel
    {
        public DesignMainViewModel()
        {
            this.Name = "TestTour";
            this.Description = "TestDescription";
            this.RouteInformation = "TestInformation";
            this.Distance = 5;

            this.Data.Clear();
            this.Data.Add(new Tour("TestTour","TestTourDescription","TestTourInfo", 0));
            this.Data.Add(new Tour("TestTour2","TestTourDescription2","TestTourInfo2", 2));
            this.Data.Add(new Tour("TestTour3","TestTourDescription3","TestTourInfo3", 3333));
        }
    }
}
