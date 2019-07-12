using Microsoft.VisualStudio.TestTools.UnitTesting;
using JollazApiQueries.Library.Extensions;
using System.Linq;
using System;
using System.Linq.Dynamic.Core;
using JollazApiQueries.Model.Core;

namespace JollazApiQueries.Tests.Filtering
{
    [TestClass]
    public class EnumFilterTests
    {
        [TestMethod]
        public void TestIfQueryIsFilteredByEnumEquals()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.Filters = new FilterItem[]
            {
                new FilterItem
                {
                    Name = "Gender",
                    Criterion = FilterCriterion.Equal,
                    Parameter = 1
                }
            };
            var query = Person.GetPersonQuery();

            var newQuery = query.FilterByDataRequest(dataRequest);
            
            Assert.AreEqual(2, newQuery.Count());
        }

        [TestMethod]
        public void TestIfQueryFilteredByEnumUnsupportedCriterionThrowsException()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.Filters = new FilterItem[]
            {
                new FilterItem
                {
                    Name = "Gender",
                    Criterion = FilterCriterion.StringContains,
                    Parameter = 0
                }
            };
            var query = Person.GetPersonQuery();

            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                var newQuery = query.FilterByDataRequest(dataRequest);
            });
        }
    }
}
