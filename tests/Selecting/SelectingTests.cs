using Microsoft.VisualStudio.TestTools.UnitTesting;
using JollazApiQueries.Library.Extensions;
using System.Linq;
using System;
using System.Linq.Dynamic.Core;
using JollazApiQueries.Model.Requests;
using JollazApiQueries.Model.Options;

namespace JollazApiQueries.Tests.Selecting
{
    [TestClass]
    public class SelectingTests
    {
        [TestMethod]
        public void TestIfSelectingWorks()
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
            dataRequest.Select = new string[]
            {
                "Name",
                "Age"
            };
            var query = Person.GetPersonQuery();
            var newQuery = query
            .FilterByDataRequest(dataRequest)
            .SelectByDataRequest(dataRequest);

            Assert.AreEqual(1, newQuery.Count());
            Assert.AreEqual("John Doe", newQuery.First().Name);
        }

        [TestMethod]
        public void TestIfSelectingNestedPropertyWorks()
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
            dataRequest.Select = new string[]
            {
                "Name",
                "ContactInfo.Email",

            };
            var query = Person.GetPersonQuery();
            var newQuery = query
            .FilterByDataRequest(dataRequest)
            .SelectByDataRequest(dataRequest);

            Assert.AreEqual(1, newQuery.Count());
            Assert.AreEqual("johndoe@gmail.com", newQuery.First().Email);
        }

        [TestMethod]
        public void TestIfInvalidPropertyNameThrowsException()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.Select = new string[]
            {
                "Name",
                "Foo Bar"
            };
            var query = Person.GetPersonQuery();

            Assert.ThrowsException<ArgumentException>(() =>
            {
                var newQuery = query
                    .FilterByDataRequest(dataRequest)
                    .SelectByDataRequest(dataRequest);
            });
        }
    }
}
