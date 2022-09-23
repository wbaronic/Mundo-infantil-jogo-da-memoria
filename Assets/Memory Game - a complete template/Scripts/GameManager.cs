using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using System.Linq;

/*
 * Developed by WESoft Soluções
 *  http://www.wesoft.com.br
 * 
 */


public class GameManager : MonoBehaviour {
    Button playButton;
    Button aboutButton;
    Button quitGameButton;
    Button languageButton;

    Button mainMenuButton;
    Button aboutMainMenuButton;

    Text gameTitle;
    Text selectThemeText;
    Text selectLanguage;
    Text selectDificulty;
    Text developedByText;

    Button animalsButton;
    Button alimentoButton;
    Button flagsButton;
    Button lettersButton;
    Button landscapesButton;
    Button brazilianFlagsButton;
    Button BibleButton;

    Button A;
    Button B;

    Toggle vEasyToggle;
    Toggle easyToggle;
    Toggle mediumToggle;
    Toggle hardToggle;
    Toggle vHardToggle;

    ToggleGroup dificultyToggleGroup;


    Button confirmButton;
    Button cancelButton;


    Toggle englishToggle;
    Toggle brazilianToggle;

    Button startGameButton;
    Button themeSelectButton;
    //InterstitialAdExample interstitialAd;



    private void Awake()
    {



        GameSceneManager.Initialize();

        

        englishToggle = GameObject.Find("englishToggle").GetComponent<Toggle>();
        brazilianToggle = GameObject.Find("brazilianToggle").GetComponent<Toggle>();
        
        playButton = GameObject.Find("playButton").GetComponent<Button>();
       // aboutButton = GameObject.Find("aboutButton").GetComponent<Button>();
        quitGameButton = GameObject.Find("quitGameButton").GetComponent<Button>();
        quitGameButton.onClick.AddListener(delegate { GameSceneManager.QuitGame(); });

        mainMenuButton = GameObject.Find("mainMenuButton").GetComponent<Button>();
        //aboutMainMenuButton = GameObject.Find("aboutMainMenuButton").GetComponent<Button>();
        languageButton = GameObject.Find("languageButton").GetComponent<Button>();

        A = GameObject.Find("A").GetComponent<Button>();
        B = GameObject.Find("B").GetComponent<Button>();
      
        
        animalsButton = GameObject.Find("animals").GetComponent<Button>();
        alimentoButton = GameObject.Find("alimento").GetComponent<Button>();
        flagsButton = GameObject.Find("C").GetComponent<Button>();
        lettersButton = GameObject.Find("D").GetComponent<Button>();
        landscapesButton = GameObject.Find("F").GetComponent<Button>();
        brazilianFlagsButton = GameObject.Find("G").GetComponent<Button>();
      //  BibleButton= GameObject.Find("Bible").GetComponent<Button>();

        vEasyToggle = GameObject.Find("vEasyToggle_0").GetComponent<Toggle>();
        easyToggle = GameObject.Find("easyToggle_1").GetComponent<Toggle>();
        mediumToggle = GameObject.Find("mediumToggle_2").GetComponent<Toggle>();
        hardToggle = GameObject.Find("hardToggle_3").GetComponent<Toggle>();
        vHardToggle = GameObject.Find("vHardToggle_4").GetComponent<Toggle>();

        dificultyToggleGroup = GameObject.Find("dificultyToggleGroup").GetComponent<ToggleGroup>();

        confirmButton = GameObject.Find("confirmButton").GetComponent<Button>();
        cancelButton = GameObject.Find("cancelButton").GetComponent<Button>();


        gameTitle = GameObject.Find("gameTitle").GetComponent<Text>();
        selectThemeText = GameObject.Find("selectThemeText").GetComponent<Text>();

        selectLanguage = GameObject.Find("selectLanguageTitle").GetComponent<Text>();


        selectDificulty = GameObject.Find("selectDificultyTitle").GetComponent<Text>();

       // startGameButton = GameObject.Find("startGameButton").GetComponent<Button>();        

        themeSelectButton = GameObject.Find("themeSelectButton").GetComponent<Button>();


      //  developedByText = GameObject.Find("developedByText").GetComponent<Text>();

        SetLanguage(PlayerPrefs.GetString("language", "null"));







    }

    // Use this for initialization
    void Start ()
    {


    }


    public void ConfirmLanguage()
    {
        if (englishToggle.isOn)
            SetLanguage("en-us");
        else
            SetLanguage("pt-br");
    }


    public void SetTheme(int theme)
    {
        Settings.Theme = theme;
    }

