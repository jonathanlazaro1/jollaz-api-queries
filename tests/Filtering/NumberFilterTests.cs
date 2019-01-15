using Microsoft.VisualStudio.TestTools.UnitTesting;
using JollazApiQueries.Library.Extensions;
using System.Linq;
using System;
using System.Linq.Dynamic.Core;
using JollazApiQueries.Model.Options;
using JollazApiQueries.Model.Requests;

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

            var newQuery = query.FilterByDataRequest(dataRequest);
            
            Assert.AreEqual(1, newQuery.Count());
            Assert.AreEqual(newQuery.First().Name, "John Doe");
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

            var newQuery = query.FilterByDataRequest(dataRequest);
            
            Assert.AreEqual(2, newQuery.Count());
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

            var newQuery = query.FilterByDataRequest(dataRequest);
            
            Assert.AreEqual(1, newQuery.Count());
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

            var newQuery = query.FilterByDataRequest(dataRequest);
            
            Assert.AreEqual(3, newQuery.Count());
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

            var newQuery = query.FilterByDataRequest(dataRequest);
            
            Assert.AreEqual(2, newQuery.Count());
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
                var newQuery = query.FilterByDataRequest(dataRequest);
            });
        }
    }
}
