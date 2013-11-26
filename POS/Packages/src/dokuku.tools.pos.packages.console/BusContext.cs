using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;

namespace dokuku.tools.pos.packages.console
{
    class BusContext
    {
        static IBus _bus;
        public static IBus Bus
        {
            get
            {
                if (_bus == null)
                {
                    InitBus();
                }
                return _bus;
            }
        }

        private static void InitBus()
        {
            _bus = Configure.With()
                                .DefaultBuilder()
                                .MsmqSubscriptionStorage()
                                .BinarySerializer()
                                .MsmqTransport()
                                    .IsTransactional(true)
                                    .PurgeOnStartup(false)
                                .UnicastBus()
                                    .LoadMessageHandlers()
                                    .ImpersonateSender(true)
                                .CreateBus()
                                .Start();
        }
    }
}
