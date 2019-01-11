using Microsoft.VisualStudio.TestTools.UnitTesting;
using JollazApiQueries.Library.Extensions;
using System.Linq;
using System;
using System.Linq.Dynamic.Core;
using JollazApiQueries.Library.Models.Requests;
using JollazApiQueries.Library.Models.Options;
using JollazApiQueries.Library.Models.Results;

namespace JollazApiQueries.Tests.Selecting
{
    [TestClass]
    public class GroupingTests
    {
        [TestMethod]
        public void TestIfGroupingWorks()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.Grouping = "BirthCountry";
            dataRequest.Ordering = new OrderingItem[]
            {
                new OrderingItem
                {
                    Name = "BirthCountry"
                }
            };
            
            var query = Person.GetPersonQuery();
            var newQuery = query
                .OrderByDataRequest(dataRequest)
                .GroupByDataRequest(dataRequest);

            var groupedData = GroupedData.FromDynamicQuery(newQuery);

            // Three countries: Fantasy Land, Russia, United States
            Assert.AreEqual(3, groupedData.Count());
            // Ordered by BirthCountry, so Fantasy Land must be the first group
            Assert.AreEqual("Fantasy Land", groupedData.First().Key);
        }

        [TestMethod]
        public void TestIfMultipleGroupingWorks()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.Grouping = "x => new { x.Gender, x.BirthCountry }";
            dataRequest.Ordering = new OrderingItem[]
            {
                new OrderingItem
                {
                    Name = "Gender",
                    Descending = true
                },
                new OrderingItem
                {
                    Name = "BirthCountry"
                }
            };
            
            var query = Person.GetPersonQuery();
            var newQuery = query
                .OrderByDataRequest(dataRequest)
                .GroupByDataRequest(dataRequest);

            var groupedData = GroupedData.FromDynamicQuery(newQuery);

            /* Three countries: (FL)Fantasy Land, (RUS)Russia, (US)United States
            Two genders: (M)Male, (F)Female
            Four groups: M-RUS, M-US, F-FL, F-US
             */
            Assert.AreEqual(4, groupedData.Count());
            // Ordered by Gender dsc/BirthCountry asc
            Assert.AreEqual("Snow White", groupedData.First().Values.First().Name);
        }

        [TestMethod]
        public void TestIfSelectingOrderingAndGroupingWorks()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.Grouping = "Gender";
            dataRequest.Select = new string[]
            {
                "Name",
                "Gender"
            };
            dataRequest.Ordering = new OrderingItem[]
            {
                new OrderingItem
                {
                    Name = "Gender"
                },
                new OrderingItem
                {
                    Name = "Name",
                    Descending = true
                }
            };
            
            var query = Person.GetPersonQuery();
            var newQuery = query
                .OrderByDataRequest(dataRequest)
                .SelectByDataRequest(dataRequest)
                .GroupByDataRequest(dataRequest);

            var groupedData = GroupedData.FromDynamicQuery(newQuery);

            // Two groups: Male and Female
            Assert.AreEqual(2, groupedData.Count());
            // Ordered by Gender asc/Name dsc, 
            Assert.AreEqual("White Death", groupedData.First().Values.First().Name);
        }

        [TestMethod]
        public void TestIfInvalidGroupingThrowsException()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.Grouping = "FooBar";
            var query = Person.GetPersonQuery();

            Assert.ThrowsException<ArgumentException>(() =>
            {
                var newQuery = query
                    .GroupByDataRequest(dataRequest);
            });
        }
    }
}
