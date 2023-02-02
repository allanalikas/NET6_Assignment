using System;
using Xunit;
using CashMachine.Services;

namespace CashMachine.UnitTests.Services
{
    public class CashMachineService_CannotInsert
    {
        private readonly CashMachineService cashMachineService = new CashMachineService();

        [Fact]
        public void CannotInsert_NoBanknotes_ReturnException()
        {
            var cashMachine = cashMachineService.createCashMachine();

            Assert.Throws<InvalidAmountException>(() => cashMachine.Insert(new int[]{}));
        }

        [Fact]
        public void CannotInsert_NegativeBanknotes_ReturnException()
        {
            var cashMachine = cashMachineService.createCashMachine();

            Assert.Throws<InvalidBanknoteException>(() => cashMachine.Insert(new int[]{-100, -50, -10}));
        }

        [Fact]
        public void CannotInsert_BanknotesThatContain0_ReturnException()
        {
            var cashMachine = cashMachineService.createCashMachine();

            Assert.Throws<InvalidBanknoteException>(() => cashMachine.Insert(new int[]{100, 0, 50}));
        }

                [Fact]
        public void CannotInsert_BanknotesThatAreNotSupported_ReturnException()
        {
            var cashMachine = cashMachineService.createCashMachine();

            Assert.Throws<InvalidBanknoteException>(() => cashMachine.Insert(new int[]{100, 0, 50, 49}));
        }
    }
}