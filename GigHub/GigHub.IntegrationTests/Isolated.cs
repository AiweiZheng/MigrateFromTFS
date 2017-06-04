using System;
using System.Transactions;
using NUnit.Framework;

namespace GigHub.IntegrationTests
{
    public class Isolated : Attribute, ITestAction
    {
        private TransactionScope _transactionScope;

        public void BeforeTest(TestDetails testDetails)
        {
            _transactionScope = new TransactionScope();
        }

        public void AfterTest(TestDetails testDetails)
        {
            _transactionScope.Dispose();
        }

        public ActionTargets Targets { get { return ActionTargets.Test; } }
    }
}
