namespace TourPlanner.BusinessLayer
{
    public static class TourFactory
    {
        private static ITourFactory _instance;

        public static ITourFactory GetInstance()
        {
            if (_instance == null)
            {
                _instance = new TourFactoryImpl();
            }

            return _instance;
        }

    }
}
