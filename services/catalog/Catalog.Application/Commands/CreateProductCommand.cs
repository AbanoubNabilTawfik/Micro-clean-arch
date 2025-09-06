using Catalog.Application.Responses;
using Catalog.Core.Entities;
using MediatR;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Commands
{
    public class CreateProductCommand :IRequest<ProductReponseDto>
    {
        public string Name {get;set;}
        public string Description {get;set;}
        public string Summary {get;set;}
        public string ImageFile {get;set;}

        [BsonRepresentation(MongoDB.Bson.BsonType.Decimal128)]
        public decimal Price {get;set;}

        public ProductBrand Brand {get;set;}

        public ProductType Type {get;set;}
    }
}
