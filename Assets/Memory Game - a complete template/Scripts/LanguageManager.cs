/*
 * Developed by WESoft Soluções
 *  http://www.wesoft.com.br
 * 
 */


using System.Collections.Generic;
using System.Linq;
using System.Text;



using System.Xml;
using System.Xml.Linq;
using UnityEngine;




public class LanguageManager
{
    

    string _currentLanguage;
    
    TextAsset langFile;

    XDocument xDoc;

    

    


    string _changeLanguage;
    string _selectTheme;
    string _gameTitle;
    string _play;
    string _about;
    string _attempts;
    string _errors;
    string _finished;
    string _finishedErrors;
    string _mainMenu;
    string _quitGame;
    string _playAgain;
    string _confirm;
    string _cancel;
    string _selectLanguage;
    string _selectDificulty;
    string _startGame;
    string _backToTheme;
    string _developedByText;
    string _powerText;


    public struct ThemesStruct
    {
        public string Bible;


        public string A;
        public string B;


        public string alimento;
        public string Animals;
        public string Flags;
        public string Letters;
        public string Landscapes;
        public string Brazilian_Flags;
    }

    public struct DificultyStruct
    {
        public string VeryEasy;
        public string Easy;
        public string Medium;
        public string Hard;
        public string VeryHard;

    }


    ThemesStruct _theme;
    DificultyStruct _dificulty;

    public LanguageManager(string language)
    {
        langFile = Resources.Load<TextAsset>("Languages/" + language);

        _currentLanguage = language;


        xDoc = XDocument.Parse(langFile.text);
        //xmlDoc.LoadXml(langFile.text);



        
        _gameTitle = xDoc.Descendants("title").SingleOrDefault().Value;

        
        _play = xDoc.Descendants("play").SingleOrDefault().Value;
        _about = xDoc.Descendants("about").SingleOrDefault().Value;
        _attempts = xDoc.Descendants("attemptsText").SingleOrDefault().Value;
        _errors = xDoc.Descendants("errorsText").SingleOrDefault().Value;
        _finished = xDoc.Descendants("finishedText").SingleOrDefault().Value;
        _finishedErrors = xDoc.Descendants("finishedErrorsText").SingleOrDefault().Value;
        _mainMenu = xDoc.Descendants("mainMenu").SingleOrDefault().Value;
        _quitGame = xDoc.Descendants("quitGame").SingleOrDefault().Value;
        _playAgain = xDoc.Descendants("playAgain").SingleOrDefault().Value;
        _selectTheme = xDoc.Descendants("selectTheme").SingleOrDefault().Value;
        _changeLanguage = xDoc.Descendants("changeLanguage").SingleOrDefault().Value;
        _selectLanguage = xDoc.Descendants("selectLanguage").SingleOrDefault().Value;
        _selectDificulty = xDoc.Descendants("selectDificulty").SingleOrDefault().Value;


        _theme.Animals = xDoc.Root.Element("Themes").Descendants("animals").SingleOrDefault().Value;
        _theme.Brazilian_Flags = xDoc.Root.Element("Themes").Descendants("brazilian_flags").SingleOrDefault().Value;
        _theme.Flags = xDoc.Root.Element("Themes").Descendants("flags").SingleOrDefault().Value;
        _theme.Landscapes = xDoc.Root.Element("Themes").Descendants("landscapes").SingleOrDefault().Value;
        _theme.Letters = xDoc.Root.Element("Themes").Descendants("letters").SingleOrDefault().Value;
        _theme.alimento = xDoc.Root.Element("Themes").Descendants("alimento").SingleOrDefault().Value;
        _theme.Bible = xDoc.Root.Element("Themes").Descendants("Bible").SingleOrDefault().Value;

      //  _theme.A = xDoc.Root.Element("Themes").Descendants("A").SingleOrDefault().Value;
       // _theme.B = xDoc.Root.Element("Themes").Descendants("B").SingleOrDefault().Value;



        _dificulty.VeryEasy = xDoc.Root.Element("Dificulty").Descendants("very_easy").SingleOrDefault().Value;
        _dificulty.Easy = xDoc.Root.Element("Dificulty").Descendants("easy").SingleOrDefault().Value;
        _dificulty.Medium = xDoc.Root.Element("Dificulty").Descendants("medium").SingleOrDefault().Value;
        _dificulty.Hard = xDoc.Root.Element("Dificulty").Descendants("hard").SingleOrDefault().Value;
        _dificulty.VeryHard = xDoc.Root.Element("Dificulty").Descendants("very_hard").SingleOrDefault().Value;

        

        _confirm  = xDoc.Descendants("confirm").SingleOrDefault().Value;
        _cancel = xDoc.Descendants("cancel").SingleOrDefault().Value;

        _startGame = xDoc.Descendants("startGame").SingleOrDefault().Value;
        _backToTheme = xDoc.Descendants("back_theme_selection").SingleOrDefault().Value;

        _developedByText = xDoc.Descendants("developedBy").SingleOrDefault().Value;
        _powerText = xDoc.Descendants("powerText").SingleOrDefault().Value;



    }


    public string GameTitle
    {
        get
        {
            return _gameTitle;
        }
    }

    public string CurrentLanguage
    {
        get
        {

            return _currentLanguage;
        }
    }

    public string ConfirmTitle
    {
        get
        {
            return _confirm;
        }
    }
    public string StartGameTitle
    {
        get
        {
            return _startGame;
        }
    }
    public string BackToThemeTitle
    {
        get
        {
            return _backToTheme;
        }
    }

    public string CancelTitle
    {
        get
        {
            return _cancel;
        }
    }

    public ThemesStruct Theme
    {
        get
        {
            return _theme;
        }
    }

    public DificultyStruct Dificulty
    {
        get
        {
            return _dificulty;
        }
    }

    public string ChangeLanguageTitle
    {
        get
        {
            return _changeLanguage;
        }
    }

    public string SelectLanguageTitle
    {
        get
        {
            return _selectLanguage;
        }
    }

    public string SelectDificultyTitle
    {
        get
        {
            return _selectDificulty;
        }
    }

    public string SelectThemeTitle
    {
        get
        {
            return _selectTheme;
        }
    }

    public string PlayTitle
    {
        get
        {
            return _play;
        }
    }

    public string AboutTitle
    {
        get
        {
            return _about;
        }
    }

    public string MainMenuTitle
    {
        get
        {
            return _mainMenu;
        }
    }

    public string PlayAgainTitle
    {
        get
        {
            return _playAgain;
        }
    }

    public string QuitGameTitle
    {
        get
        {
            return _quitGame;
        }
    }


    public string powerText
    {
        get
        {
            return _powerText;
        }
    }

    public string AttemptsText(int value)
    {
        return _attempts.Replace("%attempts", value.ToString());
    }


    public string ErrorsText(int value)
    {
        return _errors.Replace("%errors", value.ToString());
    }

    public string FinishedText(int value)
    {
        return _finished.Replace("%attempts", value.ToString());
    }

    public string FinishedErrorsText(int value)
    {
        return _finishedErrors.Replace("%errors", value.ToString());
    }

    public string DevelopedByText(string value)
    {
        return _developedByText.Replace("%company", value.ToString());
    }
    
    

}

