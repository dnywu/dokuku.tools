using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using dokuku.tools.pos.packages.repositories;
using dokuku.tools.pos.packages.valueobjects;

namespace dokuku.tools.pos.packages.tests
{
    class when_get_pos_log_is_amount_after_shared_discount_is_zero
    {
        static POSLogRepository repository;
        static List<POSLog> results;

        Establish context = () =>
        {
            repository = new POSLogRepository();
        };

        Because of = () =>
        {
            results = repository.GetTransactionAmountAfterSharedDiscountIsZero("sft.pos1@gmail.com");
        };

        It results_should_not_be_empty = () =>
        {
            results.ShouldNotBeEmpty();
        };
    }
}
