using Microsoft.VisualStudio.TestTools.UnitTesting;
using JollazApiQueries.Library.Extensions;
using System.Linq;
using System;
using JollazApiQueries.Library.Models.Requests;
using JollazApiQueries.Library.Models.Options;

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

            query = query.FilterByDataRequest(dataRequest);
            
            Assert.AreEqual(1, query.Count());
            Assert.AreEqual(query.First().Name, "John Doe");
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

            query = query.FilterByDataRequest(dataRequest);
            
            Assert.AreEqual(2, query.Count());
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

            query = query.FilterByDataRequest(dataRequest);
            
            Assert.AreEqual(1, query.Count());
            Assert.AreEqual("White Death", query.First().Name);
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

            query = query.FilterByDataRequest(dataRequest);
            
            Assert.AreEqual(1, query.Count());
            Assert.AreEqual("Snow White", query.First().Name);
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

            query = query.FilterByDataRequest(dataRequest);
            
            Assert.AreEqual(0, query.Count());
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
                query = query.FilterByDataRequest(dataRequest);
            });
        }
    }
}
