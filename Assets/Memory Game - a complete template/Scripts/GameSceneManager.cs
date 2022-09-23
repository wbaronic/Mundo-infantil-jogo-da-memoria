/*
 * Developed by WESoft Soluções
 *  http://www.wesoft.com.br
 * 
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

#if UNITY_ADS
using UnityEngine.Advertisements;
#endif

using UnityEngine.SceneManagement;
//using System.Runtime;


public static class GameSceneManager
{



    static string  _newSceneName;


#if WINDOWS_UWP
    static bool _hasAd = false;
#endif

    public static void Initialize()
    {

#if WINDOWS_UWP
        Vungle.init("", "", "58e70f13d1bcb8494f00012f");
        Vungle.adPlayableEvent += Vungle_adPlayableEvent;
        Vungle.onAdFinishedEvent += Vungle_onAdFinishedEvent;        
#endif
    }



    public static void MainMenu(bool showAd = true)
    {
        _newSceneName = "Menu";

        if (showAd)
            ShowAd();
        else
            SceneManager.LoadScene(_newSceneName);
    }


    


    public static void StartGame()
    {

        SceneManager.LoadScene("Game");
    }

    public static void StartOrganize()
    {

        SceneManager.LoadScene("Chapter3");
    }

    public static void QuitGame()
    {
        Application.Quit();
    }

    public static void ReloadScene()
    {

        _newSceneName = SceneManager.GetActiveScene().name;
        ShowAd();        
    }

    public static void ShowAd()
    {
        bool show = false;

#if WINDOWS_UWP
        if (_hasAd)
        {   
            show = true;
            Vungle.playAd();        
            
        }
        
#elif (UNITY_IOS || UNITY_ANDROID) &&  UNITY_ADS
        if (Advertisement.IsReady())
        {
            show = true;
            var showOptions = new ShowOptions { resultCallback = UnityAdResult };
            Advertisement.Show(showOptions);
        }

#endif

        if (!show)
            continueLoadingScene();
    }


#if WINDOWS_UWP
    private static void Vungle_onAdFinishedEvent(AdFinishedEventArgs obj)
    {
        continueLoadingScene();
    }

    private static void Vungle_adPlayableEvent(bool adAvailable)
    {
        _hasAd = adAvailable;
    }
#elif (UNITY_IOS || UNITY_ANDROID) && UNITY_ADS
    private static void UnityAdResult(ShowResult result)
    {
        continueLoadingScene();
    }
#endif

    private static void continueLoadingScene()
    {
        SceneManager.LoadScene(_newSceneName);
    }

    public static List<Texture2D> escolherCartas(int numberOfPairs)
    {
        List<Texture2D> selectedImages = new List<Texture2D>(Resources.LoadAll<Texture2D>("Themes/" + Settings.Themes[Settings.Theme] + "/"));

       // List<Texture2D> selectedImages = new List<Texture2D>();

        List<Texture2D> selectedImages2 = new List<Texture2D>();

        //for (int i = 25; i < Settings.Themes.Length; i++)
        //{
        //    if (Settings.Themes[i].Length > 1)
        //    {
        //        selectedImages2.AddRange(new List<Texture2D>(Resources.LoadAll<Texture2D>("Themes/" + Settings.Themes[i] + "/")));
        //    }
        //}

             selectedImages2.AddRange(new List<Texture2D>(Resources.LoadAll<Texture2D>("Themes/")));

        List<Texture2D> texture2Ds;
        if (Settings.LanguageManager.CurrentLanguage.Equals("pt-br"))
        {

            texture2Ds = selectedImages2.FindAll(T => T.name.StartsWith(Settings.Themes[Settings.Theme], System.StringComparison.OrdinalIgnoreCase));

        }
        else
        {
            texture2Ds = selectedImages2.FindAll(T => T.name.EndsWith(Settings.Themes[Settings.Theme],System.StringComparison.OrdinalIgnoreCase)&& T.name.Length>2);

        }
        selectedImages.AddRange(texture2Ds);

      

        selectedImages.Shuffle(); //initial shuffle, to select images for this round (as the number of images in the selected theme can be greater than the current level)

        if (selectedImages.Count < numberOfPairs)
        {

            int ramdom = UnityEngine.Random.Range(0, Settings.Themes.Length);
            //trocar pra numero aleatorio
            selectedImages.AddRange(new List<Texture2D>(Resources.LoadAll<Texture2D>("Themes/" + Settings.Themes[29] + "/")));
          //  selectedImages.AddRange(new List<Texture2D>(Resources.LoadAll<Texture2D>("Themes/" + Settings.Themes[26] + "/")));


        }

        selectedImages.RemoveRange(numberOfPairs, selectedImages.Count - numberOfPairs); //we need only the number of pairs
        selectedImages.AddRange(selectedImages.GetRange(0, numberOfPairs)); //duplicate the selected images
        selectedImages.Shuffle(); //a new - final - shuffle with the selected images;
        return selectedImages;
    }




}

