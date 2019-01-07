# JollazApiQueries
A .NET package to help in the process of serving customized data from queries. Meant to be used with APIs, but any project in need of doing something more with C# queries will benefit from it.

You can install our package by using [NuGet](https://www.nuget.org/packages/JollazApiQueries/):

`    dotnet add package JollazApiQueries`

## What can JollazApiQueries do?
Basically, this package can do three query operations: filtering, sorting, selecting and paginating. It's able to do all of these by receiving a single DataRequest object, that can grow as big as the data consumer needs to. In the end, you can stick with the query original type or select only the properties you want and access them dinamically, thanks to amazing work of the guys at [Dynamic Linq Core](https://github.com/StefH/System.Linq.Dynamic.Core).

## How to use
Check our [wiki](https://github.com/jonathanlazaro1/jollaz-api-queries/wiki) to learn more how this package works!
