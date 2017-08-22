using System;

namespace ClearentTest
{
    class InterestCalculator
    {
        public decimal CardHolderInterest(CardHolder CardHolder)
        {
            try
            {
                if (CardHolder == null)
                {
                    throw new Exception("No CardHolder object received");
                }
                decimal returnVal = 0;
                foreach (Wallet Wallet in CardHolder.Wallets)
                {
                    foreach (Card Card in Wallet.Cards)
                    {
                        returnVal += Card.InterestRate * Card.Balance;
                    }
                }
                return returnVal;
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public decimal PerWalletInterest(CardHolder CardHolder, int WalletID)
        {
            try
            {
                if (CardHolder == null)
                {
                    throw new Exception("No CardHolder object received");
                }
                if (WalletID < 1)
                {
                    throw new Exception("Invalid Wallet ID");
                }
                decimal returnVal = 0;
                Wallet Wallet = CardHolder.Wallets.Find(item => item.ID == WalletID);
                foreach (Card Card in Wallet.Cards)
                {
                    returnVal += Card.InterestRate * Card.Balance;
                }
                return returnVal;
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public decimal PerCardInterest(CardHolder CardHolder, int CardID)
        {
            try
            {
                if (CardHolder == null)
                {
                    throw new Exception("No CardHolder object received");
                }
                if (CardID < 1)
                {
                    throw new Exception("Invalid Card ID");
                }
                decimal returnVal = 0;
                foreach (Wallet Wallet in CardHolder.Wallets)
                {
                    Card Card = Wallet.Cards.Find(item => item.ID == CardID);
                    returnVal += Card.InterestRate * Card.Balance;
                }
                return returnVal;
            }
            catch (Exception e)
            {
                return -1;
            }
        }
    }
}
