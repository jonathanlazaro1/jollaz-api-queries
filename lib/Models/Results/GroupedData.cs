using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace JollazApiQueries.Library.Models.Results
{
    public class GroupedData
    {
        public dynamic Key { get; set; }

        public ICollection<dynamic> Values { get; set; }

        public GroupedData()
        {
            this.Values = new List<dynamic>();
        }


        public static IEnumerable<GroupedData> FromDynamicQuery(IQueryable data)
        {
            var ret = new List<GroupedData>();
            foreach (var group in data.ToDynamicList())
            {
                var retItem = new GroupedData();
                retItem.Key = group.Key; 
                foreach (var value in group)
                {
                    retItem.Values.Add(value);
                }
                ret.Add(retItem);
            }
            return ret;
        }
    }
}