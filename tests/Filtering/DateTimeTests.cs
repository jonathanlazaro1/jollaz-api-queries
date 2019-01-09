using Microsoft.VisualStudio.TestTools.UnitTesting;
using JollazApiQueries.Library.Extensions;
using System.Linq;
using System;
using JollazApiQueries.Library.Models.Requests;
using JollazApiQueries.Library.Models.Options;

namespace JollazApiQueries.Tests.Filtering
{
    [TestClass]
    public class DateTimeFilterTests
    {
        [TestMethod]
        public void TestIfQueryIsFilteredByEquals()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.Filters = new FilterItem[]
            {
                new FilterItem
                {
                    Name = "BirthDate",
                    Criterion = FilterCriterion.Equal,
                    Parameter = $"{DateTime.Now.Year - 21}-01-01T00:00:00"
                }
            };
            var query = Person.GetPersonQuery();

            query = query.FilterByDataRequest(dataRequest);
            
            Assert.AreEqual(1, query.Count());
            Assert.AreEqual(query.First().Name, "John Doe");
        }

        [TestMethod]
        public void TestIfQueryIsFilteredByGreaterThanOrEqual()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.Filters = new FilterItem[]
            {
                new FilterItem
                {
                    Name = "BirthDate",
                    Criterion = FilterCriterion.GreaterThanOrEqual,
                    Parameter = $"{DateTime.Now.Year - 39}-01-01T00:00:00"
               }
            };
            var query = Person.GetPersonQuery();

            query = query.FilterByDataRequest(dataRequest);
            
            Assert.AreEqual(4, query.Count());
        }

        [TestMethod]
        public void TestIfQueryIsFilteredByGreaterThan()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.Filters = new FilterItem[]
            {
                new FilterItem
                {
                    Name = "BirthDate",
                    Criterion = FilterCriterion.GreaterThan,
                    Parameter = $"{DateTime.Now.Year - 39}-01-01T00:00:00"
                }
            };
            var query = Person.GetPersonQuery();

            query = query.FilterByDataRequest(dataRequest);
            
            Assert.AreEqual(3, query.Count());
        }

        [TestMethod]
        public void TestIfQueryIsFilteredByLessThanOrEqual()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.Filters = new FilterItem[]
            {
                new FilterItem
                {
                    Name = "BirthDate",
                    Criterion = FilterCriterion.LessThanOrEqual,
                    Parameter = $"{DateTime.Now.Year - 22}-01-01T00:00:00"
                }
            };
            var query = Person.GetPersonQuery();

            query = query.FilterByDataRequest(dataRequest);
            
            Assert.AreEqual(3, query.Count());
        }

        [TestMethod]
        public void TestIfQueryIsFilteredByLessThan()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.Filters = new FilterItem[]
            {
                new FilterItem
                {
                    Name = "BirthDate",
                    Criterion = FilterCriterion.LessThan,
                    Parameter = $"{DateTime.Now.Year - 22}-01-01T00:00:00"
                }
            };
            var query = Person.GetPersonQuery();

            query = query.FilterByDataRequest(dataRequest);
            
            Assert.AreEqual(2, query.Count());
        }

        [TestMethod]
        public void TestIfQueryFilteredByDateTimeUnsupportedCriterionThrowsException()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.Filters = new FilterItem[]
            {
                new FilterItem
                {
                    Name = "BirthDate",
                    Criterion = FilterCriterion.StringContains,
                    Parameter = $"{DateTime.Now.Year - 22}-01-01T00:00:00"
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
