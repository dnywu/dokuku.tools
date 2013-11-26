using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using dokuku.tools.pos.packages.repositories;
using dokuku.tools.pos.packages.valueobjects;

namespace dokuku.tools.pos.packages.tests
{
    [Subject("Get Transaction Wrong Calculate")]
    public class get_transaction_wrong_calculate
    {
        static RecalculateService service;
        static POSLogRepository repository;
        static List<POSLog> logs;

        Establish context = () =>
        {
            repository = new POSLogRepository();
            service = new RecalculateService();
        };

        Because of = () =>
        {
            logs = service.GetWrongCalculate(repository.GetPackages());
        };

        It logs_should_not_be_empty = () =>
        {
            logs.ShouldNotBeEmpty();
        };
    }
}
