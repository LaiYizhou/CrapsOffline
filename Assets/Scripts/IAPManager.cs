using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum EMarketProduct
{
    StoreSale,
    LoginSale,
    CloseShopSale,
    BackToHallSale,
    RemoveAdSale,

    Scratcher499Sale,
}

public class MarketProduct
{
    public MarketProduct(string id, ProductType type, string appleAppStoreName, long originalChipAmount, long chipAmount, double price, EMarketProduct eMarketProduct)
    {
        Id = id;
        Type = type;
        AppleAppStoreName = appleAppStoreName;
        OriginalChipAmount = originalChipAmount;
        ChipAmount = chipAmount;
        Price = price;
        EMarketProduct = eMarketProduct;
    }

    public string Id { get; private set; }

    public EMarketProduct EMarketProduct { get; private set; }

    public ProductType Type { get; private set; }
    public string AppleAppStoreName { get; private set; }
    public long OriginalChipAmount { get; private set; }

    public long ChipAmount { get; private set; }

    public double Price { get; private set; }


}

public class IAPManager : MonoBehaviour, IStoreListener
{

    public static IAPManager Instance;

    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtensionProvider;

    private List<MarketProduct> marketProductList = new List<MarketProduct>()
    {
        new MarketProduct("Remove_Ads_1_99", ProductType.NonConsumable, "Remove_Ads_1_99", -1, 0, 1.99, EMarketProduct.StoreSale),
        new MarketProduct("Craps_4_99", ProductType.Consumable, "Craps_4_99", -1, 200000, 4.99, EMarketProduct.StoreSale),
        new MarketProduct("Craps_9_99", ProductType.Consumable, "Craps_9_99", -1, 450000, 9.99, EMarketProduct.StoreSale),
        new MarketProduct("Craps_19_99", ProductType.Consumable, "Craps_19_99", -1, 1000000, 19.99, EMarketProduct.StoreSale),
        new MarketProduct("Craps_39_99", ProductType.Consumable, "Craps_39_99", -1, 3000000, 39.99, EMarketProduct.StoreSale),
        new MarketProduct("Craps_49_99", ProductType.Consumable, "Craps_49_99", -1, 4000000, 49.99, EMarketProduct.StoreSale),
        new MarketProduct("Craps_99_99", ProductType.Consumable, "Craps_99_99", -1, 12000000, 99.99, EMarketProduct.StoreSale),

        new MarketProduct("Craps_login_sale_4_99", ProductType.Consumable, "Craps_login_sale_4_99", 200000, 600000, 4.99, EMarketProduct.LoginSale),
        new MarketProduct("Craps_login_sale_9_99", ProductType.Consumable, "Craps_login_sale_9_99", 450000, 1350000, 9.99, EMarketProduct.LoginSale),
        new MarketProduct("Craps_login_sale_19_99", ProductType.Consumable, "Craps_login_sale_19_99", 1000000, 3000000, 19.99, EMarketProduct.LoginSale),

        new MarketProduct("Craps_closeshop__4_99", ProductType.Consumable, "Craps_closeshop__4_99", 200000, 400000, 4.99, EMarketProduct.CloseShopSale),
        new MarketProduct("Craps_closeshop_9_99", ProductType.Consumable, "Craps_closeshop_9_99", 450000, 900000, 9.99, EMarketProduct.CloseShopSale),
        new MarketProduct("Craps_closeshop_19_99", ProductType.Consumable, "Craps_closeshop_19_99", 1000000, 2000000, 19.99, EMarketProduct.CloseShopSale),
        new MarketProduct("Craps_closeshop_49_99", ProductType.Consumable, "Craps_closeshop_49_99", 4000000, 8000000, 49.99, EMarketProduct.CloseShopSale),
        new MarketProduct("Craps_closeshop_99_99", ProductType.Consumable, "Craps_closeshop_99_99", 12000000, 24000000, 99.99, EMarketProduct.CloseShopSale),

        new MarketProduct("Craps_returnlobby_4_99", ProductType.Consumable, "Craps_returnlobby_4_99", 200000, 400000, 4.99, EMarketProduct.BackToHallSale),
        new MarketProduct("Craps_returnlobby_9_99", ProductType.Consumable, "Craps_returnlobby_9_99", 450000, 900000, 9.99, EMarketProduct.BackToHallSale),
        new MarketProduct("Craps_returnlobby_19_99", ProductType.Consumable, "Craps_returnlobby_19_99", 1000000, 2000000, 19.99, EMarketProduct.BackToHallSale),
        new MarketProduct("Craps_returnlobby_49_99", ProductType.Consumable, "Craps_returnlobby_49_99", 4000000, 8000000, 49.99, EMarketProduct.BackToHallSale),
        new MarketProduct("Craps_returnlobby_99_99", ProductType.Consumable, "Craps_returnlobby_99_99", 12000000, 24000000, 99.99, EMarketProduct.BackToHallSale),

        new MarketProduct("Craps_removeads_sale_4_99", ProductType.Consumable, "Craps_removeads_sale_4_99", 200000, 400000, 4.99, EMarketProduct.RemoveAdSale),
        new MarketProduct("Craps_removeads_sale_9_99", ProductType.Consumable, "Craps_removeads_sale_9_99", 450000, 900000, 9.99, EMarketProduct.RemoveAdSale),

        new MarketProduct("Craps_4_99_scratcher", ProductType.Consumable, "Craps_4_99_scratcher", -1, -1, 4.99, EMarketProduct.Scratcher499Sale)

    };

