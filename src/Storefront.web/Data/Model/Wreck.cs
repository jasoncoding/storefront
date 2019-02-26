using System;

namespace Storefront.web.Data.Model
{
    public class Wreck
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public decimal Price { get; set; }
        public DateTime AddedOn { get; set; }
    }
}
