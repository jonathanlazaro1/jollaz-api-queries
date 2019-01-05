using Microsoft.VisualStudio.TestTools.UnitTesting;
using JollazApiQueries.Library.Extensions;
using JollazApiQueries.Models.Requests;
using JollazApiQueries.Models.Options;
using System.Linq;
using System;

namespace JollazApiQueries.Tests.Filters
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

            query = query.FilterByDataRequest(dataRequest);
            
            Assert.AreEqual(2, query.Count());
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
                query = query.FilterByDataRequest(dataRequest);
            });
        }
    }
}
