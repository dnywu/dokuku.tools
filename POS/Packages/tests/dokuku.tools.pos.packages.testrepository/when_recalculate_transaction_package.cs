using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using dokuku.tools.pos.packages.repositories;
using dokuku.tools.pos.packages.valueobjects;

namespace dokuku.tools.pos.packages.tests
{
    [Subject("ReCalculate")]
    class when_recalculate_transaction_package
    {
        static RecalculateService service;
        static POSLogRepository repository;
        static POSLog logBeforeRevertCalculation;
        static POSLog logAfterRevertCalculation;
        Establish context = () =>
        {
            repository = new POSLogRepository();
            service = new RecalculateService();
            logBeforeRevertCalculation = service.GetWrongCalculate(repository.GetPackages()).FirstOrDefault();
        };

        Because of = () =>
        {
            logAfterRevertCalculation = service.ReCalculate(logBeforeRevertCalculation);
        };

        It should_be_recalculated = () =>
        {

        };
    }
}
