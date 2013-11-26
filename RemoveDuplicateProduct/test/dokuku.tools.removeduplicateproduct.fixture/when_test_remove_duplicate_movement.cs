﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using dokuku.mongoconfiguration;
using dokuku.tools.removeduplicateproduct.model;

namespace dokuku.tools.removeduplicateproduct.fixture
{
    [Subject("test count")]
    public class when_test_remove_duplicate_movement
    {
        static MongoConfig mongo;
        static RemoveDuplicateProductRepository repo;

        static string ownerId;

        Establish context = () =>
        {
            mongo = new MongoConfig();
            repo = new RemoveDuplicateProductRepository(mongo);

            ownerId = "minion.dave@yahoo.com";
        };

        Because of = () =>
        {
            repo.RemoveDuplicateProductMovement(ownerId);
        };

        It should_be_removed = () =>
        {
        };
    }
}