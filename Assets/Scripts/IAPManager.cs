using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class IAPManager : MonoBehaviour, IStoreListener
{

    public static IAPManager Instance;
    public Button RestoreButton;
    public Text ButtonText;

    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtensionProvider;

    // Use this for initialization
    void Start ()
    {

        Instance = this;

        if (m_StoreController == null)
        {
            Debug.Log("m_StoreController == null");
            //GameHelper.Instance.Log("m_StoreController == null");
            InitializePurchasing();
        }
        
    }


    public void InitializePurchasing()
    {
        if (IsInitialized())
            return;

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        #if UNITY_ANDROID
            builder.AddProduct("Remove_Ads_1_99", ProductType.Subscription, new IDs(){
                { "remove_ads_1_99_android", GooglePlay.Name}
            });
        #elif UNITY_IOS
            builder.AddProduct("Remove_Ads_1_99", ProductType.NonConsumable, new IDs(){
                { "Remove_Ads_1_99", AppleAppStore.Name },
            });
        #endif

        builder.AddProduct("Word_Find_1_99", ProductType.Consumable, new IDs(){
            { "Word_Find_1_99", AppleAppStore.Name },
            { "word_find_1_99", GooglePlay.Name}
        });

        builder.AddProduct("Word_Connect_3_99", ProductType.Consumable, new IDs(){
            { "Word_Connect_3_99", AppleAppStore.Name },
            { "word_find_3_99", GooglePlay.Name}
        });

        builder.AddProduct("Word_Connect_6_99", ProductType.Consumable, new IDs(){
            { "Word_Connect_6_99", AppleAppStore.Name },
            { "word_find_6_99", GooglePlay.Name}
        });

        builder.AddProduct("Word_Connect_13_99", ProductType.Consumable, new IDs(){
            { "Word_Connect_13_99", AppleAppStore.Name },
            { "word_find_13_99", GooglePlay.Name}
        });

        builder.AddProduct("Word_Connect_27_99", ProductType.Consumable, new IDs(){
            { "Word_Connect_27_99", AppleAppStore.Name },
            { "word_find_27_99", GooglePlay.Name}
        });

        builder.AddProduct("Word_Connect_69_99", ProductType.Consumable, new IDs(){
            { "Word_Connect_69_99", AppleAppStore.Name },
            { "word_find_69_99", GooglePlay.Name}
        });

        UnityPurchasing.Initialize(this, builder);

        //GameHelper.Instance.gameStart.Progress += Random.Range(0.15f, 0.2f);
    }

    

    private bool IsInitialized()
    {
        // Only say we are initialized if both the Purchasing references are set.
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    public void BuyProductID(string productId)
    {
        // If Purchasing has been initialized ...
        if (IsInitialized())
        {
            // ... look up the Product reference with the general product identifier and the Purchasing 
            // system's products collection.
            Product product = m_StoreController.products.WithID(productId);

            // If the look up found a product for this device's store and that product is ready to be sold ... 
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                //GameHelper.Instance.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
                // asynchronously.
                m_StoreController.InitiatePurchase(product);
            }
            // Otherwise ...
            else
            {
                // ... report the product look-up failure situation  
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                //GameHelper.Instance.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");

                //CanvasControl.Instance.gameShopping.ShowBuyFailed();
            }
        }
        // Otherwise ...
        else
        {
            // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
            // retrying initiailization.
            Debug.Log("BuyProductID FAIL. Not initialized.");
            //GameHelper.Instance.Log("BuyProductID FAIL. Not initialized.");

            //CanvasControl.Instance.gameShopping.ShowBuyFailed();
            InitializePurchasing();
        }
    }

    public void RestorePurchases()
    {
        // If Purchasing has not yet been set up ...
        if (!IsInitialized())
        {
            // ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        // If we are running on an Apple device ... 
        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            // ... begin restoring purchases
            Debug.Log("RestorePurchases started ...");

            // Fetch the Apple store-specific subsystem.
            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            // Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
            // the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
            apple.RestoreTransactions((result) => {
                // The first phase of restoration. If no more responses are received on ProcessPurchase then 
                // no purchases are available to be restored.
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
        }
        // Otherwise ...
        else
        {
            // We are not running on an Apple device. No work is necessary to restore purchases.
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }

    IEnumerator DelayRestore()
    {
        yield return new WaitForSeconds(1.0f);

        ButtonText.text = "Restore Purchase";
        RestoreButton.interactable = true;
    }


    // ! ! ! Compelete the IStoreListener

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        // Purchasing has succeeded initializing. Collect our Purchasing references.
        Debug.Log("OnInitialized: PASS");
        //GameHelper.Instance.Log("OnInitialized: PASS");

        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;

        
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
        //GameHelper.Instance.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }


    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        // A consumable product has been purchased by this user.
        if (String.Equals(args.purchasedProduct.definition.id, "Word_Find_1_99", StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
            //ScoreManager.score += 100;
            //CanvasControl.Instance.PlayerCoinNumber += 1400;
            GameHelper.player.ChangeCoins(300);
            //GameHelper.player.SetIsPaid(true);

            //CanvasControl.Instance.gameShopping.ShowBuySuccessfully("300");

            //IronSourceControl.Instance.DestroyBanner();
        }
        else if(String.Equals(args.purchasedProduct.definition.id, "Word_Connect_3_99", StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
            //ScoreManager.score += 100;
            //CanvasControl.Instance.PlayerCoinNumber += 1400;
            GameHelper.player.ChangeCoins(720);
            //GameHelper.player.SetIsPaid(true);

            //CanvasControl.Instance.gameShopping.ShowBuySuccessfully("720");

            //IronSourceControl.Instance.DestroyBanner();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, "Word_Connect_6_99", StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
            //ScoreManager.score += 100;
            //CanvasControl.Instance.PlayerCoinNumber += 2500;
            GameHelper.player.ChangeCoins(1320);
            //GameHelper.player.SetIsPaid(true);

            //CanvasControl.Instance.gameShopping.ShowBuySuccessfully("1,320");

            //IronSourceControl.Instance.DestroyBanner();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, "Word_Connect_13_99", StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
            //ScoreManager.score += 100;
            GameHelper.player.ChangeCoins(2880);
            //GameHelper.player.SetIsPaid(true);

            //CanvasControl.Instance.gameShopping.ShowBuySuccessfully("2,880");

            //IronSourceControl.Instance.DestroyBanner();
            //CanvasControl.Instance.PlayerCoinNumber += 7000;
        }
        else if (String.Equals(args.purchasedProduct.definition.id, "Word_Connect_27_99", StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
            //ScoreManager.score += 100;
            GameHelper.player.ChangeCoins(6240);
            //GameHelper.player.SetIsPaid(true);

            //CanvasControl.Instance.gameShopping.ShowBuySuccessfully("6,240");

            //IronSourceControl.Instance.DestroyBanner();
            //CanvasControl.Instance.PlayerCoinNumber += 12000;
        }
        else if (String.Equals(args.purchasedProduct.definition.id, "Word_Connect_69_99", StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
            //ScoreManager.score += 100;
            GameHelper.player.ChangeCoins(18000);
            //GameHelper.player.SetIsPaid(true);

            //CanvasControl.Instance.gameShopping.ShowBuySuccessfully("18,000");

            //IronSourceControl.Instance.DestroyBanner();
            //CanvasControl.Instance.ToolRearrangeNumber += 15;
            //CanvasControl.Instance.ToolRevealNumber += 9;
            //CanvasControl.Instance.ToolRemoveNumber += 3;
            //CanvasControl.Instance.ToolSkipNumber += 2;

        }
        // Or ... a non-consumable product has been purchased by this user.
        else if (String.Equals(args.purchasedProduct.definition.id, "Remove_Ads_1_99", StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));

            //GameHelper.player.SetIsPaid(true);

            //CanvasControl.Instance.gameShopping.ShowBuySuccessfully();

            //IronSourceControl.Instance.DestroyBanner();

            //CanvasControl.Instance._gameLevel.IsHasBought = true;
            //CanvasControl.Instance._gameLevel.CoinLevel1Number = 0;
            //CanvasControl.Instance._gameLevel.CoinLevel2Number = 0;
            //CanvasControl.Instance._gameLevel.CoinLevel3Number = 0;

            //CanvasControl.Instance._gameLevel.SetCoinLevelsState();
        }
        // Or ... a subscription product has been purchased by this user.
        else if (String.Equals(args.purchasedProduct.definition.id, "Pictoword_3_99", StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // TODO: The subscription item has been successfully purchased, grant this to the player.
        }
        // Or ... an unknown product has been purchased by this user. Fill in additional products here....
        else
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));

            //CanvasControl.Instance.gameShopping.ShowBuyFailed();
        }

        // Return a flag indicating whether this product has completely been received, or if the application needs 
        // to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
        // saving purchased products to the cloud, and when that save is delayed. 
        return PurchaseProcessingResult.Complete;
    }


    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
        // this reason with the user to guide their troubleshooting actions.
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));

        //CanvasControl.Instance.gameShopping.ShowBuyFailed();

    }
}
