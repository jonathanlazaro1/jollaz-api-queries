using JollazApiQueries.Model.Core;

namespace JollazApiQueries.Tests
{
    public class TestCommons
    {
        public static DataRequest CreateDataRequest()
        {
            var dataRequest = new DataRequest
            {
                ItemsPerPage = 10,
                CurrentPage = 1,
            };
            return dataRequest;
        }
    }
}