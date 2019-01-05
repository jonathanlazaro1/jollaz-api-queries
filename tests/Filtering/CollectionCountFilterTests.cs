using Microsoft.VisualStudio.TestTools.UnitTesting;
using JollazApiQueries.Library.Extensions;
using JollazApiQueries.Models.Requests;
using JollazApiQueries.Models.Options;
using System.Linq;
using System;

namespace JollazApiQueries.Tests.Filtering
{
    [TestClass]
    public class CollectionCountFilterTests
    {
        [TestMethod]
        public void TestIfQueryIsFilteredByEquals()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.Filters = new FilterItem[]
            {
                new FilterItem
                {
                    Name = "Addresses",
                    Criterion = FilterCriterion.Equal,
                    Parameter = 1
                }
            };
            var query = Person.GetPersonQuery();

            query = query.FilterByDataRequest(dataRequest);
            
            Assert.AreEqual(4, query.Count());
        }

        [TestMethod]
        public void TestIfQueryIsFilteredByGreaterThanOrEqual()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.Filters = new FilterItem[]
            {
                new FilterItem
                {
                    Name = "Addresses",
                    Criterion = FilterCriterion.GreaterThanOrEqual,
                    Parameter = 1
                }
            };
            var query = Person.GetPersonQuery();

            query = query.FilterByDataRequest(dataRequest);
            
            Assert.AreEqual(5, query.Count());
        }

        [TestMethod]
        public void TestIfQueryIsFilteredByGreaterThan()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.Filters = new FilterItem[]
            {
                new FilterItem
                {
                    Name = "Addresses",
                    Criterion = FilterCriterion.GreaterThan,
                    Parameter = 1
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
                    Name = "Addresses",
                    Criterion = FilterCriterion.LessThanOrEqual,
                    Parameter = 2
                }
            };
            var query = Person.GetPersonQuery();

            query = query.FilterByDataRequest(dataRequest);
            
            Assert.AreEqual(5, query.Count());
        }

        [TestMethod]
        public void TestIfQueryIsFilteredByLessThan()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.Filters = new FilterItem[]
            {
                new FilterItem
                {
                    Name = "Addresses",
                    Criterion = FilterCriterion.LessThan,
                    Parameter = 2
                }
            };
            var query = Person.GetPersonQuery();

            query = query.FilterByDataRequest(dataRequest);
            
            Assert.AreEqual(4, query.Count());
        }

        [TestMethod]
        public void TestIfQueryFilteredByNumberUnsupportedCriterionThrowsException()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.Filters = new FilterItem[]
            {
                new FilterItem
                {
                    Name = "Addresses",
                    Criterion = FilterCriterion.StringContains,
                    Parameter = 1
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
