using HDWallet.Core;
using HDWallet.Secp256k1;
using NUnit.Framework;
using NUnit.Framework.Legacy;


namespace HDWallet.Tron.Tests
{
    public class GenerateAccount
    {
        [Test]
        public void ShouldCreateAccount()
        {
            string words = "conduct stadium ask orange vast impose depend assume income sail chunk tomorrow life grape dutch";
            IHDWallet<TronWallet> wallet = new TronHDWallet(words);
            var account0wallet0 = wallet.GetAccount(0).GetExternalWallet(0); // m/44'/195'/0'/0/0
            ClassicAssert.AreEqual("031a97d1707d7cc37a1e61830554a40c47edc7fb03a4098fdfa690020376d99870", account0wallet0.PublicKey.ToHex());

            // TMQ3RtdjwCCoeA2RAYiTrFNZTKtzh5t9YQ

            // Account Extended Private Key for m/44'/195'/0';
            var accountExtendedPrivateKey = "xprv9yB7gYqxZdR4AUGppodn1XL7RpJkRUnDE1fM6oEY4LQrvstH1qCdfFHmW9msdqAsPEPqr9LhYmw2nZQfk8uBbk1KYhzjNVzWdwsugTTgNvc";
            IAccount<TronWallet> account = new Account<TronWallet>(accountExtendedPrivateKey, NBitcoin.Network.Main);
            
            // m/44'/195'/0'/0/0
            var depositWallet0 = account.GetExternalWallet(0);
            ClassicAssert.AreEqual("031a97d1707d7cc37a1e61830554a40c47edc7fb03a4098fdfa690020376d99870", depositWallet0.PublicKey.ToHex());

            ClassicAssert.AreEqual(account0wallet0.PublicKey, depositWallet0.PublicKey);
        }

        [Test]
        public void ShouldCreateAddressFromMasterKey()
        {
            var accountExtendedPrivateKey = "xprv9yB7gYqxZdR4AUGppodn1XL7RpJkRUnDE1fM6oEY4LQrvstH1qCdfFHmW9msdqAsPEPqr9LhYmw2nZQfk8uBbk1KYhzjNVzWdwsugTTgNvc";
            IAccount<TronWallet> account = new Account<TronWallet>(accountExtendedPrivateKey, NBitcoin.Network.Main);
            var depositWallet0 = account.GetExternalWallet(0);
            ClassicAssert.AreEqual("031a97d1707d7cc37a1e61830554a40c47edc7fb03a4098fdfa690020376d99870", depositWallet0.PublicKey.ToHex());
            ClassicAssert.AreEqual("TMQ3RtdjwCCoeA2RAYiTrFNZTKtzh5t9YQ", depositWallet0.Address);
        }

        [Test]
        public void ShouldCreateTronWalletFromMasterKey()
        {
            var accountExtendedPrivateKey = "xprv9yB7gYqxZdR4AUGppodn1XL7RpJkRUnDE1fM6oEY4LQrvstH1qCdfFHmW9msdqAsPEPqr9LhYmw2nZQfk8uBbk1KYhzjNVzWdwsugTTgNvc";
            var account = TronHDWallet.GetAccountFromMasterKey(accountExtendedPrivateKey);
            var depositWallet0 = account.GetExternalWallet(0);
            ClassicAssert.AreEqual("TMQ3RtdjwCCoeA2RAYiTrFNZTKtzh5t9YQ", depositWallet0.Address);
        }
    }
}