using Microsoft.VisualStudio.TestTools.UnitTesting;
using JollazApiQueries.Library.Extensions;
using System.Linq;
using System;
using JollazApiQueries.Library.Models.Requests;
using JollazApiQueries.Library.Models.Options;

namespace JollazApiQueries.Tests.Filtering
{
    [TestClass]
    public class CommonFilterTests
    {
        public FilterItem[] CreateFilterItemCollection()
        {
            return new FilterItem[]
            {
                new FilterItem
                {
                        Name = "Name",
                        Criterion = FilterCriterion.StringContains,
                        Parameter = "john"
                },
                new FilterItem
                {
                    Name = "Age",
                    Criterion = FilterCriterion.Equal,
                    Parameter = 21
                }
            };
        } 

        public DataRequest CreateDataRequestWithTwoFilters()
        {
            var dataRequest = TestCommons.CreateDataRequest();

            dataRequest.Filters = this.CreateFilterItemCollection();
            dataRequest.Operators = new FilterOperator[]
            {
                FilterOperator.And
            };

            return dataRequest;
        }

        public DataRequest CreateDataRequestWithExpressions()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.Expressions = new FilterExpression[]
            {
                new FilterExpression
                {
                    Filters = this.CreateFilterItemCollection(),
                    Operators = new FilterOperator[]
                    {
                        FilterOperator.And
                    }
                },
                new FilterExpression
                {
                    Filters = new FilterItem[]
                    {
                        new FilterItem
                        {
                                Name = "Name",
                                Criterion = FilterCriterion.StringContains,
                                Parameter = "Snow White",
                                MatchCase = true
                        },
                        new FilterItem
                        {
                            Name = "Age",
                            Criterion = FilterCriterion.GreaterThan,
                            Parameter = 100
                        }
                    },
                    Operators = new FilterOperator[]
                    {
                        FilterOperator.And
                    }
                }
                
            };
            dataRequest.Operators = new FilterOperator[]
            {
                FilterOperator.Or
            };

            return dataRequest;
        }

        [TestMethod]
        public void TestIfIsPossibleToFilterByNestedProperty()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.Filters = new FilterItem[]
            {
                new FilterItem
                {
                    Name = "ContactInfo.Email",
                    Criterion = FilterCriterion.Equal,
                    Parameter = "peterpan@neverland.com",
                    MatchCase = true
                }
            };
            var query = Person.GetPersonQuery();
            query = query.FilterByDataRequest(dataRequest);

            Assert.AreEqual(1, query.Count());
            Assert.AreEqual("Peter Pan", query.First().Name);
        }

        [TestMethod]
        public void TestIfIsPossibleToFilterByFilterItemCollection()
        {
            var query = Person.GetPersonQuery();
            query = query.FilterByDataRequest(this.CreateDataRequestWithTwoFilters());

            Assert.AreEqual(1, query.Count());
        }

        [TestMethod]
        public void TestIfIsPossibleToFilterByExpressions()
        {
            var query = Person.GetPersonQuery();
            query = query.FilterByDataRequest(this.CreateDataRequestWithExpressions());

            Assert.AreEqual(2, query.Count());
        }

        [TestMethod]
        public void TestIfQueryFilteredByInvalidPropertyThrowsException()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.Filters = new FilterItem[]
            {
                new FilterItem
                {
                    Name = "FooBar",
                    Criterion = FilterCriterion.StringContains,
                    Parameter = "some value"
                }
            };
            var query = Person.GetPersonQuery();

            Assert.ThrowsException<ArgumentException>(() =>
            {
                query = query.FilterByDataRequest(dataRequest);
            });
        }

        [TestMethod]
        public void TestIfNullParameterThrowsException()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.Filters = new FilterItem[]
            {
                new FilterItem
                {
                    Name = "Name",
                    Criterion = FilterCriterion.Equal,
                    MatchCase = true
                }
            };
            var query = Person.GetPersonQuery();

            Assert.ThrowsException<ArgumentNullException>(()=>
            {
                query = query.FilterByDataRequest(dataRequest);
            });
        }

        [TestMethod]
        public void TestIfInvalidNumberOfLogicalOperatorsToFilterItemsThrowsException()
        {
            var query = Person.GetPersonQuery();
            var dataRequest = this.CreateDataRequestWithTwoFilters();
            dataRequest.Operators = new FilterOperator[] {};

            Assert.ThrowsException<ArgumentException>(() =>
            {
                query = query.FilterByDataRequest(dataRequest);
            });
        }

        [TestMethod]
        public void TestIfInvalidNumberOfLogicalOperatorsToExpressionsThrowsException()
        {
            var query = Person.GetPersonQuery();
            var dataRequest = this.CreateDataRequestWithExpressions();
            dataRequest.Operators = new FilterOperator[] {};

            Assert.ThrowsException<ArgumentException>(() =>
            {
                query = query.FilterByDataRequest(dataRequest);
            });
        }

        [TestMethod]
        public void TestIfQueryFilteredByNestedCollectionPropertyThrowsException()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.Filters = new FilterItem[]
            {
                new FilterItem
                {
                    Name = "Addresses.AddressLine1",
                    Criterion = FilterCriterion.StringContains,
                    Parameter = "street"
                }
            };
            var query = Person.GetPersonQuery();

            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                query = query.FilterByDataRequest(dataRequest);
            });
        }

        [TestMethod]
        public void TestIfParameterCastingErrorThrowsException()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.Filters = new FilterItem[]
            {
                new FilterItem
                {
                    Name = "Age",
                    Criterion = FilterCriterion.Equal,
                    Parameter = "Test"
                }
            };
            var query = Person.GetPersonQuery();

            Assert.ThrowsException<InvalidCastException>(() =>
            {
                query = query.FilterByDataRequest(dataRequest);
            });
        }
    }
}