    private Dictionary<string, MarketProduct> marketProductDictionary;
    private Dictionary<EMarketProduct, List<MarketProduct>> marketProductEDictionary;

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


        // Store Product
        //#if UNITY_ANDROID
        //        builder.AddProduct("Remove_Ads_1_99", ProductType.Subscription, new IDs(){
        //            { "remove_ads_1_99_android", GooglePlay.Name}
        //        });
        //#elif UNITY_IOS
        //        builder.AddProduct("Remove_Ads_1_99", ProductType.NonConsumable, new IDs(){
        //            { "Remove_Ads_1_99", AppleAppStore.Name },
        //        });
        //#endif

        marketProductDictionary = new Dictionary<string, MarketProduct>();
        marketProductEDictionary = new Dictionary<EMarketProduct, List<MarketProduct>>();

        for (int i = 0; i < marketProductList.Count; i++)
        {
            MarketProduct marketProduct = marketProductList[i];

            marketProductDictionary.Add(marketProduct.Id, marketProduct);

            if (marketProductEDictionary.ContainsKey(marketProduct.EMarketProduct))
            {
                marketProductEDictionary[marketProduct.EMarketProduct].Add(marketProduct);
            }
            else
            {
                List<MarketProduct> list = new List<MarketProduct>();
                list.Add(marketProduct);
                marketProductEDictionary.Add(marketProduct.EMarketProduct, list);
            }

            builder.AddProduct(marketProduct.Id, marketProduct.Type, new IDs(){
                { marketProduct.AppleAppStoreName, AppleAppStore.Name },
            });
        }

