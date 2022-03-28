namespace HDWallet.Core
{
    public interface IWallet
    {
        string Address { get; }

        Signature Sign(byte[] message);

        byte[] PrivateKeyBytes { get; set; }

        byte[] PublicKeyBytes { get; }

        uint Index { get; set; }

        bool Verify(byte[] message, Signature sig);
    }
}