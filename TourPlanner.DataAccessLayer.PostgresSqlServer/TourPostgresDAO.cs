﻿using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using TourPlanner.DataAccessLayer.Common;
using TourPlanner.DataAccessLayer.DAO;
using TourPlanner.Models;

namespace TourPlanner.DataAccessLayer.PostgresSqlServer {
    public class TourPostgresDAO : ITourDAO
    {
        private const string SqlFindById = "SELECT * FROM public.\"Tour\" WHERE \"Id\"=@Id;";

        private const string SqlGetAllTours = "SELECT * FROM public.\"Tour\";";

        private const string SqlDeleteTour = "DELETE FROM public.\"Tour\" WHERE \"Id\"=@Id;";

        private const string SqlEditTour = "UPDATE public.\"Tour\" SET " +
                                           "\"ToLocation\"=@ToLocation, \"Name\"=@Name, \"FromLocation\"=@FromLocation, " +
                                           "\"Description\"=@Descripton, \"Distance\"=@Distance " +
                                           "WHERE \"Id\"=@Id RETURNING \"Id\";";

        private const string SqlInsertNewTour = "INSERT INTO public.\"Tour\" (\"Name\",\"FromLocation\",\"ToLocation\",\"Description\",\"Distance\") " +
                                                "VALUES (@Name, @FromLocation, @ToLocation, @Description, @Distance) " +
                                                "RETURNING \"Id\";";


        private IDatabase _database;

        public TourPostgresDAO()
        {
            this._database = DALFactory.GetDatabase();
        }

        public TourPostgresDAO(IDatabase database)
        {
            this._database = database;
        }

        public Tour FindById(int tourId)
        {
            DbCommand findCommand = _database.CreateCommand(SqlFindById);
            _database.DefineParameter(findCommand, "@Id", DbType.Int32, tourId);

            IEnumerable<Tour> tours = QueryToursFromDatabase(findCommand);
            return tours.FirstOrDefault();
        }

        public void AddNewTour(string tourName, string tourFromLocation, string tourToLocation, string tourDescription, int tourDistance)
        {
            DbCommand insertCommand = _database.CreateCommand(SqlInsertNewTour);
            _database.DefineParameter(insertCommand, "@ToLocation", DbType.String, tourToLocation);
            _database.DefineParameter(insertCommand, "@Name", DbType.String, tourName);
            _database.DefineParameter(insertCommand, "@FromLocation", DbType.String, tourFromLocation);
            _database.DefineParameter(insertCommand, "@Description", DbType.String, tourDescription);
            _database.DefineParameter(insertCommand, "@Distance", DbType.Int32, tourDistance);

            _database.ExecuteScalar(insertCommand);
        }

        public void DeleteTour(Tour tour)
        {
            DbCommand deleteCommand = _database.CreateCommand(SqlDeleteTour);
            _database.DefineParameter(deleteCommand, "@Id", DbType.Int32, tour.Id);

            _database.ExecuteScalar(deleteCommand);
        }

        public void EditTour(Tour tour, string tourName, string tourFromLocation, string tourToLocation, string tourDescription,
            int tourDistance)
        {
            DbCommand editCommand = _database.CreateCommand(SqlEditTour);
            _database.DefineParameter(editCommand, "@Name", DbType.String, tourName);
            _database.DefineParameter(editCommand, "@FromLocation", DbType.String, tourFromLocation);
            _database.DefineParameter(editCommand, "@ToLocation", DbType.String, tourToLocation);
            _database.DefineParameter(editCommand, "@Description", DbType.String, tourDescription);
            _database.DefineParameter(editCommand, "@Distance", DbType.Int32, tourDistance);
            _database.DefineParameter(editCommand, "@Id", DbType.Int32, tour.Id);

            _database.ExecuteScalar(editCommand);
        }

        public IEnumerable<Tour> GetTours()
        {
            DbCommand tourCommand = _database.CreateCommand(SqlGetAllTours);

            return QueryToursFromDatabase(tourCommand);
        }

        private IEnumerable<Tour> QueryToursFromDatabase(DbCommand command)
        {
            List<Tour> tours = new List<Tour>();

            using (IDataReader reader = _database.ExecuteReader(command))
            {
                while (reader.Read())
                {
                    tours.Add(new Tour(
                        (int)reader["Id"],
                        (string)reader["Name"],
                        (string)reader["Description"],
                        (string)reader["FromLocation"],
                        (string)reader["ToLocation"],
                        (int)reader["Distance"]));
                }
            }

            return tours;
        }
    }
}
