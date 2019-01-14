using Microsoft.VisualStudio.TestTools.UnitTesting;
using JollazApiQueries.Library.Extensions;
using System.Linq;
using System;
using JollazApiQueries.Library.Models.Requests;
using JollazApiQueries.Library.Models.Options;
using System.Linq.Dynamic.Core;

namespace JollazApiQueries.Tests.Filtering
{
    [TestClass]
    public class BoolFilterTests
    {
        [TestMethod]
        public void TestIfQueryIsFilteredByBoolEquals()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.Filters = new FilterItem[]
            {
                new FilterItem
                {
                    Name = "IsChild",
                    Criterion = FilterCriterion.Equal,
                    Parameter = true
                }
            };
            var query = Person.GetPersonQuery();

            var newQuery = query.FilterByDataRequest(dataRequest);
            
            Assert.AreEqual(1, newQuery.Count());
        }

        [TestMethod]
        public void TestIfQueryFilteredByBoolUnsupportedCriterionThrowsException()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.Filters = new FilterItem[]
            {
                new FilterItem
                {
                    Name = "IsChild",
                    Criterion = FilterCriterion.StringStartsWith,
                    Parameter = false
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
