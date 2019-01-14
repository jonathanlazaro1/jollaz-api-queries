using Microsoft.VisualStudio.TestTools.UnitTesting;
using JollazApiQueries.Library.Extensions;
using System.Linq;
using System;
using JollazApiQueries.Library.Models.Requests;
using System.Linq.Dynamic.Core;

namespace JollazApiQueries.Tests.Ordering
{
    [TestClass]
    public class OrderingTests
    {
        [TestMethod]
        public void TestIfOrderingWorks()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.Ordering = new OrderingItem[]
            {
                new OrderingItem
                {
                    Name = "Name"
                }
            };
            var query = Person.GetPersonQuery();
            var newQuery = query.OrderByDataRequest(dataRequest);

            Assert.AreEqual("Alicia Florick", newQuery.First().Name);
        }

        [TestMethod]
        public void TestIfReverseOrderingWorks()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.Ordering = new OrderingItem[]
            {
                new OrderingItem
                {
                    Name = "Age",
                    Descending = true
                }
            };
            var query = Person.GetPersonQuery();
            var newQuery = query.OrderByDataRequest(dataRequest);

            Assert.AreEqual(10, newQuery.Last().Age);
        }

        [TestMethod]
        public void TestIfNestedPropertyOrderingWorks()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.Ordering = new OrderingItem[]
            {
                new OrderingItem
                {
                    Name = "ContactInfo != null ? ContactInfo.Email : string.Empty"
                }
            };
            var query = Person.GetPersonQuery();
            var newQuery = query.OrderByDataRequest(dataRequest);

            Assert.IsNull(newQuery.First().ContactInfo);
            Assert.AreEqual("aliciaflorick@hotmail.com", newQuery.Skip(2).First().ContactInfo.Email);
        }

        [TestMethod]
        public void TestIfInvalidPropertyNameThrowsException()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.Ordering = new OrderingItem[]
            {
                new OrderingItem
                {
                    Name = "FooBar"
                }
            };
            var query = Person.GetPersonQuery();

            Assert.ThrowsException<ArgumentException>(() =>
            {
                var newQuery = query.OrderByDataRequest(dataRequest);
            });
        }
    }
}
