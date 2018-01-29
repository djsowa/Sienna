using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIENN.Services.Models
{
    public class GenericList<T> where T : class
    {
        public GenericList()
        {
            Items = new List<T>();
        }

        public GenericList(List<T> items, int totalCount)
        {
            Items = items;
            TotalCount = totalCount;
        }

        public List<T> Items { get; set; }

        public int TotalCount { get; set; }
    }
}