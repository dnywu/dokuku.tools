using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.tools.pos.packages.valueobjects;
using dokuku.tools.pos.packages.repositories;
using dokuku.pos.messages;
using NServiceBus;

namespace dokuku.tools.pos.packages
{
    public class RecalculateService
    {
        IPOSLogRepository posLogRepository;
        IBus bus;
        public RecalculateService() { }
        public RecalculateService(IPOSLogRepository posRepository, IBus bus)
        {
            this.posLogRepository = posRepository;
            this.bus = bus;
        }

        public void ReCalculate(string ownerId)
        {
            List<POSLog> logs = posLogRepository.GetPackages(ownerId);
            logs = GetWrongCalculate(logs);
            foreach (var log in logs)
            {
                var logAfterRevertCalculate = ReCalculate(log);
                posLogRepository.Update(logAfterRevertCalculate);
                Publish(log);
            }
        }


        public void ReCalculateByTransactionNo(string transactionNo)
        {
            POSLog log = posLogRepository.GetPackagesByTransactionNo(transactionNo);
            if (IsWrongCalculated(log))
            {
                //var logAfterRevertCalculate = ReCalculate(log);
                //posLogRepository.Update(logAfterRevertCalculate);
                Publish(log);
            }
        }

        public void ReCalculate()
        {
            List<POSLog> logs = posLogRepository.GetPackages();
            logs = GetWrongCalculate(logs);
            foreach (var log in logs)
            {
                var logAfterRevertCalculate = ReCalculate(log);
                posLogRepository.Update(logAfterRevertCalculate);
                Publish(log);
            }
        }

        private void Publish(POSLog log)
        {
            bus.Send("dokukupostransactionlog", new ShoppingCartCheckedOut
            {
                Id = log.Id
            });

            bus.Send("salesreport", new ShoppingCartCheckedOut
            {
                Id = log.Id
            });

            bus.Send("dokukuprofitlossworker", new ShoppingCartCheckedOut
            {
                Id = log.Id
            });
        }

        public POSLog ReCalculate(POSLog log)
        {
            POSLog logAfterRevertCalculation = RevertCalculatePackage(log);
            foreach (ShoppingCartDataItemNonStock scNonStockItem in logAfterRevertCalculation.ShoppingCartData.ItemsNonStockable)
            {
                decimal discountPercent = scNonStockItem.DiscountPercent;
                foreach (StockableDataItem stockableSubItem in scNonStockItem.Items)
                {
                    Guid guid = Guid.NewGuid();
                    var discountAmountItem = ((discountPercent * stockableSubItem.SubTotal) / 100) / stockableSubItem.Qty;
                    var discountPercentItem = discountPercent / stockableSubItem.Qty;

                    logAfterRevertCalculation.ShoppingCartData.Items.Add(new ShoppingCartDataItem
                    {
                        AmountAfterDiscount = stockableSubItem.SubTotal - (discountAmountItem * stockableSubItem.Qty),
                        AmountBeforeDiscount = stockableSubItem.SubTotal,
                        DiscountAmount = discountAmountItem,
                        DiscountPercent = discountPercentItem,
                        Guid = guid,
                        ItemName = string.Format("{0} [{1}]", stockableSubItem.ItemName, scNonStockItem.Name),
                        PartCode = stockableSubItem.Id,
                        Price = stockableSubItem.PackagePrice,
                        Qty = stockableSubItem.Qty,
                        PartGuid = stockableSubItem.Id,
                    });
                }
            }
            return logAfterRevertCalculation;
        }

        public POSLog RevertCalculatePackage(POSLog log)
        {
            foreach (var package in log.ShoppingCartData.ItemsNonStockable)
            {
                foreach (StockableDataItem itemPackage in package.Items)
                {
                    ShoppingCartDataItem item = log.ShoppingCartData.Items.FirstOrDefault(i => i.PartGuid == itemPackage.Id);
                    if (item != null && item.Qty != itemPackage.Qty)
                        item.Qty = item.Qty - itemPackage.Qty;
                    else
                        log.ShoppingCartData.Items.Remove(item);
                }
            }
            return log;
        }

        public List<POSLog> GetWrongCalculate(List<POSLog> logs)
        {
            List<POSLog> results = new List<POSLog>();
            foreach (var log in logs)
            {
                foreach (var package in log.ShoppingCartData.ItemsNonStockable)
                {
                    foreach (StockableDataItem itemPackage in package.Items)
                    {
                        ShoppingCartDataItem item = log.ShoppingCartData.Items.FirstOrDefault(i => i.PartGuid == itemPackage.Id);
                        if (item == null) continue;
                        if (item.Qty != itemPackage.Qty)
                        {
                            results.Add(log);
                        }
                    }
                }
            }
            return results;
        }

        public bool IsWrongCalculated(POSLog log)
        {
            Console.WriteLine("log is null : ", log == null);
            Console.WriteLine("shoppingcartdata is null : ", log.ShoppingCartData == null);
            Console.WriteLine("itemNonStockable is null : ", log.ShoppingCartData.ItemsNonStockable == null);
            foreach (var package in log.ShoppingCartData.ItemsNonStockable)
            {
                foreach (StockableDataItem itemPackage in package.Items)
                {
                    ShoppingCartDataItem item = log.ShoppingCartData.Items.FirstOrDefault(i => i.PartGuid == itemPackage.Id);
                    if (item == null) continue;
                    if (item.Qty != itemPackage.Qty)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void UpdateAmountAfterSharedDiscount(string ownerId)
        {
            List<POSLog> logs = posLogRepository.GetTransactionAmountAfterSharedDiscountIsZero(ownerId);
            foreach (POSLog log in logs)
            {
                foreach (var item in log.ShoppingCartData.Items)
                {
                    if (item.AmountAfterSharedDiscount == 0)
                    {
                        item.AmountAfterSharedDiscount = item.AmountAfterDiscount;
                        posLogRepository.Update(log);
                        Publish(log);
                    }
                }
            }
        }
    }
}
