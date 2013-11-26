using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using dokuku.tools.pos.packages.repositories;
using dokuku.tools.pos.packages.valueobjects;

namespace dokuku.tools.pos.packages.tests
{
    [Subject("Get Transaction Packages")]
    public class when_get_package_transaction
    {
        static POSLogRepository repository;
        static List<POSLog> results;

        Establish context = () =>
        {
            repository = new POSLogRepository();
        };

        Because of = () =>
        {
            results = repository.GetPackages();
        };

        It results_should_not_be_empty = () =>
        {
            results.ShouldNotBeEmpty();
        };
    }
}
