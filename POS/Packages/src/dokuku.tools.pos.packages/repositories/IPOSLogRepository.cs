using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.tools.pos.packages.valueobjects;

namespace dokuku.tools.pos.packages.repositories
{
    public interface IPOSLogRepository
    {
        List<POSLog> GetPackages(string ownerId);
        List<POSLog> GetPackages();

        void Update(POSLog logAfterRevertCalculate);

        POSLog GetPackagesByTransactionNo(string transactionNo);

        List<valueobjects.POSLog> GetTransactionAmountAfterSharedDiscountIsZero(string ownerId);
    }
}
