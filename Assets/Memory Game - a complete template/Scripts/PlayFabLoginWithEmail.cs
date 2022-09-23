using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
public class PlayFabLoginWithEmail : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Interface")]
    public InputField password_text;
    public InputField confirm_password_text;
    public InputField email_text;
    public InputField username_text;
    public Text messageText;
    private int valorOperacao;

    void Start()
    {

        //register();
        login();


    }


    public void register()
    {

        //if (password_text.text.Length < 6)
        //{
        //    messageText.text = "Maior que 6";
        //    return;


        //}

        //if (password_text.text == confirm_password_text.text)
        //{
        var request = new RegisterPlayFabUserRequest
        {
            // Username = username_text.text,
            //Password = password_text.text,
            // Email = email_text.text,
            //DisplayName= username_text.text
            Username = "baroniHotmail2",
            Password = "teste2",
            Email = "wcampello2@hotmail.com",
            DisplayName = "wbaronic4"
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, On_Register_successful, register_failed);
        //}

    }


    public void login()
    {
        var request = new LoginWithEmailAddressRequest
        {
            //   Email = email_text.text,
            //   Password = password_text.text

            Password = "teste2",
            Email = "wbaronic2@gmail.com",
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }

        };


        PlayFabClientAPI.LoginWithEmailAddress(request, On_Login_successful, register_failed);

    }

    void getUserData()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), ondatarecived, register_failed);
    }

    void ondatarecived(GetUserDataResult result)
    {
        // Debug.Log(result.Data[);

        Dictionary<string, UserDataRecord> dados = result.Data;

        foreach (var item in dados)
        {
            Debug.Log(item.Key);
            Debug.Log(item.Value.Value);


        }

    }
    void savaUserData()
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {"win","5" },
                {"kills","7" }
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OndataSend, register_failed);
    }

    void OndataSend(UpdateUserDataResult result)
    {
        Debug.Log("salvou no Servidor");
    }
    void On_Login_successful(LoginResult result)
    {
        //Debug.Log(result.InfoResultPayload.PlayerProfile.);
        Debug.Log("Deu certo login");
        //  getUserData();
        // getVirtualCurrency();
        // buyCoins(1000);
       //  MakeOnePurchase(10);
      // ConsumeTank(1);
     // GetInventory();
        //GetInventory();
   

    }
    public void resetPasswordButton()
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            //Email = email_text.text,
            Email = "wcampello@hotmail.com",
            TitleId = "86FDC"
           
        };

        PlayFabClientAPI.SendAccountRecoveryEmail(request, onPAsswordReset, register_failed);
        PlayerPrefs.GetString("displayname");

    }


    public void onPAsswordReset(SendAccountRecoveryEmailResult result)
    {
        //  messageText.text = "olha teu email";
        Debug.Log("ver-email");
    }


    void On_Register_successful(RegisterPlayFabUserResult result)
    {
        // messageText.text = "Deu certo";
        Debug.Log("deu certo");
    }

    void register_failed(PlayFabError error)
    {
        // messageText.text = error.ErrorMessage;
        Debug.Log(error.ErrorMessage);
    }

    #region
    public void getVirtualCurrency()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), onGetVirtualCurrencySuccess, register_failed);

    }
    public Button[] botoes;
    string selecionado;
  

    void spendCoins()
    {
        var request = new SubtractUserVirtualCurrencyRequest
        {
            VirtualCurrency = "CN",
            Amount = 2
        };

        PlayFabClientAPI.SubtractUserVirtualCurrency(request, compraDeMoedaCorreta, register_failed);
        //PlayFabClientAPI.AddUserVirtualCurrency(request, onSpend, register_failed);
    }


    public void buyCoins(int coins)
    {
        var request = new AddUserVirtualCurrencyRequest
        {
            VirtualCurrency = "CN",
            Amount = coins
        };

        //PlayFabClientAPI.SubtractUserVirtualCurrency(request, onSpend, register_failed);
        PlayFabClientAPI.AddUserVirtualCurrency(request, compraDeMoedaCorreta, register_failed);
    }
    void compraDeMoedaCorreta(ModifyUserVirtualCurrencyResult result)
    {


        MakeCoinPurchase(result.BalanceChange);

        Debug.Log("Ganhou " + result.VirtualCurrency);
    }

    void MakeCoinPurchase(int price)
    {

        int precoComprado = price;
        int precoUnicoMoeda = 200;
        int x = precoComprado / precoUnicoMoeda;

        for (int i = 0; i < x; i++)
        {
            Debug.Log("Make  Coin purchase");
            PlayFabClientAPI.PurchaseItem(new PurchaseItemRequest
            {
                // In your game, this should just be a constant matching your primary catalog
                CatalogVersion = "basico",
                ItemId = "coins",
                Price = 10,
                VirtualCurrency = "RM",


            }, LogSuccess, LogFailure);

        }


    }

    void LogSuccess(PurchaseItemResult result)
    {

        var requestName = result.Request.GetType().Name;
        Debug.Log(requestName + " successful");
    }


    void onGetVirtualCurrencySuccess(GetUserInventoryResult result)
    {
        int coins = result.VirtualCurrency["CN"];
        Debug.Log(coins.ToString());

    }
    #endregion


    #region

    // **** Shared example utility functions ****

    // This is typically NOT how you handle success
    // You will want to receive a specific result-type for your API, and utilize the result parameters
  

    void LogSuccessConsumeItem(ConsumeItemResult result)
    {
        var requestName = result.Request.GetType().Name;
        Debug.Log(requestName + " successful");
    }

    // Error handling can be very advanced, such as retry mechanisms, logging, or other options
    // The simplest possible choice is just to log it
    void LogFailure(PlayFabError error)
    {

        Debug.LogError(error.GenerateErrorReport());
    }

    public void comprarMoeda(int quantidade)
    {

        buyCoins(quantidade);

    }


    void MakeOnePurchase(int price)
    {

        Debug.Log("Make  200moedas  purchase");
        PlayFabClientAPI.PurchaseItem(new PurchaseItemRequest
        {
            // In your game, this should just be a constant matching your primary catalog
            CatalogVersion = "basico",
            ItemId = "200moedas",
            Price = 10,
            VirtualCurrency = "CN",
            
        }, LogSuccess, LogFailure);
    }

    

    void GetInventory()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), LogSuccessInventory, LogFailure);
    }
    void LogSuccessInventory(GetUserInventoryResult inventoryResult)
    {
       Debug.Log( inventoryResult.VirtualCurrency["CN"]);
       // Debug.Log(inventoryResult.VirtualCurrencyRechargeTimes["CN"]);


        List<ItemInstance> inventory = inventoryResult.Inventory;


        if (!inventory.Exists(x => x.ItemId.Equals("poder"))) {
            Debug.Log("poder n�o existe");
        }
        


        foreach (ItemInstance item in inventory)
        {
            Debug.Log(item.ItemId);
            if (item.ItemId.Equals("retirarpropaganda"))
            {
                Debug.Log(item.ItemId);


                ConsumeTank(item.ItemInstanceId);
                break;
            }


        }
    }
    void ConsumeTank(string  id)
    {
        Debug.Log("ConsumindoTanque");
        PlayFabClientAPI.ConsumeItem(new ConsumeItemRequest
        {
            ConsumeCount = 2,
            // This is a hex-string value from the GetUserInventory result
            ItemInstanceId = id
        }, LogSuccessConsumeItem, LogFailure);
    }
    #endregion
}