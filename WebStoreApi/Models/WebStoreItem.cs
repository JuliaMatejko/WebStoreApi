using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStoreApi.Models
{
    public class WebStoreItem
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsDiscounted { get; set; }
        public string Picture { get; set; }
        public bool IsHidden { get; set; }
    }
}
