using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableStorage.Entities
{
    public class Product : TableEntity
    {
        public string Name { get; set; }
        public string No { get; set; }
    }
}
