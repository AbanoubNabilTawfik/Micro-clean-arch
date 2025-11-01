using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Core.Entities
{
    public class Product :BaseEntity
    {
        public string Name {get;set;}
        public string Description {get;set;}
        public string Summary {get;set;}
        public string ImageFile {get;set;}

        [BsonRepresentation(MongoDB.Bson.BsonType.Decimal128)]
        public decimal Price {get;set;}

        public ProductBrand Brands {get;set;}

        public ProductType Types {get;set;}
    }
}
