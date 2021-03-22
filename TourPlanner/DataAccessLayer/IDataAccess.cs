using System;
using System.Collections.Generic;
using System.Text;
using TourPlanner.Model;

namespace TourPlanner.DataAccessLayer
{
    interface IDataAccess
    {
        public List<Tour> GetTours();
    }
}
