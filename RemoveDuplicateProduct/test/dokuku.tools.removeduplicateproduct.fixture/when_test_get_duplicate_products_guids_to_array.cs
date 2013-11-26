using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using dokuku.mongoconfiguration;
using dokuku.tools.removeduplicateproduct.model;

namespace dokuku.tools.removeduplicateproduct.fixture
{
    [Subject("test get guids")]
    public class when_test_get_duplicate_products_guids_to_array
    {
        static MongoConfig mongo;
        static RemoveDuplicateProductRepository repo;
        static Guid[] guids;

        static string ownerId;

        Establish context = () =>
        {
            mongo = new MongoConfig();
            repo = new RemoveDuplicateProductRepository(mongo);

            ownerId = "minion.dave@yahoo.com";
        };

        Because of = () =>
        {
           guids  = repo.GetDuplicateIds(ownerId);
        };

        It should_not_be_null = () =>
        {
            guids.ShouldNotBeNull();
        };
    }
}
