using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using dokuku.tools.pos.packages.repositories;
using dokuku.tools.pos.packages.valueobjects;

namespace dokuku.tools.pos.packages.tests
{
    [Subject("Revert Calculation Package")]
    class when_revert_calculate_transaction_package
    {
        static RecalculateService service;
        static POSLogRepository repository;
        static POSLog logBeforeRevertCalculation;
        static POSLog logAfterRevertCalculation;
        static decimal qtyBeforeRevertCalculation;

        Establish context = () =>
        {
            repository = new POSLogRepository();
            service = new RecalculateService();
            logBeforeRevertCalculation = service.GetWrongCalculate(repository.GetPackages()).FirstOrDefault();
            qtyBeforeRevertCalculation = logBeforeRevertCalculation.ShoppingCartData.Items.Sum(i => i.Qty);
        };

        Because of = () =>
        {
            logAfterRevertCalculation = service.RevertCalculatePackage(logBeforeRevertCalculation);
        };

        It qty_before_calculate_and_after_calculate_should_be_not_same = () =>
        {
            var qtyAfterRevertCalculation = logAfterRevertCalculation.ShoppingCartData.Items.Sum(i => i.Qty);
            qtyBeforeRevertCalculation.ShouldNotEqual(qtyAfterRevertCalculation);
        };
    }
}
