using System;
using Xunit;
using CashMachine.Services;

namespace CashMachine.UnitTests.Services
{
    public class CashMachineService_InsertValidAmounts
    {
        private readonly CashMachineService cashMachineService = new CashMachineService();

        [Fact]
        public void InsertValidAmounts_AllDifferentBanknotes_ReturnException()
        {
            var cashMachine = cashMachineService.createCashMachine();

            cashMachine.Insert(new int[]{5, 10, 20, 50, 100});
        }

        [Fact]
        public void InsertValidAmounts_AllSameBanknotes_ReturnException()
        {
            var cashMachine = cashMachineService.createCashMachine();

            cashMachine.Insert(new int[]{5, 5, 5, 5, 5});
        }
    }
}