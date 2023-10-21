﻿using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace Primary.Tests
{
    [TestFixture]
    internal class AccountsTests : TestWithApi
    {
        [Test]
        public async Task AccountStatementCanBeRetrieved()
        {
            // Submit an order
            var accountStatement = await Api.GetAccountStatement(Api.DemoAccount);

            Assert.That(accountStatement, Is.Not.Null);
            Assert.That(accountStatement.Name, Is.Not.Null.And.Not.Empty);
            Assert.That(accountStatement.MarketMember, Is.Not.Null.And.Not.Empty);
            Assert.That(accountStatement.MarketMemberIdentity, Is.Not.Null.And.Not.Empty);

            Assert.That(accountStatement.DetailedAccountReports, Is.Not.Empty);

            var detailedAccountReport = accountStatement.DetailedAccountReports.Values.First();
            Assert.That(detailedAccountReport.SettlementDate, Is.Not.EqualTo(default));
            Assert.That(detailedAccountReport.CurrencyBalance, Is.Not.Null.And.Not.Empty);
            Assert.That(detailedAccountReport.AvailableToOperate.Cash.DetailedCash, Is.Not.Empty);
        }

        [Test]
        public async Task AccountsCanBeRetrieved()
        {
            var accounts = await Api.GetAccounts();

            Assert.That(accounts, Is.Not.Null);
            Assert.That(accounts.Length, Is.GreaterThan(0));
            Assert.That(accounts[0].BrokerId, Is.EqualTo(1));
            Assert.That(accounts[0].Id, Is.EqualTo(4842));
            Assert.That(accounts[0].Name, Is.EqualTo(Api.DemoAccount));
            Assert.That(accounts[0].Status, Is.EqualTo(true));
        }
    }
}
