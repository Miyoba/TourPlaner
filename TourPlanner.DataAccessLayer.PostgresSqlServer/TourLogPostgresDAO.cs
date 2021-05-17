using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using TourPlanner.DataAccessLayer.Common;
using TourPlanner.DataAccessLayer.DAO;
using TourPlanner.Models;

namespace TourPlanner.DataAccessLayer.PostgresSqlServer {
    class TourLogPostgresDAO : ITourLogDAO {

        private const string SqlFindById = "SELECT * FROM public.\"TourLog\" WHERE \"Id\"=@Id;";
        private const string SqlFindByTour = "SELECT * FROM public.\"TourLog\" WHERE \"TourId\"=@TourId;";
        private const string SqlDeleteTourLog = "DELETE FROM public.\"TourLog\" WHERE \"Id\"=@Id;";
        private const string SqlInsertNewTourLog = "INSERT INTO public.\"TourLog\" (\"TourId\",\"DateTime\",\"Report\",\"Distance\",\"TotalTime\", \"Rating\") " +
                                                   "VALUES (@TourId, @DateTime, @Report, @Distance, @TotalTime, @Rating) " +
                                                   "RETURNING \"Id\";";

        private IDatabase _database;
        private ITourDAO _tourDAO;

        public TourLogPostgresDAO()
        {
            this._database = DALFactory.GetDatabase();
            this._tourDAO = DALFactory.CreateTourDAO();
        }

        public TourLogPostgresDAO(IDatabase database, ITourDAO tourDAO)
        {
            this._database = database;
            this._tourDAO = tourDAO;
        }

        public TourLog FindById(int tourLogId)
        {
            DbCommand findCommand = _database.CreateCommand(SqlFindById);
            _database.DefineParameter(findCommand, "@Id", DbType.Int32, tourLogId);

            IEnumerable<TourLog> logs = QueryTourLogsFromDatabase(findCommand);
            return logs.FirstOrDefault();
        }

        public TourLog AddNewTourLog(Tour tour, string dateTime, string report, int distance, string totalTime, int rating)
        {
            DbCommand insertCommand = _database.CreateCommand(SqlInsertNewTourLog);
            _database.DefineParameter(insertCommand, "@TourId", DbType.Int32, tour.Id);
            _database.DefineParameter(insertCommand, "@DateTime", DbType.String, dateTime);
            _database.DefineParameter(insertCommand, "@Report", DbType.String, report);
            _database.DefineParameter(insertCommand, "@Distance", DbType.Int32, distance);
            _database.DefineParameter(insertCommand, "@TotalTime", DbType.String, totalTime);
            _database.DefineParameter(insertCommand, "@Rating", DbType.Int32, rating);

            return FindById(_database.ExecuteScalar(insertCommand));
        }

        public void DeleteTourLog(TourLog tourLog)
        {
            DbCommand deleteCommand = _database.CreateCommand(SqlDeleteTourLog);
            _database.DefineParameter(deleteCommand, "@Id", DbType.Int32, tourLog.Id);

            _database.ExecuteScalar(deleteCommand);
        }

        public TourLog EditTourLog(TourLog tourLog, string dateTime, string report, int distance, string totalTime, int rating)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TourLog> GetTourLogs(Tour tour)
        {
            return QueryTourLogsFromTour(tour);
        }

        private IEnumerable<TourLog> QueryTourLogsFromTour(Tour tour)
        {
            DbCommand getLogsCommand = _database.CreateCommand(SqlFindByTour);
            _database.DefineParameter(getLogsCommand, "@TourId", DbType.Int32, tour.Id);

            return QueryTourLogsFromDatabase(getLogsCommand);
        }

        private IEnumerable<TourLog> QueryTourLogsFromDatabase(DbCommand command)
        {
            List<TourLog> tourLogs = new List<TourLog>();

            using (IDataReader reader = _database.ExecuteReader(command))
            {
                while (reader.Read())
                {
                    tourLogs.Add(new TourLog(
                        (int)reader["Id"],
                        _tourDAO.FindById((int)reader["TourId"]),
                        (string)reader["DateTime"],
                        (string)reader["Report"],
                        (int)reader["Distance"],
                        (string)reader["TotalTime"],
                        (int)reader["Rating"]));
                }
            }

            return tourLogs;
        }
    }
}
