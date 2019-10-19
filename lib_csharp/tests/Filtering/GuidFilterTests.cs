using Microsoft.VisualStudio.TestTools.UnitTesting;
using JollazApiQueries.Library.Extensions;
using System.Linq;
using System;
using System.Linq.Dynamic.Core;
using JollazApiQueries.Model.Core;

namespace JollazApiQueries.Tests.Filtering
{
    [TestClass]
    public class GuidFilterTests
    {
        [TestMethod]
        public void TestIfQueryIsFilteredByGuidEquals()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.Filters = new FilterItem[]
            {
                new FilterItem
                {
                    Name = "Id",
                    Criterion = FilterCriterion.Equal,
                    Parameter = new Guid("74a46e33-09b3-4f90-9818-7ca3609f887f")
                }
            };
            var query = Person.GetPersonQuery();

            var newQuery = query.FilterByDataRequest(dataRequest);

            Assert.AreEqual(1, newQuery.Count());
            Assert.AreEqual("Peter Pan", newQuery.First().Name);
        }

        [TestMethod]
        public void TestIfQueryIsFilteredByGuidIsNull()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.Filters = new FilterItem[]
            {
                new FilterItem
                {
                    Name = "Id",
                    Criterion = FilterCriterion.NotNull,
                    Not = true
                }
            };
            var query = Person.GetPersonQuery();

            var newQuery = query.FilterByDataRequest(dataRequest);

            Assert.AreEqual(5, newQuery.Count());
        }

        [TestMethod]
        public void TestIfQueryFilteredByGuidUnsupportedCriterionThrowsException()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.Filters = new FilterItem[]
            {
                new FilterItem
                {
                    Name = "Id",
                    Criterion = FilterCriterion.StringStartsWith,
                    Parameter = Guid.Empty
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
