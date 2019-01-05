using Microsoft.VisualStudio.TestTools.UnitTesting;
using JollazApiQueries.Library.Extensions;
using JollazApiQueries.Models.Requests;
using JollazApiQueries.Models.Options;
using System.Linq;
using System;

namespace JollazApiQueries.Tests.Filtering
{
    [TestClass]
    public class NumberFilterTests
    {
        [TestMethod]
        public void TestIfQueryIsFilteredByEquals()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.Filters = new FilterItem[]
            {
                new FilterItem
                {
                    Name = "Age",
                    Criterion = FilterCriterion.Equal,
                    Parameter = 21
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
                    Name = "Age",
                    Criterion = FilterCriterion.GreaterThanOrEqual,
                    Parameter = 39
                }
            };
            var query = Person.GetPersonQuery();

            query = query.FilterByDataRequest(dataRequest);
            
            Assert.AreEqual(2, query.Count());
        }

        [TestMethod]
        public void TestIfQueryIsFilteredByGreaterThan()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.Filters = new FilterItem[]
            {
                new FilterItem
                {
                    Name = "Age",
                    Criterion = FilterCriterion.GreaterThan,
                    Parameter = 39
                }
            };
            var query = Person.GetPersonQuery();

            query = query.FilterByDataRequest(dataRequest);
            
            Assert.AreEqual(1, query.Count());
        }

        [TestMethod]
        public void TestIfQueryIsFilteredByLessThanOrEqual()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.Filters = new FilterItem[]
            {
                new FilterItem
                {
                    Name = "Age",
                    Criterion = FilterCriterion.LessThanOrEqual,
                    Parameter = 22
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
                    Name = "Age",
                    Criterion = FilterCriterion.LessThan,
                    Parameter = 22
                }
            };
            var query = Person.GetPersonQuery();

            query = query.FilterByDataRequest(dataRequest);
            
            Assert.AreEqual(2, query.Count());
        }

        [TestMethod]
        public void TestIfQueryFilteredByNumberUnsupportedCriterionThrowsException()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.Filters = new FilterItem[]
            {
                new FilterItem
                {
                    Name = "Age",
                    Criterion = FilterCriterion.StringContains,
                    Parameter = 22
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
