[![NuGet](https://img.shields.io/nuget/v/HDWallet.Secp256k1)](https://www.nuget.org/packages/HDWallet.Secp256k1/)
[![Build status](https://ci.appveyor.com/api/projects/status/20y31c79trpa6gim?svg=true)](https://ci.appveyor.com/project/farukterzioglu/hdwallet) 

### HD Wallet

A generic HD wallet for Elliptic Curve (Secp256k1) and Edwards-curve (Ed25519) based crypto currencies like Bitcoin, Ethereum, Cosmos, Tezos, Tron, Cardano, Polkadot, Avalanche, FileCoin and more.  
HD wallets can be generated from mnemonic (w/ or w/o passphrase) or from extended private key (xprv) and non-hd wallets can be generated directly from private key.  

HD wallets can derive sub accounts, and from that accounts external (deposit) wallets or internal (change) wallets can be derived. Using generated wallets, addresses can be retrived by implementing address generators for related crypto currency. 

#### Deterministic Address Derivation
An API can be hosted as Docker container or run as .Net Core app and will receive requests for address generation. Generated addresses (or private keys) won't be stored and will be generated when ever queried.  

#### Sign server 
If activated within config, a sign server with receive messages to be signed and in reponse, signature and public key that signed the message will be returned.  

#### SDK (.Net Core)
By using HDWallet.Secp256k1 project, any Elliptic Curve (Secp256k1) based crypto currency wallet can be generated with defining purpose (e.g. BIP44) and coin type (e.g. 195 for Tron).  
HDWallet.Tron project is already ready to go HD wallet for Tron (TRX) which uses HDWallet.Secp256k1 project.  

By using HDWallet.Ed25519 project, any Edwards-curve (Ed25519) based crypto currency wallet can be generated by defining purpose (e.g. BIP44) and coin type (e.g. 1852 for Cardano).     

#### Supported crypto currencies
##### Secp256k1  
Tron, Avalanche, FileCoin  
[WIP] : Bitcoin, Ethereum, Cosmos, Tezos

##### Ed25519  
Polkadot,  
[WIP] : Cardano

### How to use [WIP]
```bash
curl GET 'https://localhost:8080/api/v1/tron/deposit/0'
curl GET 'https://localhost:8080/api/v1/tron/account/0/deposit/0'
curl GET 'https://localhost:8080/api/v1/tron/account/0/change/0'
curl GET 'https://localhost:8080/api/v1/tron/address/validation/TPbBpRXnt6ztse8XkCLiJstZyqQZvxW2sx'

curl -X POST 'https://localhost:8080/api/v1/tron/account/0/address/0/sign' --header 'Content-Type: application/json' --data-raw '{
    "message":"MESSAGE-TO-SIGN"
}'

{
  "signature" : "[SIGNATURE]",
  "publickey" : "[PUBLICKEY]"
}
```

### How to Run 
With Docker [WIP]  
```bash
➜  ~ docker run -e mnemonic="conduct stadium ask orange vast impose depend assume income sail chunk tomorrow life grape dutch" -e passphrase=P@55PHR@S3 -p 8080:80 hdwallet-api 

➜  ~ docker run -e accounthdkey=xprv9xyvwx1jBEBKwjZtXYogBwDyfXTyTa3Af6urV2dU843CyBxLu9J5GLQL4vMWvaW4q3skqAtarUvdGmBoWQZnU2RBLnmJdCM4FnbMa72xWNy -p 8080:80 hdwallet-api 
```

From terminal  
```bash
➜  ~ dotnet hdwallet-api.dll
```

### Authentication
Cookie based [WIP]  
Cookie based authentication activated by default. As the application starts, a cookie file created at `~/.hdwallet/.cookie` (can be configured w/ `--cookiefile` flag). Api user can authenticate with Basic Authentication using credentials in cookie file.  

If one wants to activate sign server feature, a flag is needed as `--signserver`  

### Configuration
#### from appSettings.json
```json
{
  "mnemonic" : "conduct stadium ask orange vast impose depend assume income sail chunk tomorrow life grape dutch",
  "passphrase" : "P@55PHR@S3"
}

{
  "accounthdkey" : "xprv9xyvwx1jBEBKwjZtXYogBwDyfXTyTa3Af6urV2dU843CyBxLu9J5GLQL4vMWvaW4q3skqAtarUvdGmBoWQZnU2RBLnmJdCM4FnbMa72xWNy"
}
```

#### from Environment
```bash
➜  ~ export MNEMONIC="conduct stadium ask orange vast impose depend assume income sail chunk tomorrow life grape dutch"
➜  ~ export PASSPHRASE=P@55PHR@S3
➜  ~ export ACCOUNTHDKEY=xprv9xyvwx1jBEBKwjZtXYogBwDyfXTyTa3Af6urV2dU843CyBxLu9J5GLQL4vMWvaW4q3skqAtarUvdGmBoWQZnU2RBLnmJdCM4FnbMa72xWNy

➜  ~ dotnet hdwallet-api.dll
```
#### from parameters
```bash
➜  ~ dotnet hdwallet-api.dll --mnemonic "conduct stadium ask orange vast impose depend assume income sail chunk tomorrow life grape dutch" --passphrase P@55PHR@S3

➜  ~ dotnet hdwallet-api.dll --accounthdkey xprv9xyvwx1jBEBKwjZtXYogBwDyfXTyTa3Af6urV2dU843CyBxLu9J5GLQL4vMWvaW4q3skqAtarUvdGmBoWQZnU2RBLnmJdCM4FnbMa72xWNy
```

### SDK Usage (.Net) 
Sample for generating Secp256k1 HD Wallet for Bitcoin (purpose: BIP44, coin type: 0) from mnemonic and getting the first account's first deposit wallet;  
```csharp
IHDWallet<BitcoinWallet> bitcoinHDWallet = new BitcoinHDWallet("conduct stadium ask orange vast impose depend assume income sail chunk tomorrow life grape dutch", "");
var depositWallet0 = bitcoinHDWallet.GetAccount(0).GetExternalWallet(0);        
var address = depositWallet0.Address;
```  

Sample for generating HD Wallet (m/44'/0'/0') from extended private key;  
```csharp
var accountExtendedPrivateKey = "xprv9xyvwx1jBEBKwjZtXYogBwDyfXTyTa3Af6urV2dU843CyBxLu9J5GLQL4vMWvaW4q3skqAtarUvdGmBoWQZnU2RBLnmJdCM4FnbMa72xWNy";
IAccountHDWallet<BitcoinWallet> accountHDWallet = new AccountHDWallet<BitcoinWallet>(accountExtendedPrivateKey, 0);

var depositWallet0 = accountHDWallet.Account.GetExternalWallet(0);
```

Sample for signing messages with generated wallets;  
```csharp
...
var signature = depositWallet0.Sign(messageBytes);
```

### Build the api   
```bash
docker build -f src/HDWallet.Api/Dockerfile -t hdwallet-api .
```