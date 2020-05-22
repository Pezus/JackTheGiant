using System;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;  

public class AdsController : MonoBehaviour {

    public static AdsController instance;

    private bool showAd = true;
    
    private BannerView bannerView;
    private InterstitialAd interstitial;

    void Awake()
    {
        MakeSingleton();
    }

    void Start()
    {
    }

    void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void OnLevelWasLoaded()
    {
        if (Application.loadedLevelName == "Gameplay" && showAd)
        {
            ShowBanner();
            showAd = false;
        }
        if (Application.loadedLevelName == "MainMenu")
        {
            showAd = true;
        }
    }

    private void RequestBanner()
    {
        #if UNITY_EDITOR
                string adUnitId = "unused";
        #elif UNITY_ANDROID
                    string adUnitId = "ca-app-pub-5162749801192352/4199515429";
        #elif UNITY_IPHONE
                    string adUnitId = "INSERT_IOS_BANNER_AD_UNIT_ID_HERE";
        #else
                    string adUnitId = "unexpected_platform";
        #endif

        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Top);
        // Register for ad events.
        RegisterDelegateForBanner();
        // Load a banner ad.
        bannerView.LoadAd(createAdRequest());
    }

    private void RequestInterstitial()
    {
        #if UNITY_EDITOR
                string adUnitId = "unused";
        #elif UNITY_ANDROID
                    string adUnitId = "ca-app-pub-5162749801192352/5676248628";
        #elif UNITY_IPHONE
                    string adUnitId = "INSERT_IOS_INTERSTITIAL_AD_UNIT_ID_HERE";
        #else
                    string adUnitId = "unexpected_platform";
        #endif

        // Create an interstitial.
        interstitial = new InterstitialAd(adUnitId);
        // Register for ad events.
        RegisterDelegateForInterstitial();
        // Load an interstitial ad.
        interstitial.LoadAd(createAdRequest());
    }

    // Returns an ad request with custom ad targeting.
    private AdRequest createAdRequest()
    {
        return new AdRequest.Builder()
                .AddTestDevice(AdRequest.TestDeviceSimulator)
                .AddTestDevice("0123456789ABCDEF0123456789ABCDEF")
                .AddKeyword("game")
                .SetGender(Gender.Male)
                .SetBirthday(new DateTime(1985, 1, 1))
                .TagForChildDirectedTreatment(false)
                .AddExtra("color_bg", "9B30FF")
                .Build();

    }

    void RegisterDelegateForBanner()
    {
        bannerView.AdLoaded += HandleAdLoaded;
        bannerView.AdFailedToLoad += HandleAdFailedToLoad;
        bannerView.AdOpened += HandleAdOpened;
        bannerView.AdClosing += HandleAdClosing;
        bannerView.AdClosed += HandleAdClosed;
        bannerView.AdLeftApplication += HandleAdLeftApplication;
    }

    void UnregisterDelegateForBanner()
    {
        bannerView.AdLoaded -= HandleAdLoaded;
        bannerView.AdFailedToLoad -= HandleAdFailedToLoad;
        bannerView.AdOpened -= HandleAdOpened;
        bannerView.AdClosing -= HandleAdClosing;
        bannerView.AdClosed -= HandleAdClosed;
        bannerView.AdLeftApplication -= HandleAdLeftApplication;

    }

    void RegisterDelegateForInterstitial()
    {
        interstitial.AdLoaded += HandleInterstitialLoaded;
        interstitial.AdFailedToLoad += HandleInterstitialFailedToLoad;
        interstitial.AdOpened += HandleInterstitialOpened;
        interstitial.AdClosing += HandleInterstitialClosing;
        interstitial.AdClosed += HandleInterstitialClosed;
        interstitial.AdLeftApplication += HandleInterstitialLeftApplication;
        GoogleMobileAdsDemoHandler handler = new GoogleMobileAdsDemoHandler();
        interstitial.SetInAppPurchaseHandler(handler);

    }

    void UnregisterDelegateForInterstitial()
    {
        interstitial.AdLoaded -= HandleInterstitialLoaded;
        interstitial.AdFailedToLoad -= HandleInterstitialFailedToLoad;
        interstitial.AdOpened -= HandleInterstitialOpened;
        interstitial.AdClosing -= HandleInterstitialClosing;
        interstitial.AdClosed -= HandleInterstitialClosed;
        interstitial.AdLeftApplication -= HandleInterstitialLeftApplication;
        GoogleMobileAdsDemoHandler handler = new GoogleMobileAdsDemoHandler();
        interstitial.SetInAppPurchaseHandler(handler);

    }

    public void ShowInterstitial()
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
        else
        {
            RequestInterstitial();
        }
    }

    public void ShowBanner()
    {
        bannerView.Show();
    }
    
    public void HandleAdLoaded(object sender, EventArgs args)
    {
        ShowBanner();
    }

    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        UnregisterDelegateForBanner();
        RequestBanner();
    }

    public void HandleAdOpened(object sender, EventArgs args)
    {

    }

    void HandleAdClosing(object sender, EventArgs args)
    {

    }

    public void HandleAdClosed(object sender, EventArgs args)
    {
        UnregisterDelegateForBanner();
        RequestBanner();
    }

    public void HandleAdLeftApplication(object sender, EventArgs args)
    {

    }

    // ===========================

    public void HandleInterstitialLoaded(object sender, EventArgs args)
    {
        ShowInterstitial();
    }

    public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        UnregisterDelegateForInterstitial();
        RequestInterstitial();
    }

    public void HandleInterstitialOpened(object sender, EventArgs args)
    {

    }

    void HandleInterstitialClosing(object sender, EventArgs args)
    {

    }

    public void HandleInterstitialClosed(object sender, EventArgs args)
    {
        UnregisterDelegateForInterstitial();
        RequestInterstitial();
    }

    public void HandleInterstitialLeftApplication(object sender, EventArgs args)
    {

    }
}


public class GoogleMobileAdsDemoHandler : IInAppPurchaseHandler
{
    private readonly string[] validSkus = { "android.test.purchased" };

    public static string key = "pub-5162749801192352";

    //Will only be sent on a success.
    public void OnInAppPurchaseFinished(IInAppPurchaseResult result)
    {
        result.FinishPurchase();
    }

    //Check SKU against valid SKUs.
    public bool IsValidPurchase(string sku)
    {
        foreach (string validSku in validSkus)
        {
            if (sku == validSku)
            {
                return true;
            }
        }
        return false;
    }

    //Return the app's public key.
    public string AndroidPublicKey
    {
        //In a real app, return public key instead of null.
        get { return key; }
    }
}
