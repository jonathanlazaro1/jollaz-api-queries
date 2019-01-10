using Microsoft.VisualStudio.TestTools.UnitTesting;
using JollazApiQueries.Library.Extensions;
using System.Linq;
using System;
using System.Linq.Dynamic.Core;
using JollazApiQueries.Library.Models.Requests;
using JollazApiQueries.Library.Models.Options;

namespace JollazApiQueries.Tests.DataResult
{
    [TestClass]
    public class DataResultTests
    {
        [TestMethod]
        public void TestIfDataResultWorks()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.Filters = new FilterItem[]
            {
                // We expect two results with this filter
                new FilterItem
                {
                    Name = "Name",
                    Criterion = FilterCriterion.StringContains,
                    Parameter = "white"
                }
            };

            dataRequest.Ordering = new OrderingItem[]
            {
                new OrderingItem
                {
                    Name = "Name"
                }
            };

            var query = Person.GetPersonQuery();
            var dataResult = query.Proccess(dataRequest);

            Assert.AreEqual(2, dataResult.ItemsTotal);
            Assert.AreEqual("Snow White", dataResult.Items.First().Name);
        }

        [TestMethod]
        public void TestIfPaginationWorks()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.ItemsPerPage = 1;
            dataRequest.CurrentPage = 2;
            dataRequest.Filters = new FilterItem[]
            {
                // We expect two results with this filter
                new FilterItem
                {
                    Name = "Name",
                    Criterion = FilterCriterion.StringContains,
                    Parameter = "white"
                }
            };

            dataRequest.Ordering = new OrderingItem[]
            {
                new OrderingItem
                {
                    Name = "Name"
                }
            };

            var query = Person.GetPersonQuery();
            var dataResult = query
                .Proccess(dataRequest);

            Assert.AreEqual(2, dataResult.ItemsTotal);
            Assert.AreEqual(1, dataResult.ItemsPerPage);
            Assert.AreEqual(2, dataResult.PageCount);
            Assert.AreEqual(2, dataResult.CurrentPage);
            Assert.AreEqual("White Death", dataResult.Items.First().Name);
        }

        [TestMethod]
        public void TestIfNegativeItemsPerPageNumberCrashesPagination()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.ItemsPerPage = -1000;
            dataRequest.CurrentPage = 2;
            dataRequest.Filters = new FilterItem[]
            {
                // We expect two results with this filter
                new FilterItem
                {
                    Name = "Name",
                    Criterion = FilterCriterion.StringContains,
                    Parameter = "white"
                }
            };

            dataRequest.Ordering = new OrderingItem[]
            {
                new OrderingItem
                {
                    Name = "Name"
                }
            };

            var query = Person.GetPersonQuery();
            var dataResult = query
                .Proccess(dataRequest);

            Assert.AreEqual(2, dataResult.ItemsTotal);
            Assert.AreEqual(1, dataResult.ItemsPerPage);
            Assert.AreEqual(2, dataResult.PageCount);
            Assert.AreEqual(2, dataResult.CurrentPage);
        }

        [TestMethod]
        public void TestIfNegativePageNumberCrashesPagination()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.ItemsPerPage = 1;
            dataRequest.CurrentPage = -1000;
            dataRequest.Filters = new FilterItem[]
            {
                // We expect two results with this filter
                new FilterItem
                {
                    Name = "Name",
                    Criterion = FilterCriterion.StringContains,
                    Parameter = "white"
                }
            };

            dataRequest.Ordering = new OrderingItem[]
            {
                new OrderingItem
                {
                    Name = "Name"
                }
            };

            var query = Person.GetPersonQuery();
            var dataResult = query
                .Proccess(dataRequest);

            Assert.AreEqual(2, dataResult.ItemsTotal);
            Assert.AreEqual(1, dataResult.ItemsPerPage);
            Assert.AreEqual(2, dataResult.PageCount);
            Assert.AreEqual(1, dataResult.CurrentPage);
        }

        [TestMethod]
        public void TestIfPageNumberAbovePageCountCrashesPagination()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.ItemsPerPage = 1;
            dataRequest.CurrentPage = 5;
            dataRequest.Filters = new FilterItem[]
            {
                // We expect two results with this filter
                new FilterItem
                {
                    Name = "Name",
                    Criterion = FilterCriterion.StringContains,
                    Parameter = "white"
                }
            };

            dataRequest.Ordering = new OrderingItem[]
            {
                new OrderingItem
                {
                    Name = "Name"
                }
            };

            var query = Person.GetPersonQuery();
            var dataResult = query
                .Proccess(dataRequest);

            Assert.AreEqual(2, dataResult.ItemsTotal);
            Assert.AreEqual(1, dataResult.ItemsPerPage);
            Assert.AreEqual(2, dataResult.PageCount);
            Assert.AreEqual(2, dataResult.CurrentPage);
        }
    }
}
