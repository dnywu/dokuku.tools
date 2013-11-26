using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace dokuku.tools.pos.packages.valueobjects
{
    [BsonIgnoreExtraElements]
    public class POSLog
    {
        public string Id { get; set; }
        public long POSId { get; set; }
        public string OwnerId { get; set; }
        public ShoppingCartData ShoppingCartData { get; set; }
    }
    [BsonIgnoreExtraElements]
    public class ShoppingCartData
    {
        public string CardHolder { get; set; }
        public string CardNo { get; set; }
        public long CardTypeId { get; set; }
        public decimal CashFromMixedPayment { get; set; }
        public decimal ChargeAmount { get; set; }
        public decimal CreditFromMixedPayment { get; set; }
        public long CustomerId { get; set; }
        public decimal DiscountCard { get; set; }
        public decimal DiscountTotalAmount { get; set; }
        public decimal DiscPercent { get; set; }
        public long Id { get; set; }
        public List<ShoppingCartDataItem> Items { get; set; }
        public List<ShoppingCartDataItem> ItemsDeleted { get; set; }
        public List<ShoppingCartDataItemNonStock> ItemsNonStockable { get; set; }
        public List<ShoppingCartDataItemNonStock> ItemsNonStockableDeleted { get; set; }
        public decimal NetAmount { get; set; }
        public DateTime NoticedDate { get; set; }
        public string TableNumber { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TaxPercent { get; set; }
        public decimal TotalAmountOfItems { get; set; }
        public decimal TotalAmountOfItemsBeforeDiscount { get; set; }
        public decimal TotalDiscountItemAmount { get; set; }
        public decimal TotalDiscountItemPercent { get; set; }
        public long TotalGuest { get; set; }
        public long TransactionCcyId { get; set; }
        public string TransactionCcyCode { get; set; }
        public DateTime TransactionDate { get; set; }

        [BsonDefaultValue(MessageStatus.NOTLATE)]
        public MessageStatus MessageStatus { get; set; }

        public DateTime OriginalDate { get; set; }
        public string TransactionNumber { get; set; }
        public int TransPaymentType { get; set; }
        public bool WasPaidByCard { get; set; }
        public string UserName { get; set; }
        public long SessionId { get; set; }
        public string SessionGuid { get; set; }
        public DateTime SessionOpenDate { get; set; }
        public int TransactionType { get; set; }
        public decimal ServiceChargeAmount { get; set; }
        public string Guid { get; set; }
        public string SalesMan { get; set; }
    }
    [BsonIgnoreExtraElements]
    public class ShoppingCartDataItem
    {
        public decimal AmountAfterDiscount { get; set; }
        public decimal AmountBeforeDiscount { get; set; }
        public decimal SharedDiscountAmount { get; set; }
        public decimal AmountAfterSharedDiscount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal DiscountPercent { get; set; }
        public string ItemName { get; set; }
        public List<ShoppingCartPackageItem> PackageItems { get; set; }
        public long PartId { get; set; }
        public decimal Price { get; set; }
        public decimal Qty { get; set; }
        public long UnitId { get; set; }
        public string PartCode { get; set; }
        public string UnitCode { get; set; }
        public string PartGuid { get; set; }
        public Guid Guid { get; set; }
    }
    [BsonIgnoreExtraElements]
    public class ShoppingCartPackageItem
    {
        public string ItemPackageName { get; set; }
        public long PackageId { get; set; }
        public decimal PackageQty { get; set; }
        public long PartId { get; set; }
        public decimal Price { get; set; }
        public decimal TotalQty { get; set; }
    }
    [BsonIgnoreExtraElements]
    public class ShoppingCartDataItemNonStock
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Total { get; set; }
        public List<StockableDataItem> Items { get; set; }
        public decimal AmountAfterDiscount { get; set; }
        public decimal AmountBeforeDiscount { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Qty { get; set; }
    }
    [BsonIgnoreExtraElements]
    public class StockableDataItem
    {
        public decimal PackagePrice { get; set; }
        public string Barcode { get; set; }
        public string PartCode { get; set; }
        public string Id { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public decimal Qty { get; set; }
        public decimal SubTotal { get; set; }
    }

    public enum MessageStatus
    {
        NOTLATE,
        LATE
    }
}
