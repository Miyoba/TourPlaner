﻿using System.Collections.Generic;
using TourPlanner.Models;

namespace TourPlanner.DataAccessLayer
{
    interface IDataAccess
    {
        public List<Tour> GetTours();
        public List<TourLog> GetTourLogs(Tour tour);
    }
}
