using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.mongoconfiguration;
using MongoDB.Driver;
using dokuku.tools.pos.packages.valueobjects;
using MongoDB.Driver.Builders;

namespace dokuku.tools.pos.packages.repositories
{
    public class POSLogRepository : IPOSLogRepository
    {
        MongoConfig mongoConfig;
        public POSLogRepository()
        {
            mongoConfig = MongoConfig.Instance;
        }

        public List<valueobjects.POSLog> GetPackages(string ownerId)
        {
            IMongoQuery query = Query.And(Query.ElemMatch("ShoppingCartData.ItemsNonStockable", Query.Exists("Code", true)), Query.EQ("OwnerId", ownerId));
            List<POSLog> results = Collection.Find(query).ToList();
            return results;
        }

        public List<valueobjects.POSLog> GetPackages()
        {
            List<POSLog> results = Collection.Find(Query.ElemMatch("ShoppingCartData.ItemsNonStockable", Query.Exists("Code", true))).ToList();
            return results;
        }

        public List<valueobjects.POSLog> GetTransactionAmountAfterSharedDiscountIsZero(string ownerId)
        {
            IMongoQuery query = Query.And(Query.EQ("ShoppingCartData.Items.AmountAfterSharedDiscount", "0"), Query.EQ("OwnerId", ownerId));
            List<POSLog> results = Collection.Find(query).ToList();
            return results;
        }

        public POSLog GetPackagesByTransactionNo(string transactionNo)
        {
            IMongoQuery query = Query.And(Query.ElemMatch("ShoppingCartData.ItemsNonStockable", Query.Exists("Code", true)), Query.EQ("ShoppingCartData.TransactionNumber", transactionNo));
            POSLog results = Collection.FindOne(query);
            return results;
        }

        public void Update(POSLog logAfterRevertCalculate)
        {
            Collection.Save(logAfterRevertCalculate);
        }

        private MongoCollection<POSLog> Collection
        {
            get { return mongoConfig.Database.GetCollection<POSLog>("POSSalesLog"); }
        }
    }
}
