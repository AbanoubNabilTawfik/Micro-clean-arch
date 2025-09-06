using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Core.Entities
{
    public class ProductType :BaseEntity
    {
        //[BsonElement("name")]
        public string Name { get; set; }
    }
}
