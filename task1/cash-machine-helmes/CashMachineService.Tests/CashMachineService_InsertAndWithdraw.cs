using System;
using Xunit;
using CashMachine.Services;

namespace CashMachine.UnitTests.Services
{
    public class CashMachineService_InsertAndWithdraw
    {
        private readonly CashMachineService cashMachineService = new CashMachineService();

        [Fact]
        public void InsertAndWithdraw_WithdrawNonDivisibleAmount_ReturnException()
        {
            var cashMachine = cashMachineService.createCashMachine();
            cashMachine.Insert(new int[]{5, 10, 20, 50, 100});

            Assert.Throws<NotEnoughBanknotesException>(() => cashMachine.Withdraw(1));
        }

        [Fact]
        public void InsertAndWithdraw_NotEnoughRequiredBanknotes_ReturnException()
        {
            var cashMachine = cashMachineService.createCashMachine();
            cashMachine.Insert(new int[]{5, 50, 100});

            Assert.Throws<NotEnoughBanknotesException>(() => cashMachine.Withdraw(60));
        }

        [Fact]
        public void InsertAndWithdraw_BanknotesThatContain0_ReturnException()
        {
            var cashMachine = cashMachineService.createCashMachine();
            cashMachine.Insert(new int[]{100, 100, 100, 100});

            Assert.Throws<NotEnoughFundsException>(() => cashMachine.Withdraw(500));
        }

        [Fact]
        public void InsertAndWithdraw_ValidBanknotes_ReturnException()
        {
            var cashMachine = cashMachineService.createCashMachine();
            cashMachine.Insert(new int[]{5, 10, 20, 50, 100});
            Assert.Equal(185, cashMachine.Withdraw(185));
        }

        [Fact]
        public void InsertAndWithdraw_ValidSameBanknotes_ReturnException()
        {
            var cashMachine = cashMachineService.createCashMachine();
            cashMachine.Insert(new int[]{100, 100, 100, 100, 100});
            Assert.Equal(500, cashMachine.Withdraw(500));
        }

        [Fact]
        public void InsertAndWithdraw_MultipleWithdrawals_ReturnException()
        {
            var cashMachine = cashMachineService.createCashMachine();
            cashMachine.Insert(new int[]{100, 100, 100, 100, 100});
            for (int i = 0; i < 5; i++)
            {
                Assert.Equal(100, cashMachine.Withdraw(100));
            }
            Assert.Throws<NotEnoughFundsException>(() => cashMachine.Withdraw(100));
        }
    }
}