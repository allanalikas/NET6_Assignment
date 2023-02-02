using System;
using Xunit;
using CashMachine.Services;

namespace CashMachine.UnitTests.Services
{
    public class CashMachineService_CannotWithdraw
    {
        private readonly CashMachineService cashMachineService = new CashMachineService();

        [Fact]
        public void CannotWithdraw_EmptyCashMachine_ReturnException()
        {
            var cashMachine = cashMachineService.createCashMachine();

            Assert.Throws<NotEnoughFundsException>(() => cashMachine.Withdraw(100));
        }

        [Fact]
        public void CannotWithdraw_EmptyCashMachineAndNegativeAmount_ReturnException()
        {
            var cashMachine = cashMachineService.createCashMachine();

            Assert.Throws<InvalidAmountException>(() => cashMachine.Withdraw(-100));
        }

        [Fact]
        public void CannotWithdraw_EmptyCashMachineAndAmount0_ReturnException()
        {
            var cashMachine = cashMachineService.createCashMachine();

            Assert.Throws<InvalidAmountException>(() => cashMachine.Withdraw(0));
        }
    }
}