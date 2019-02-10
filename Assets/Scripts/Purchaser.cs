using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;


public class Purchaser : MonoBehaviour, IStoreListener
{
    private static IStoreController m_StoreController;          // The Unity Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.
    public static Purchaser instance;
    public static string kProductIDConsumable = "consumable";
        

        
    private static string kProductNameAppleSubscription = "com.unity3d.subscription.new";

        
    private static string kProductNameGooglePlaySubscription = "com.unity3d.subscription.original";

    void Start()
    { 
        if (m_StoreController == null)
        {
            InitializePurchasing();
        }
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void InitializePurchasing()
    {
        if (IsInitialized())
        {
            return;
        }
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
          
        builder.AddProduct(kProductIDConsumable, ProductType.Consumable);
        builder.AddProduct("brucebanner", ProductType.Consumable);
        builder.AddProduct("naruto", ProductType.Consumable);

            builder.AddProduct("subscription", ProductType.Subscription, new IDs(){
                { kProductNameAppleSubscription, AppleAppStore.Name },
                { kProductNameGooglePlaySubscription, GooglePlay.Name },
            });
        UnityPurchasing.Initialize(this, builder);
    }


    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }
    public void BuyProductID(string productId)
    {
        // If Purchasing has been initialized ...
       
       
            if (IsInitialized())
            {
                Product product = m_StoreController.products.WithID(productId);

                // If the look up found a product for this device's store and that product is ready to be sold ... 
                if (product != null && product.availableToPurchase)
                {
                    Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                    m_StoreController.InitiatePurchase(product);

                }
                // Otherwise ...
                else
                {
                    Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                }
            }
            // Otherwise ...
            else
            {
                Debug.Log("BuyProductID FAIL. Not initialized.");
            }
        
    }
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {

        Debug.Log("OnInitialized: PASS");

        m_StoreController = controller;
           
        m_StoreExtensionProvider = extensions;
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
            
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }


    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (String.Equals(args.purchasedProduct.definition.id, kProductIDConsumable, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
        }
        else if (String.Equals(args.purchasedProduct.definition.id, "brucebanner", StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            SystemCharacter.instance.SetCurrentCharacterToInventory();
            
        }
        else if (String.Equals(args.purchasedProduct.definition.id, "naruto", StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            SystemCharacter.instance.SetCurrentCharacterToInventory();
        }
        else
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
        }
        
        return PurchaseProcessingResult.Complete;
    }


    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
}
