using Microsoft.VisualStudio.TestTools.UnitTesting;
using JollazApiQueries.Library.Extensions;
using System.Linq;
using System;
using JollazApiQueries.Library.Models.Requests;

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
            query = query.OrderByDataRequest(dataRequest);

            Assert.AreEqual("Alicia Florick", query.First().Name);
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
            query = query.OrderByDataRequest(dataRequest);

            Assert.AreEqual(10, query.Last().Age);
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
                query = query.OrderByDataRequest(dataRequest);
            });
        }
    }
}
