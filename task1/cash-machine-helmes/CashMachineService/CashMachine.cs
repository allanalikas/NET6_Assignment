namespace CashMachine.Services {
    public class CashMachine : ICashMachine
    {
        private Dictionary<int, int> cashMachineBanknotes { get; set; } = new Dictionary<int, int>();
        private int cashMachineBalance { get; set; } = 0;
        private List<int> acceptableBanknotes = new List<int>{100, 50, 20, 10, 5};

        public void Insert(int[] cash)
        { 
            // Currently making the assumption that if one of the banknotes is invalid, then the insert is rejected for all of the banknotes.
            validateInsertedBanknotes(cash);

            cashMachineBalance += addInsertedBanknotesToCashMachine(cash);
        }

        public int Withdraw(int amount)
        {
            // Currently returning the withdrawn amount as I don't think it would be reasonable to return the cash machine balance to a user.
            if(amount <= 0) {
                throw new InvalidAmountException(String.Format("You cannot withdraw a negative amount from the cash machine : {0}", amount));
            }
            return findWithdrawnAmount(amount);
        }

        private int addInsertedBanknotesToCashMachine(int[] cash)
        {
            int insertedAmount = 0;
            List<int> insertedBanknotes = new List<int>();

            foreach (int amount in cash)
            {
                insertedBanknotes.Add(amount);
                insertedAmount += amount;
            }

            insertBanknotesToCashMachine(insertedBanknotes);

            return insertedAmount;
        }

        private void insertBanknotesToCashMachine(List<int> insertedBanknotes)
        {
            foreach (int banknote in insertedBanknotes)
            {
                if (!cashMachineBanknotes.ContainsKey(banknote))
                {
                    cashMachineBanknotes.Add(banknote, 1);
                }
                else
                {
                    cashMachineBanknotes[banknote]++;
                }
            }
        }

        private int findWithdrawnAmount(int amount)
        {
            if (amount > cashMachineBalance)
            {
                throw new NotEnoughFundsException(String.Format("The cash machine does not have enough balance to withdraw {0}", amount));
            }

            // Copy dictionary in case we don't have the banknotes to give out the requested amount.
            Dictionary<int, int> availableBanknotes = new Dictionary<int, int>(cashMachineBanknotes);
            int currentAmount = amount;

            currentAmount = findWithdrawableAmount(availableBanknotes, currentAmount);

            if (currentAmount != 0)
            {
                throw new NotEnoughBanknotesException(String.Format("The cash machine does not have the corresponding banknotes to withdraw {0}", amount));
            }

            updateCashMachine(amount, availableBanknotes);

            return amount;
        }

        private void updateCashMachine(int amount, Dictionary<int, int> availableBanknotes)
        {
            cashMachineBalance = (cashMachineBalance - amount);
            cashMachineBanknotes = availableBanknotes;
        }

        private int findWithdrawableAmount(Dictionary<int, int> availableBanknotes, int currentAmount)
        {
            foreach (int banknote in acceptableBanknotes)
            {
                int banknoteCount = 0;
                if (availableBanknotes.TryGetValue(banknote, out banknoteCount) && banknoteCount > 0)
                {
                    int usedBanknoteCount = currentAmount / banknote > banknoteCount ? banknoteCount : currentAmount / banknote;
                    currentAmount -= usedBanknoteCount * banknote;
                    availableBanknotes[banknote] -= usedBanknoteCount;
                }
            }

            return currentAmount;
        }

        private void validateInsertedBanknotes(int[] cash) {
            if(cash.Count() == 0) {
                throw new InvalidAmountException(String.Format("You cannot call insert with no bank notes"));
            }
            foreach(int banknote in cash) {
                if(banknote <= 0) {
                    throw new InvalidBanknoteException(String.Format("You cannot insert negative or 0 amounts into the cash machine : {0}", banknote));
                }
                if(!acceptableBanknotes.Contains(banknote)) {
                    throw new InvalidBanknoteException(String.Format("The cash machine cannot accept a banknote worth : {0}", banknote));
                }
            }
        }
    }
}