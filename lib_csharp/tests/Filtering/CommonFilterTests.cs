using Microsoft.VisualStudio.TestTools.UnitTesting;
using JollazApiQueries.Library.Extensions;
using System.Linq;
using System;
using System.Linq.Dynamic.Core;
using JollazApiQueries.Model.Core;

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
            var newQuery = query.FilterByDataRequest(dataRequest);

            Assert.AreEqual(1, newQuery.Count());
            Assert.AreEqual("Peter Pan", newQuery.First().Name);
        }

        [TestMethod]
        public void TestIfIsPossibleToFilterByFilterItemCollection()
        {
            var query = Person.GetPersonQuery();
            var newQuery = query.FilterByDataRequest(this.CreateDataRequestWithTwoFilters());

            Assert.AreEqual(1, newQuery.Count());
        }

        [TestMethod]
        public void TestIfIsPossibleToFilterByExpressions()
        {
            var query = Person.GetPersonQuery();
            var newQuery = query.FilterByDataRequest(this.CreateDataRequestWithExpressions());

            Assert.AreEqual(2, newQuery.Count());
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
                var newQuery = query.FilterByDataRequest(dataRequest);
            });
        }

        [TestMethod]
        public void TestIfAdvancedFilterWorks()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.Filters = new FilterItem[]
            {
                new FilterItem
                {
                    IsAdvanced = true,
                    AdvancedQuery = "np(ContactInfo.Email) == \"aliciaflorick@hotmail.com\""
                }
            };
            var query = Person.GetPersonQuery();
            var newQuery = query.FilterByDataRequest(dataRequest);

            Assert.AreEqual(1, newQuery.Count());
        }

        [TestMethod]
        public void TestIfAdvancedAndTraditionalFiltersWorkTogether()
        {
            var dataRequest = TestCommons.CreateDataRequest();
            dataRequest.Operators = new FilterOperator[]
            {
                FilterOperator.And
            };

            dataRequest.Filters = new FilterItem[]
            {
                new FilterItem
                {
                    IsAdvanced = true,
                    AdvancedQuery = "np(Addresses.Count) > 1"
                },
                new FilterItem
                {
                    Name = "Name",
                    Criterion = FilterCriterion.StringContains,
                    Parameter = "snow"
                },
            };
            var query = Person.GetPersonQuery();
            var newQuery = query.FilterByDataRequest(dataRequest);

            Assert.AreEqual(1, newQuery.Count());
            Assert.AreEqual("Snow White", newQuery.First().Name);
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

            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                var newQuery = query.FilterByDataRequest(dataRequest);
            });
        }

        [TestMethod]
        public void TestIfInvalidNumberOfLogicalOperatorsToFilterItemsThrowsException()
        {
            var query = Person.GetPersonQuery();
            var dataRequest = this.CreateDataRequestWithTwoFilters();
            dataRequest.Operators = new FilterOperator[] { };

            Assert.ThrowsException<ArgumentException>(() =>
            {
                var newQuery = query.FilterByDataRequest(dataRequest);
            });
        }

        [TestMethod]
        public void TestIfInvalidNumberOfLogicalOperatorsToExpressionsThrowsException()
        {
            var query = Person.GetPersonQuery();
            var dataRequest = this.CreateDataRequestWithExpressions();
            dataRequest.Operators = new FilterOperator[] { };

            Assert.ThrowsException<ArgumentException>(() =>
            {
                var newQuery = query.FilterByDataRequest(dataRequest);
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

            Assert.ThrowsException<ArgumentException>(() =>
            {
                var newQuery = query.FilterByDataRequest(dataRequest);
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
                var newQuery = query.FilterByDataRequest(dataRequest);
            });
        }
    }
}
