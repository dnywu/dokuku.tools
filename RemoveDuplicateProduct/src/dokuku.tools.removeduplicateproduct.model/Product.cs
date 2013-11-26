using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace dokuku.tools.removeduplicateproduct.model
{
    [BsonIgnoreExtraElements]
    public class Product
    {
        public Guid Id { get; set; }
        public Guid UniqueId { get; set; }
        public string OwnerId { get; set; }
        public string Barcode { get; set; }
        public string Name { get; set; }
    }
}
