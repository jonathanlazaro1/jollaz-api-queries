using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using JollazApiQueries.Library.Extensions;
using JollazApiQueries.Library.Models.Requests;

namespace JollazApiQueries.Library.Models.Results
{
    ///<summary>
    /// Class that represents the result of a DataRequest that was applied to a query. The data type of the return collection is unknown.
    ///</summary>
    public class DataResult
    {
        private int _itemsPerPage;

        private int _currentPage;

        public DataResult()
        {
        }

        ///<summary>
        /// The DataResult class constructor.
        ///<param name="items">The query which will be filtered.</param>
        ///<param name="dataRequest">The DataRequest that will be proccessed.</param>
        ///</summary>
        public DataResult(IQueryable items, DataRequest dataRequest)
        {
            this.ItemsTotal = items.Count();
            this.ItemsPerPage = dataRequest.ItemsPerPage;
            this.CurrentPage = dataRequest.CurrentPage;

            this.Items = items
                .SelectByDataRequest(dataRequest)
                .Skip(this.ItemsPerPage * (this.CurrentPage - 1))
                .Take(this.ItemsPerPage);
            if (!string.IsNullOrEmpty(dataRequest.Grouping))
            {
                this.Items = GroupedData.FromDynamicQuery(this.Items.GroupByDataRequest(dataRequest));
            }
        }

        ///<summary>
        /// Number of query total items, before pagination.
        ///</summary>
        public int ItemsTotal { get; set; }

        ///<summary>
        /// How many items per page are in results.
        /// <para>If the request comes with a number of items per page lesser than 1, this will become automatically 1.</para>
        ///</summary>
        public int ItemsPerPage
        {
            get => this._itemsPerPage;
            set
            {
                this._itemsPerPage = value < 1 ? 1 : value;
            }
        }

        ///<summary>
        /// The page where the results are coming from.
        /// <para>If the request comes with a current page number lesser than 1, this will become automatically 1.</para>
        /// <para>If the request comes with a current page number greater than the calculated in PageCount property, this will assume automatically the PageCount value.</para>
        ///</summary>
        public int CurrentPage
        {
            get => this._currentPage;
            set
            {
                // Value has to be at least 1
                value = value < 1 ? 1 : value;
                // Valor can't be greater than PageCount
                value = value > this.PageCount ? this.PageCount : value;
                this._currentPage = value;
            }
        }

        ///<summary>
        /// Total pages count. This is calculated automatically using the formula: (ItemsTotal / ItemsPerPage).
        ///</summary>
        public int PageCount
        {
            get
            {
                var pageCount = (int)Math.Ceiling((decimal)this.ItemsTotal / this.ItemsPerPage);
                return pageCount > 0 ? pageCount : 1;
            }
        }

        ///<summary>
        /// The items collection that will be returned. The collection type is unknown, which allows the custom selection of the return properties.
        ///</summary>
        public virtual IQueryable Items { get; set; }
    }
}