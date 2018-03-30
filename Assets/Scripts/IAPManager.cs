﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class IAPManager : MonoBehaviour, IStoreListener
{

    public static IAPManager Instance;

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

        builder.AddProduct("Craps_4_99", ProductType.Consumable, new IDs(){
            { "Craps_4_99", AppleAppStore.Name }
        });

        builder.AddProduct("Craps_9_99", ProductType.Consumable, new IDs(){
            { "Craps_9_99", AppleAppStore.Name }
        });

        builder.AddProduct("Craps_19_99", ProductType.Consumable, new IDs(){
            { "Craps_19_99", AppleAppStore.Name }
        });

        builder.AddProduct("Craps_39_99", ProductType.Consumable, new IDs(){
            { "Craps_39_99", AppleAppStore.Name }
        });

        builder.AddProduct("Craps_49_99", ProductType.Consumable, new IDs(){
            { "Craps_49_99", AppleAppStore.Name }
        });

        builder.AddProduct("Craps_99_99", ProductType.Consumable, new IDs(){
            { "Craps_99_99", AppleAppStore.Name }
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

                CanvasControl.Instance.gameStore.ShowPurchasedFailTransform();
            }
        }
        // Otherwise ...
        else
        {
            // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
            // retrying initiailization.
            Debug.Log("BuyProductID FAIL. Not initialized.");
            //GameHelper.Instance.Log("BuyProductID FAIL. Not initialized.");

            CanvasControl.Instance.gameStore.ShowPurchasedFailTransform();
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
        if (String.Equals(args.purchasedProduct.definition.id, "Craps_4_99", StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
            //ScoreManager.score += 100;
            //CanvasControl.Instance.PlayerCoinNumber += 1400;
            GameHelper.player.ChangeCoins(200000);
            GameHelper.player.SetIsPaid(true);

            CanvasControl.Instance.gameStore.ShowPurchasedTransform(200000);

            AppsFlyerManager.Instance.TrackIAP("4.99", "1");

            //IronSourceControl.Instance.DestroyBanner();
        }
        else if(String.Equals(args.purchasedProduct.definition.id, "Craps_9_99", StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
            //ScoreManager.score += 100;
            //CanvasControl.Instance.PlayerCoinNumber += 1400;
            GameHelper.player.ChangeCoins(450000);
            GameHelper.player.SetIsPaid(true);

            CanvasControl.Instance.gameStore.ShowPurchasedTransform(450000);

            AppsFlyerManager.Instance.TrackIAP("9.99", "1");

            //IronSourceControl.Instance.DestroyBanner();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, "Craps_19_99", StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
            //ScoreManager.score += 100;
            //CanvasControl.Instance.PlayerCoinNumber += 2500;
            GameHelper.player.ChangeCoins(1000000);
            GameHelper.player.SetIsPaid(true);

            CanvasControl.Instance.gameStore.ShowPurchasedTransform(1000000);

            AppsFlyerManager.Instance.TrackIAP("19.99", "1");
            //IronSourceControl.Instance.DestroyBanner();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, "Craps_39_99", StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
            //ScoreManager.score += 100;
            GameHelper.player.ChangeCoins(3000000);
            GameHelper.player.SetIsPaid(true);

            CanvasControl.Instance.gameStore.ShowPurchasedTransform(3000000);

            AppsFlyerManager.Instance.TrackIAP("39.99", "1");

            //IronSourceControl.Instance.DestroyBanner();
            //CanvasControl.Instance.PlayerCoinNumber += 7000;
        }
        else if (String.Equals(args.purchasedProduct.definition.id, "Craps_49_99", StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
            //ScoreManager.score += 100;
            GameHelper.player.ChangeCoins(4000000);
            GameHelper.player.SetIsPaid(true);

            CanvasControl.Instance.gameStore.ShowPurchasedTransform(4000000);

            AppsFlyerManager.Instance.TrackIAP("49.99", "1");
            //IronSourceControl.Instance.DestroyBanner();
            //CanvasControl.Instance.PlayerCoinNumber += 12000;
        }
        else if (String.Equals(args.purchasedProduct.definition.id, "Craps_99_99", StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
            //ScoreManager.score += 100;
            GameHelper.player.ChangeCoins(12000000);
            GameHelper.player.SetIsPaid(true);

            CanvasControl.Instance.gameStore.ShowPurchasedTransform(12000000);

            AppsFlyerManager.Instance.TrackIAP("99.99", "1");

        }
        // Or ... a non-consumable product has been purchased by this user.
        else if (String.Equals(args.purchasedProduct.definition.id, "Remove_Ads_1_99", StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));

            GameHelper.player.SetIsPaid(true);

            AppsFlyerManager.Instance.TrackIAP("1.99", "1");

          
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

            CanvasControl.Instance.gameStore.ShowPurchasedFailTransform();
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
        CanvasControl.Instance.gameStore.ShowPurchasedFailTransform();

    }
}
