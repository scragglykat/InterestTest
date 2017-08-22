using System.Collections.Generic;
using NUnit.Framework;

namespace ClearentTest
{
    static class Globals
    {
        //Setting global values for cards. These would normally be pulled from a data source, with a table for card types, and then user based
        // tables containing varying interest rates and balances
        public static Card VISA = new Card() { ID = 1, ProviderName = "VISA", InterestRate = .10m, Balance = 100m };
        public static Card MC = new Card() { ID = 2, ProviderName = "MasterCard", InterestRate = .05m, Balance = 100m };
        public static Card Discover = new Card() { ID = 3, ProviderName = "Discover", InterestRate = .01m, Balance = 100m };
    }

    [TestFixture]
    public class CalculateInterest
    {
        [Test]
        public void CalcFirstScenario()
        {
            //Initializing wallet and cardholder values. These would normally be pulled from a data source.
            Wallet Wallet1 = new Wallet() { ID = 1, Cards = new List<Card>(new Card[] { Globals.VISA, Globals.MC, Globals.Discover }) };
            CardHolder CardHolder = new CardHolder() { ID = 1, Name = "CardHolder1", Wallets = new List<Wallet>() { Wallet1 } };
            //calculating and testing values
            InterestCalculator Calc = new InterestCalculator();
            decimal CardHolderInterest = Calc.CardHolderInterest(CardHolder);
            decimal VISAInterest = Calc.PerCardInterest(CardHolder, 1);
            decimal MCInterest = Calc.PerCardInterest(CardHolder, 2);
            decimal DiscoverInterest = Calc.PerCardInterest(CardHolder, 3);
            Assert.AreEqual(16m, CardHolderInterest);
            Assert.AreEqual(10m, VISAInterest);
            Assert.AreEqual(5m, MCInterest);
            Assert.AreEqual(1m, DiscoverInterest);
        }

        [Test]
        public void CalcSecondScenario()
        {
            //Initializing wallet and cardholder values. These would normally be pulled from a data source.
            Wallet Wallet1 = new Wallet() { ID = 1, Cards = new List<Card>(new Card[] { Globals.VISA, Globals.Discover }) };
            Wallet Wallet2 = new Wallet() { ID = 2, Cards = new List<Card>(new Card[] { Globals.MC }) };
            CardHolder CardHolder = new CardHolder() { ID = 1, Name = "CardHolder1", Wallets = new List<Wallet>() { Wallet1, Wallet2 } };
            //calculating and testing values
            InterestCalculator Calc = new InterestCalculator();
            decimal CardHolderInterest = Calc.CardHolderInterest(CardHolder);
            decimal FirstWalletInterest = Calc.PerWalletInterest(CardHolder, 1);
            decimal SecondWalletInterest = Calc.PerWalletInterest(CardHolder, 2);
            Assert.AreEqual(16m, CardHolderInterest);
            Assert.AreEqual(11m, FirstWalletInterest);
            Assert.AreEqual(5m, SecondWalletInterest);
        }

        [Test]
        public void CalcThirdScenario()
        {
            //Initializing wallet and cardholder values. These would normally be pulled from a data source.
            Wallet Wallet1 = new Wallet() { ID = 1, Cards = new List<Card>(new Card[] { Globals.VISA, Globals.MC, Globals.Discover }) };
            Wallet Wallet2 = new Wallet() { ID = 1, Cards = new List<Card>(new Card[] { Globals.VISA, Globals.MC }) };
            CardHolder CardHolder1 = new CardHolder() { ID = 1, Name = "CardHolder1", Wallets = new List<Wallet>() { Wallet1 } };
            CardHolder CardHolder2 = new CardHolder() { ID = 2, Name = "CardHolder2", Wallets = new List<Wallet>() { Wallet2 } };
            //calculating and testing values
            InterestCalculator Calc = new InterestCalculator();
            decimal FirstCardHolderInterest = Calc.CardHolderInterest(CardHolder1);
            decimal SecondCardHolderInterest = Calc.CardHolderInterest(CardHolder2);
            decimal FirstCardHolderWalletInterest = Calc.PerWalletInterest(CardHolder1, 1);
            decimal SecondCardHolderWalletInterest = Calc.PerWalletInterest(CardHolder2, 1);
            Assert.AreEqual(16m, FirstCardHolderInterest);
            Assert.AreEqual(15m, SecondCardHolderInterest);
            Assert.AreEqual(16m, FirstCardHolderWalletInterest);
            Assert.AreEqual(15m, SecondCardHolderWalletInterest);
        }
    }
}
