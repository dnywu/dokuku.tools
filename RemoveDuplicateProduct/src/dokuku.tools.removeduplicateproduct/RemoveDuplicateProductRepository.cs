using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.mongoconfiguration;
using dokuku.tools.removeduplicateproduct.model;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Bson;

namespace dokuku.tools.removeduplicateproduct
{
    public class RemoveDuplicateProductRepository
    {
        MongoConfig mongo;
        public RemoveDuplicateProductRepository(MongoConfig mongo)
        {
            this.mongo = mongo;
        }

        public IList<Product> GetDuplicateProduct(string ownerId)
        {
            IMongoQuery query = Query.And(Query.EQ("OwnerId", ownerId),Query.In("Barcode", ItemCollection.Distinct("Barcode", Query.EQ("OwnerId", ownerId))));
            IList<Product> results = ItemCollection.FindAs<Product>(query).SetSortOrder("Barcode").ToList();
            return results;
        }

        public Guid[] GetDuplicateIds(string ownerId)
        {
            var results = GetDuplicateProduct(ownerId);
            Guid[] guids = results.Where(x => x.Id != x.UniqueId).Select(x => x.Id).ToArray();
            return guids;
        }

        public string[] GetDuplicateIdsAsString(string ownerId)
        {
            var results = GetDuplicateProduct(ownerId);
            string[] guids = results.Where(x => x.Id != x.UniqueId).Select(x => x.Id.ToString()).ToArray();
            return guids;
        }

        public void RemoveDuplicateProduct(string ownerId)
        {
            Guid[] guids = GetDuplicateIds(ownerId);
            IMongoQuery qry = Query.And(Query.EQ("OwnerId", ownerId), Query.In("_id", BsonArray.Create(guids)));
            ItemCollection.Remove(qry, SafeMode.True);
        }

        public void RemoveDuplicateProductMovement(string ownerId)
        {
            string[] guids = GetDuplicateIdsAsString(ownerId);
            IMongoQuery qry = Query.And(Query.EQ("OwnerId", ownerId), Query.In("_id", BsonArray.Create(guids)));
            StockCardMovementCollection.Remove(qry, SafeMode.True);
        }

        #region PRIVATE
        private MongoCollection ItemCollection
        {
            get { return mongo.DatabaseReporting.GetCollection("Item"); }
        }

        private MongoCollection StockCardMovementCollection
        {
            get { return mongo.DatabaseReporting.GetCollection("StockCardMovement"); }
        }
        #endregion
    }
}
