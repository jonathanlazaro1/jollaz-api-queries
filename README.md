# JollazApiQueries
A .NET package to help in the process of serving customized data from queries. Meant to be used with APIs, but any project in need of doing something more with C# queries will benefit from it.

## What can JollazApiQueries do?
Basically, this package can do three query operations: filtering, sorting, selecting and paginating. It's able to do all of these by receiving a single DataRequest object, that can grow as big as the data consumer needs to. In the end, you can stick with the query original type or select only the properties you want and access them dinamically, thanks to amazing work of the guys at [Dynamic Linq Core](https://github.com/StefH/System.Linq.Dynamic.Core).

## Basic usage
**NOTE**: *this is just a small intro to JollazApiQueries. You can read more about it at Wiki (coming soon!).*

The DataRequest object is the object coming from the outside world to ask for you data to consuming. It has some essential properties:

### Filters
An array of FilterItem objects. They define by which fields the data consumer wants to filter, using what parameter and criteria. Using More than one filter will require one FilterOperator to each new FilterItem added, so we can know how the data consumer wants its filters to be combined.

### Expressions
An array of Expression objects. Each Expression has its own array of FilterItems and FilterOperators. You can think about the expression as a parenthesis surrounding FilterItems in order to grouping them. The FilterOperators in Expression help to bind the FilterItems together.

### FilterOperators
FilterOperators are the same as logical operators. There are three of them: *AND*, *OR* and *XOR*. They will be needed to:
* Binding Expressions, if there are more than one of them, or
* Binding Filters, if there are more than one of them.
