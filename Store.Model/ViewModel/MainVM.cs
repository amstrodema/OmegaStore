using Store.Model.Hybrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Model.ViewModel
{
    public class MainVM
    {

        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<CategoryHybrid> CategoryHybrids { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<Feature> Features { get; set; }
        public IEnumerable<Item> Stocks { get; set; }
        public IEnumerable<Item> Featured { get; set; }
        public IEnumerable<Item> Favourite { get; set; }
        public IEnumerable<Item> Latest { get; set; }
        public IEnumerable<Group> Groups { get; set; }
        public Order Order { get; set; }
        public Group Group { get; set; }
        public Item Stock { get; set; }
        public Category Category { get; set; }
        public int FaveCount { get; set; }
    }
}