        UnityPurchasing.Initialize(this, builder);
        
    }

    

    private bool IsInitialized()
    {
        // Only say we are initialized if both the Purchasing references are set.
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    public MarketProduct GetMarketProduct(EMarketProduct eMarketProduct)
    {
        if (marketProductEDictionary.ContainsKey(eMarketProduct) && marketProductEDictionary[eMarketProduct]!=null)
        {
            int count = marketProductEDictionary[eMarketProduct].Count;
            int index = Random.Range(0, count);
            return marketProductEDictionary[eMarketProduct][index];
        }
        else
        {
            return null;
        }
    }

    public MarketProduct GetMarketProduct(string id)
    {
        if (marketProductDictionary != null && marketProductDictionary.ContainsKey(id))
        {
            return marketProductDictionary[id];
        }
        else
        {
            return null;
        }
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
                IronSourceControl.IsShowInterstitalWhenBack = false;
                m_StoreController.InitiatePurchase(product);

#if UNITY_EDITOR
                StartCoroutine(FakeProcessPurchase(marketProductDictionary[product.definition.id]));
#endif

            }
            // Otherwise ...
            else
            {
                // ... report the product look-up failure situation  
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                //GameHelper.Instance.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");

                GameHelper.Instance.purchaseMessage.ShowPurchasedFailTransform();
            }
        }
        // Otherwise ...
        else
        {
            // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
            // retrying initiailization.
            Debug.Log("BuyProductID FAIL. Not initialized.");
            //GameHelper.Instance.Log("BuyProductID FAIL. Not initialized.");

            GameHelper.Instance.purchaseMessage.ShowPurchasedFailTransform();
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


    private IEnumerator FakeProcessPurchase(MarketProduct marketProduct)
    {
        yield return new WaitForSeconds(0.8f);

        bool isPurchaseSuccessful = false;

        foreach (var pair in marketProductDictionary)
        {
            if (String.Equals(marketProduct.Id, pair.Key, StringComparison.Ordinal))
            {

                Debug.Log(string.Format("FakeProcessPurchase: PASS. Product: '{0}'", marketProduct.Id));

                isPurchaseSuccessful = true;
                GameHelper.player.SetIsPaid(true);

                if (marketProduct.Type == ProductType.NonConsumable)
                {
                    GameHelper.Instance.purchaseMessage.ResetAllTransforms();
                }
                else
                {
                    if (marketProduct.Id == "Craps_4_99_scratcher")
                    {
                        GameHelper.Instance.purchaseMessage.ShowScratcherPurchasedTransform();
                    }
                    else
                    {
                        GameHelper.Instance.coinCollectEffect.RunEffect(marketProduct.ChipAmount,
                            new Vector3(0.0f, 0.0f),
                            new Vector3(-95.0f, 100.0f),
                            new Vector3(-140.0f, 190));

                        GameHelper.Instance.purchaseMessage.ShowPurchasedTransform(marketProduct.ChipAmount);
                    }

                }

                break;
            }
        }

        if (!isPurchaseSuccessful)
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", marketProduct.Id));

            GameHelper.Instance.purchaseMessage.ShowPurchasedFailTransform();
        }


    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {

        Debug.Log("transactionID : " + args.purchasedProduct.transactionID);

        bool isPurchaseSuccessful = false;
        bool isCheat = IsCheat(args.purchasedProduct.transactionID);
        bool isSandBox = IsSandBox(args.purchasedProduct.transactionID);

        if (isCheat)
        {
            Debug.Log(string.Format("ProcessPurchase: InValid. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
            return PurchaseProcessingResult.Complete;
        }

        foreach (var pair in marketProductDictionary)
        {
            if (String.Equals(args.purchasedProduct.definition.id, pair.Key, StringComparison.Ordinal))
            {

                MarketProduct marketProduct = pair.Value;
                Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));

                isPurchaseSuccessful = true;
                GameHelper.player.SetIsPaid(true);

                if (marketProduct.Type == ProductType.NonConsumable)
                {
                    GameHelper.Instance.purchaseMessage.ResetAllTransforms();
                }
                else
                {

                    if (args.purchasedProduct.definition.id == "Craps_4_99_scratcher")
                    {
                        GameHelper.Instance.purchaseMessage.ShowScratcherPurchasedTransform();
                    }
                    else
                    {
                        GameHelper.Instance.coinCollectEffect.RunEffect(marketProduct.ChipAmount,
                            new Vector3(0.0f, 0.0f),
                            new Vector3(-95.0f, 100.0f),
                            new Vector3(-140.0f, 190));

                        GameHelper.Instance.purchaseMessage.ShowPurchasedTransform(marketProduct.ChipAmount);
                    }

                  
                }


                if (!isSandBox)
                    AppsFlyerManager.Instance.TrackIAP(marketProduct.Price.ToString(), "1");

                break;
            }
        }

        if (!isPurchaseSuccessful)
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));

            GameHelper.Instance.purchaseMessage.ShowPurchasedFailTransform();
        }

        return PurchaseProcessingResult.Complete;

    }


    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
        // this reason with the user to guide their troubleshooting actions.
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));

        //CanvasControl.Instance.gameShopping.ShowBuyFailed();
        GameHelper.Instance.purchaseMessage.ShowPurchasedFailTransform();

    }

    private bool IsCheat(string transactionID)
    {
        Regex r = new Regex(@"^\d+$");
        return !r.IsMatch(transactionID);
    }

    private bool IsSandBox(string transactionID)
    {
        Regex r = new Regex(@"^1000000");
        return r.IsMatch(transactionID);
    }

}
