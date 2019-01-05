using Microsoft.VisualStudio.TestTools.UnitTesting;
using JollazApiQueries.Library.Extensions;
using JollazApiQueries.Models.Requests;
using JollazApiQueries.Models.Options;
using System.Linq;
using System;

namespace JollazApiQueries.Tests.Filters
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

            query = query.FilterByDataRequest(dataRequest);
            
            Assert.AreEqual(1, query.Count());
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
                query = query.FilterByDataRequest(dataRequest);
            });
        }
    }
}
