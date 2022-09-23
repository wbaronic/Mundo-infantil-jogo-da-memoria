using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/*
 * Developed by WESoft Soluções
 *  http://www.wesoft.com.br
 * 
 */

public static  class Settings
{
    
    private static LanguageManager langManager;
    private static string _language = ""; //default language

    private static int _theme = 0;
    private static int _dificulty = 2;


    private static string _companyName = "BAroni Games"; // change to your company name
    private static string _companyURL = "www.wesoft.com.br"; // change to your url
    static string[] _themes = {
                            "A",//0
                            "B",
                            "C" ,
                            "D",
                            "E",
                            "F",
                            "G",
                            "H",
                            "I",//8
                            "J",
                            "K",
                            "L",
                            "M",
                            "N",
                            "O",
                            "P",
                            "Q",
                            "R",
                            "S",
                            "T",
                            "U",
                            "V",
                            "W",
                            "x",
                            "Y",
                            "Z",
                            "animal", //26
                            "desenho", //27
                            "alimento", //28
                            "mistura",  //29 
                            "brinquedo"  //30

                       };
    internal static bool showAds =true;

    public static string CompanyURL
    {
        get
        {
            return _companyURL;
        }
    }

    public static string CompanyName
    {
        get
        {
            return _companyName;
        }
    }

    public static void SetLanguage(string language)
    {
        _language = language;

        
        langManager = new LanguageManager(_language);
        PlayerPrefs.SetString("language", _language);


    }


    public static int Theme
    {
        get
        {
            return _theme;
        }
        set
        {
            _theme = value;
        }
    }


    public static string[] Themes
    {
        get
        {
            return _themes;
        }
        set
        {
            _themes = value;
        }
    }
    public static int Dificulty
    {
        get
        {
            return _dificulty;
        }
        set
        {
            _dificulty = value;
        }
    }

    public static string CurrentLanguage
    {
        get
        {
            return _language;
        }
    }

    public static LanguageManager LanguageManager
    {
        get
        {
            if (langManager==null)
            {
                SetLanguage(_language);
            }

            return langManager;
        }
    }

    public static bool isPrint { get; internal set; }
    public static int cartasCorrentes { get; internal set; }
}

