using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.tools.pos.packages.repositories;
using NServiceBus;

namespace dokuku.tools.pos.packages.console
{
    class Program
    {
        static IPOSLogRepository posLogRepository;
        static RecalculateService service;
        static void Main(string[] args)
        {
            posLogRepository = new POSLogRepository();
            service = new RecalculateService(posLogRepository, BusContext.Bus);
            ConsoleKeyInfo key;
            do
            {
                Console.Clear();
                Console.WriteLine("Pilih option berikut ini:");
                Console.WriteLine("1. Perbaiki Transaction Package berdasarkan OwnerId");
                Console.WriteLine("2. Perbaiki Transaction Package");
                Console.WriteLine("3. Perbaiki Transaction Package berdasarkan Transaction No");
                Console.WriteLine("4. Perbaiki Transaction Yang Amount After Shared Discountnya Nol berdasarkan OwnerId");
                Console.Write("Masukkan: ");
                key = Console.ReadKey();
                Console.WriteLine();
                switch (key.KeyChar)
                {
                    case '1':
                        Console.Write("Masukkan ownerId : ");
                        string ownerId = Console.ReadLine();
                        RecalculatePackage(ownerId);
                        break;
                    case '2':
                        RecalculatePackage();
                        break;
                    case '3':
                        Console.Write("Masukkan No Transaksi : ");
                        string transactionNo = Console.ReadLine();
                        RecalculatePackageByTransactionNo(transactionNo);
                        break;
                    case '4':
                        Console.Write("Masukkan ownerId : ");
                        string id = Console.ReadLine();
                        UpdateAmountAfterSharedDiscount(id);
                        break;
                    default:
                        break;
                }
            }
            while (key.KeyChar != (char)27);
        }

        private static void UpdateAmountAfterSharedDiscount(string id)
        {
            service.UpdateAmountAfterSharedDiscount(id);
        }

        private static void RecalculatePackageByTransactionNo(string transactionNo)
        {
            service.ReCalculateByTransactionNo(transactionNo);
        }

        private static void RecalculatePackage(string ownerId)
        {
            service.ReCalculate(ownerId);
        }

        private static void RecalculatePackage()
        {
            service.ReCalculate();
        }
    }
}