    public void SetLanguage(string newLanguage)
    {




        if (newLanguage.Equals("null"))
        {
            if (Application.systemLanguage == SystemLanguage.Portuguese)
            {
                newLanguage = "pt-br";

            }
            else
            {
                newLanguage = "en-us";

            }
        }











        Settings.SetLanguage(newLanguage);
        

        if (newLanguage == "en-us")
            englishToggle.isOn = true;
        else
            brazilianToggle.isOn = true;



        playButton.GetComponentInChildren<Text>().text = Settings.LanguageManager.PlayTitle;
        languageButton.GetComponentInChildren<Text>().text = Settings.LanguageManager.ChangeLanguageTitle;
     //   aboutButton.GetComponentInChildren<Text>().text = Settings.LanguageManager.AboutTitle;
        quitGameButton.GetComponentInChildren<Text>().text = Settings.LanguageManager.QuitGameTitle;

        //mainMenuButton.GetComponentInChildren<Text>().text = Settings.LanguageManager.MainMenuTitle;
        //aboutMainMenuButton.GetComponentInChildren<Text>().text = Settings.LanguageManager.MainMenuTitle;

        gameTitle.text = Settings.LanguageManager.GameTitle;
        selectThemeText.text = Settings.LanguageManager.SelectThemeTitle;
        selectLanguage.text = Settings.LanguageManager.SelectLanguageTitle;
        selectDificulty.text = Settings.LanguageManager.SelectDificultyTitle;



      //  A.GetComponentInChildren<Text>().text = Settings.LanguageManager.Theme.A;
      //  B.GetComponentInChildren<Text>().text = Settings.LanguageManager.Theme.B;



        animalsButton.GetComponentInChildren<Text>().text = Settings.LanguageManager.Theme.Animals;
        alimentoButton.GetComponentInChildren<Text>().text = Settings.LanguageManager.Theme.alimento;
       // flagsButton.GetComponentInChildren<Text>().text = Settings.LanguageManager.Theme.Flags;
       // lettersButton.GetComponentInChildren<Text>().text = Settings.LanguageManager.Theme.Letters;
       // landscapesButton.GetComponentInChildren<Text>().text = Settings.LanguageManager.Theme.Landscapes;
      //  brazilianFlagsButton.GetComponentInChildren<Text>().text = Settings.LanguageManager.Theme.Brazilian_Flags;
       // BibleButton.GetComponentInChildren<Text>().text = Settings.LanguageManager.Theme.Bible;



        vEasyToggle.GetComponentInChildren<Text>().text = Settings.LanguageManager.Dificulty.VeryEasy;
        easyToggle.GetComponentInChildren<Text>().text = Settings.LanguageManager.Dificulty.Easy;
        mediumToggle.GetComponentInChildren<Text>().text = Settings.LanguageManager.Dificulty.Medium;
        hardToggle.GetComponentInChildren<Text>().text = Settings.LanguageManager.Dificulty.Hard;
        vHardToggle.GetComponentInChildren<Text>().text = Settings.LanguageManager.Dificulty.VeryHard;

        confirmButton.GetComponentInChildren<Text>().text = Settings.LanguageManager.ConfirmTitle;
        cancelButton.GetComponentInChildren<Text>().text = Settings.LanguageManager.CancelTitle;

        //startGameButton.GetComponentInChildren<Text>().text = Settings.LanguageManager.StartGameTitle;
        themeSelectButton.GetComponentInChildren<Text>().text = Settings.LanguageManager.BackToThemeTitle;

      //  developedByText.text = Settings.LanguageManager.DevelopedByText(Settings.CompanyName);
    }

    // Update is called once per frame
    void Update () {
		
	}

    Button[] botoes;
    string selecionado;
    public void selecionar(Button button)
    {

        string selecaoAtual = button.name.Split('_')[1];

        if (!selecionado.Equals(selecaoAtual))
        {
            foreach (var outrosBotoes in botoes)
            {
                button.GetComponentInChildren<Text>().text = "Selecionar";
            }

            button.GetComponentInChildren<Text>().text = "Selecionado";
            //trocar pelo playerPrefs
            Debug.Log(selecaoAtual);

        }




    }

    public void chooseDificulty()
    {
        Settings.Dificulty = int.Parse(dificultyToggleGroup.ActiveToggles().FirstOrDefault().name.Split('_')[1]);

    }
    public void StartGame()
    {




        // interstitialAd.ShowAd();
        GameSceneManager.StartGame();
    }



    public void StartGameOneCliclk()
    {


        Settings.Theme = PlayerPrefs.GetInt("fase", 0);
        Settings.Dificulty = PlayerPrefs.GetInt("dificuldade", 0);


        // interstitialAd.ShowAd();
        GameSceneManager.StartGame();
    }







    public void StarOrganize()
    {

        // interstitialAd.ShowAd();
        GameSceneManager.StartOrganize();
    }
    public void OpenCompanySite()
    {
        Application.OpenURL("http://" + Settings.CompanyURL);
    }

    
}
