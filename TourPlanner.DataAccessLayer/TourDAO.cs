﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;

namespace TourPlanner.DataAccessLayer
{
    public class TourDAO {

        private IDataAccess _dataAccess;

        public TourDAO() {
            // check which datasource to use
            _dataAccess = new Database();
        }

        public List<Tour> GetItems() {
            return _dataAccess.GetTours();
        }
    }
}