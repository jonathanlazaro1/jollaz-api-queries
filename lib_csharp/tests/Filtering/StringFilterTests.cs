using Microsoft.VisualStudio.TestTools.UnitTesting;
using JollazApiQueries.Library.Extensions;
using System.Linq;
using System;
using System.Linq.Dynamic.Core;
using JollazApiQueries.Model.Core;

namespace JollazApiQueries.Tests.Filtering
{
    [TestClass]
    public class StringFilterTests
    {
        [TestMethod]
        public void TestIfQueryIsFilteredByStringEquals()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.Filters = new FilterItem[]
            {
                new FilterItem
                {
                    Name = "Name",
                    Criterion = FilterCriterion.Equal,
                    Parameter = "john doe"
                }
            };
            var query = Person.GetPersonQuery();

            var newQuery = query.FilterByDataRequest(dataRequest);

            Assert.AreEqual(1, newQuery.Count());
            Assert.AreEqual(newQuery.First().Name, "John Doe");
        }

        [TestMethod]
        public void TestIfQueryIsFilteredByStringContains()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.Filters = new FilterItem[]
            {
                new FilterItem
                {
                    Name = "Name",
                    Criterion = FilterCriterion.StringContains,
                    Parameter = "white"
                }
            };
            var query = Person.GetPersonQuery();

            var newQuery = query.FilterByDataRequest(dataRequest);

            Assert.AreEqual(2, newQuery.Count());
        }

        [TestMethod]
        public void TestIfQueryIsFilteredByStringStartsWith()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.Filters = new FilterItem[]
            {
                new FilterItem
                {
                    Name = "Name",
                    Criterion = FilterCriterion.StringStartsWith,
                    Parameter = "white"
                }
            };
            var query = Person.GetPersonQuery();

            var newQuery = query.FilterByDataRequest(dataRequest);

            Assert.AreEqual(1, newQuery.Count());
            Assert.AreEqual("White Death", newQuery.First().Name);
        }

        [TestMethod]
        public void TestIfQueryIsFilteredByStringEndsWith()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.Filters = new FilterItem[]
            {
                new FilterItem
                {
                    Name = "Name",
                    Criterion = FilterCriterion.StringEndsWith,
                    Parameter = "white"
                }
            };
            var query = Person.GetPersonQuery();

            var newQuery = query.FilterByDataRequest(dataRequest);

            Assert.AreEqual(1, newQuery.Count());
            Assert.AreEqual("Snow White", newQuery.First().Name);
        }

        [TestMethod]
        public void TestIfMatchCaseAffectsStringDataResults()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.Filters = new FilterItem[]
            {
                new FilterItem
                {
                    Name = "Name",
                    Criterion = FilterCriterion.Equal,
                    Parameter = "john doe",
                    MatchCase = true
                }
            };
            var query = Person.GetPersonQuery();

            var newQuery = query.FilterByDataRequest(dataRequest);

            Assert.AreEqual(0, newQuery.Count());
        }

        [TestMethod]
        public void TestIfDiacriticsAreIgnored()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.Filters = new FilterItem[]
            {
                new FilterItem
                {
                    Name = "Name",
                    Criterion = FilterCriterion.StringContains,
                    Parameter = "joão da sílva conceição",
                }
            };
            var query = Person.GetPersonQuery();

            var newQuery = query.FilterByDataRequest(dataRequest);

            Assert.AreEqual(1, newQuery.Count());
        }

        [TestMethod]
        public void TestIfQueryFilteredByStringUnsupportedCriterionThrowsException()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.Filters = new FilterItem[]
            {
                new FilterItem
                {
                    Name = "Name",
                    Criterion = FilterCriterion.GreaterThanOrEqual,
                    Parameter = "white"
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
