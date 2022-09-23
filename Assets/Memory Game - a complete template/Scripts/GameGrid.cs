using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
 * Developed by WESoft Soluções
 *  http://www.wesoft.com.br
 * 
 */

public class GameGrid : MonoBehaviour
{

    public AudioClip ErrorSound;
    public AudioClip CorrectSound;
    public AudioClip WinSound;
    public AudioClip looseSound;
    public AudioClip looseSoundENUS;

    AudioSource audioSource;
    string acao;

    public Image finishImage;

    public Sprite winImage;
    public Sprite looseImage;
    public Sprite powerImage;

    GameObject scorePanel;
    Text timerText;
    Text powerText;
    Text errorsText;
    Text attemptsText;
    Text finishedText;
    Text finishedErrorsText;
    Button mainMenuTopButton;
    Button playAgainButton;
    Button adsButton;
    Button backButton;

    Button nextButton;
    ProgressMeters progressMeters;

    Button mainMenuButton;
    Button quitGameButton;

    public Camera cam;

    //RewardedAdExample rewardedAd;
    //InterstitialAdExample interstitialAd;
    
    GameObject finishPanel;
    GameObject powerPanel;
    GameObject PauseBanner;
    public GameObject grid;


    private bool canRotate = false;

    private Tile firstCard = null;
    private Tile secondCard = null;


    public Tile TilePrefab;
        

    int attempts = 0;
    int errors = 0;
    
    
    int numberOfPairs = 0;


    string[] themes = Settings.Themes;

    int  whichTheme;

    float timeToMemorize;

    private Tile[] tilesAll;

    

    public struct DifficultyStruct
    {
        public string Name; //to help identify only
        public int NumberOfTiles;
        public int TilesPerRow; 
        public float TimeToMemorize;
    }

    DifficultyStruct whichDifficulty;

    public bool CanRotate
    {
        get
        {
            return canRotate;
        }
        set
        {
            canRotate = value;
        }
    }

    public Tile FirstCard
    {
        get
        {
            return firstCard;
        }

        set
        {
            firstCard = value;
        }
    }

    public Tile SecondCard
    {
        get
        {
            return secondCard;
        }

        set
        {
            secondCard = value;
        }
    }

    List<DifficultyStruct> difficulties;

    private void Awake()
    {



        audioSource = GameObject.Find("narracao").GetComponent<AudioSource>();

        if (Settings.CurrentLanguage.Equals("pt-br"))
        {
            audioSource.clip = looseSound;

        }
        else
        {
            audioSource.clip = looseSoundENUS;

        }



        difficulties = new List<DifficultyStruct>();

        DifficultyStruct diff = new DifficultyStruct();
        diff.Name = "Very easy";
        diff.NumberOfTiles = 6;
        diff.TilesPerRow = 2;
        diff.TimeToMemorize = 4;

        difficulties.Add(diff);


        diff = new DifficultyStruct();
        diff.Name = "Easy";
        diff.NumberOfTiles = 8;
        diff.TilesPerRow = 2;
        diff.TimeToMemorize = 4;

        difficulties.Add(diff);

        diff = new DifficultyStruct();
        diff.Name = "Medium";
        diff.NumberOfTiles = 12;
        diff.TilesPerRow = 3;
        diff.TimeToMemorize = 6;

        difficulties.Add(diff);


        diff = new DifficultyStruct();
        diff.Name = "Hard";
        diff.NumberOfTiles = 24;
        diff.TilesPerRow = 4;
        diff.TimeToMemorize = 5;

        difficulties.Add(diff);


        diff = new DifficultyStruct();
        diff.Name = "Very Hard";
        diff.NumberOfTiles = 36;
        diff.TilesPerRow = 4;
        diff.TimeToMemorize = 4;

        difficulties.Add(diff);


        
        whichDifficulty = difficulties[Settings.Dificulty];



        mainMenuTopButton = GameObject.Find("mainMenuTopButton").GetComponent<Button>();

        mainMenuTopButton.onClick.AddListener(delegate { GameSceneManager.MainMenu(); });
        nextButton = GameObject.Find("next").GetComponent<Button>();
        adsButton = GameObject.Find("ads").GetComponent<Button>();
        backButton = GameObject.Find("backButton").GetComponent<Button>();

        
        playAgainButton = GameObject.Find("playAgainButton").GetComponent<Button>();
      
        
        playAgainButton.onClick.AddListener(delegate { GameSceneManager.ReloadScene(); });

        mainMenuButton = GameObject.Find("mainMenuButton").GetComponent<Button>();
        mainMenuButton.onClick.AddListener(delegate { GameSceneManager.MainMenu(true); });

      //  quitGameButton = GameObject.Find("quitGameButton").GetComponent<Button>();
       // quitGameButton.onClick.AddListener(delegate { GameSceneManager.QuitGame(); });


        mainMenuTopButton.GetComponentInChildren<Text>().text = Settings.LanguageManager.MainMenuTitle;
       // playAgainButton.GetComponentInChildren<Text>().text = Settings.LanguageManager.PlayAgainTitle;
        mainMenuButton.GetComponentInChildren<Text>().text = Settings.LanguageManager.MainMenuTitle;
    //    quitGameButton.GetComponentInChildren<Text>().text = Settings.LanguageManager.QuitGameTitle;

        timerText = GameObject.Find("timerText").GetComponent<Text>();
       // powerText = GameObject.Find("powerText").GetComponent<Text>();


        errorsText = GameObject.Find("errorsText").GetComponent<Text>(); 
        attemptsText = GameObject.Find("attemptsText").GetComponent<Text>(); 


        finishedText = GameObject.Find("finishedText").GetComponent<Text>();
        finishedErrorsText = GameObject.Find("finishedErrorsText").GetComponent<Text>();

        PauseBanner = GameObject.Find("PauseBanner");



        scorePanel = GameObject.Find("scorePanel");        
        scorePanel.SetActive(false);

        finishPanel = GameObject.Find("finishPanel");
        finishPanel.SetActive(false);




        powerPanel = GameObject.Find("powerPanel");
      //  powerPanel.SetActive(false);
        

    }


    // Use this for initialization
    void Start()
    {
       // interstitialAd = gameObject.GetComponent<InterstitialAdExample>();
       // rewardedAd = gameObject.GetComponent<RewardedAdExample>();


        progressMeters = gameObject.GetComponent<ProgressMeters>();
        progressMeters.totalHealth = 5;
        progressMeters.health = 5;
        newGame();
    }


    public void proximo()
    {
        Settings.Theme++;

        PlayerPrefs.SetInt("fase", Settings.Theme);
        PlayerPrefs.SetInt("dificuldade", Settings.Dificulty);

        GameSceneManager.StartGame();
    }


    void newGame()
    {
        
        attempts = 0;
        errors = 0;
        
        whichTheme =  Settings.Theme;
        string language = Settings.LanguageManager.CurrentLanguage;
        string caminho = "Themes/" + themes[whichTheme] + "/" + themes[whichTheme] + "-"+language;
        CorrectSound = (Resources.Load<AudioClip>(caminho));
        looseSound= (Resources.LoadAll<AudioClip>("Languages")[0]); 
        createTiles();
        
        timeToMemorize = 0;
        timerText.text = timeToMemorize.ToString();



        if (Settings.isPrint)
        {

            cam.clearFlags = CameraClearFlags.SolidColor;
            mainMenuTopButton.gameObject.SetActive( false);
            attemptsText.gameObject.SetActive(false);
            cam.backgroundColor = Color.white;
            revealAll();

        }
        else {
            InvokeRepeating("memorizeTimer", 0, 1);

        }
    }



   public void superPower()
    {

        acao = "poder";
        showPopUp();
       
    }

    public void playADSonLoose()
    {


        if (acao.Equals("perdeu")){
           // interstitialAd.ShowAd();
            canRotate = false;
        }




        if (acao.Equals("poder"))
        {
            // rewardedAd.ShowAd();
            //interstitialAd.ShowAd();

            canRotate = false;
        }

    }



    public void showAds()
    {
        if (acao.Equals("perdeu"))
        {
            continueAfterplayADSonLoose();

        }
        else
        {
            continueSuperPower();
        }

    }

    public void continueAfterplayADSonLoose()
    {
        canRotate = true;
        finishPanel.gameObject.SetActive(false);
        grid.gameObject.SetActive(true);
        powerPanel.SetActive(true);

        scorePanel.SetActive(true);

        if (acao.Equals("perdeu"))
        {
            progressMeters.health = 5;

        }



    }

    public void continueSuperPower()
    {

        finishPanel.gameObject.SetActive(false);
        grid.gameObject.SetActive(true);

        scorePanel.SetActive(true);

        timeToMemorize = whichDifficulty.TimeToMemorize;
        timerText.text = timeToMemorize.ToString();

        revealAll();
        timerText.gameObject.SetActive(true);
        InvokeRepeating("memorizeTimer", 1, 1);
        canRotate = false;


    }

    private void revealAll()
    {
        for (int i = 0; i < tilesAll.Length; i++)
        {
            Tile card = tilesAll[i];


            card.Reveal();
        }
    }

    private void Update()
    {
        if (canRotate)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {

                    checkRayCast();
                }
            }
        }
    }

    void checkRayCast()
    {

        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitObject;
        bool hit = Physics.Raycast(ray.origin, ray.direction,out hitObject, Mathf.Infinity);
        if (hit)
        {
            
            Tile tile = hitObject.collider.gameObject.GetComponent<Tile>();

            if (tile.IsRotating)
                return;

            if (secondCard != tile && firstCard != tile)
            {
                
                tile.Reveal();


                if (firstCard == null)
                {
                    firstCard = tile;
                }
                else //second card
                {
                    secondCard = tile;
                    canRotate = false;

                    if (firstCard.img.name != secondCard.img.name)  //error
                    {
                        PlayError();

                    }
                    else  //hit
                    {
                        if (secondCard.somAceto != null)
                        {

                            PlayCorrect(secondCard.somAceto);

                        }
                        else
                        {
                            PlayCorrect(CorrectSound);

                        }

                    }
                }
            }
        }
    }

    public void PlayError()
    {
        
        errors++;
        attempts++;
        updateScore();
        StartCoroutine(PlaySound(ErrorSound));
        Invoke("afterError", 2);

    }

    void  playSoundNormal(AudioClip sound)
    {
        audioSource.Play();

    }

    IEnumerator PlaySound(AudioClip sound)
    {
        yield return new WaitForSeconds(1f);
        GetComponent<AudioSource>().PlayOneShot(sound);
    }

    void afterError()
    {        
        firstCard.Hide();
        secondCard.Hide();

        firstCard = null;
        secondCard = null;

        canRotate = true;
        progressMeters.health--;


        if (errors % 5 == 0 && Settings.showAds)
        {


            canRotate = false;


            // interstitialAd.ShowAd();

            playSoundNormal(looseSound);
            acao = "perdeu";
            showPopUp();
            //StartCoroutine(PlaySound(looseSound));
            //Invoke("loose", 3);
        }

    }

    public void showPopUp()
    {






        if (acao.Equals("perdeu"))
        {

            if (Settings.showAds)
            {


                //  PlaySound(looseSound);
                finishedErrorsText.text = Settings.LanguageManager.FinishedErrorsText(errors);

                GameObject.Find("Grid").gameObject.SetActive(false); ;

                finishImage.sprite = looseImage;
                scorePanel.SetActive(false);
                finishPanel.SetActive(true);
                nextButton.gameObject.SetActive(false);
                mainMenuButton.gameObject.SetActive(true);
                powerPanel.SetActive(false);
                backButton.gameObject.SetActive(false);
                playAgainButton.gameObject.SetActive(false);
                adsButton.gameObject.SetActive(true);

            }


        }



        if (acao.Equals("poder"))
        {

            if (Settings.showAds)
            {


                //  PlaySound(looseSound);
                finishedErrorsText.text = Settings.LanguageManager.powerText;

                GameObject.Find("Grid").gameObject.SetActive(false); ;

                finishImage.sprite = powerImage;
                scorePanel.SetActive(false);
                finishPanel.SetActive(true);
                nextButton.gameObject.SetActive(false);
                mainMenuButton.gameObject.SetActive(false);
                powerPanel.SetActive(false);
                backButton.gameObject.SetActive(true);
                playAgainButton.gameObject.SetActive(false);
                adsButton.gameObject.SetActive(true);

            }


        }

        PauseBanner.SetActive(false);


    }

    
    public void PlayCorrect(AudioClip CorrectSound)
    {

        
        attempts++;

        updateScore();
        StartCoroutine(PlaySound(CorrectSound));
        Invoke("afterCorrect", 3);
        
    }


    void afterCorrect()
    {


        firstCard.somAceto = null;
        firstCard.gameObject.SetActive(false);
        secondCard.gameObject.SetActive(false);

       // secondCard.Destroy();

        firstCard = null;
        secondCard = null;
        canRotate = true;

        numberOfPairs--;

        if (numberOfPairs == 0)
        {
            StartCoroutine(PlaySound(WinSound));
            Invoke("win", 1);
        }
    }



    void createTiles()
    {

        float tileWidth;

        int tilesPerRow = whichDifficulty.TilesPerRow;

        int rows = whichDifficulty.NumberOfTiles / whichDifficulty.TilesPerRow;

        numberOfPairs = whichDifficulty.NumberOfTiles / 2;

        List<Texture2D> selectedImages = GameSceneManager.escolherCartas(numberOfPairs);

        Texture2D img = new Texture2D(0, 0);
        Quaternion rot = new Quaternion(0, 0, 0, 0);

        float xOffset = 0.0f;
        float yOffset = 0.0f;



        tilesAll = new Tile[whichDifficulty.NumberOfTiles];



        var height = 2 * Camera.main.orthographicSize;
        var width = height * Camera.main.aspect;
        var verticalMargin = -height / 2;



        float startX = 0f;

        if (width < height)
        {
            tileWidth = width / (whichDifficulty.TilesPerRow + 2f); // +2f -> left/right margins            

            startX = -(tileWidth / 2) * whichDifficulty.TilesPerRow;
        }
        else //landscape
        {
            tilesPerRow = whichDifficulty.NumberOfTiles / whichDifficulty.TilesPerRow;

            rows = whichDifficulty.NumberOfTiles / tilesPerRow;

            tileWidth = height / (rows + 2);
            startX = -(tileWidth / 2) * tilesPerRow;

        }



        Vector3 tileSize = new Vector3(tileWidth, tileWidth, 0.2f);


        Vector3 pos = new Vector3(startX, 0);


        tileSize = new Vector3(tileWidth, tileWidth, 0.2f);


        verticalMargin += tileWidth;

        yOffset = verticalMargin;

        for (int tilesCreated = 0; tilesCreated < whichDifficulty.NumberOfTiles; tilesCreated++)
        {


            xOffset += tileWidth;

            if (tilesCreated % tilesPerRow == 0)
            {

                yOffset += tileWidth;
                xOffset = tileWidth / 2;
            }



            Tile card = Instantiate(TilePrefab, new Vector3(pos.x + xOffset, pos.y - yOffset, pos.z), rot, transform);


            img = selectedImages[tilesCreated];

            string language = Settings.LanguageManager.CurrentLanguage;

            if (img.name.Length > 2)
            {




                for (int i = 0; i < themes.Length; i++)
                {
                    if (themes[i].Length > 1)
                    {
                        card.somAceto = (Resources.Load<AudioClip>("Themes/" + themes[i] + "/" + img.name.Split("-")[0] + "-" + language));

                        if (card.somAceto == null)
                        {
                            card.somAceto = (Resources.Load<AudioClip>("Themes/" + themes[i] + "/" + img.name + "-" + language));

                        }

                        if (card.somAceto != null)
                        {
                            break;
                        }

                    }
                }




            }




            card.img = img;
            card.displayImage();

            card.transform.localScale = tileSize;
            tilesAll[tilesCreated] = card;

            card.transform.Rotate(0, 180, 0);
            canRotate = false;





        }
    }

  

    void memorizeTimer()
    {
        timeToMemorize--;
        timerText.text = timeToMemorize.ToString();
        if (timeToMemorize <= 0)
        {
            CancelInvoke("memorizeTimer");
            
            timerText.gameObject.SetActive(false);            
            scorePanel.SetActive(true);
            updateScore();
            

            for (int i = 0; i < tilesAll.Length; i++)
            {
                Tile card = tilesAll[i];
                
                card.Hide();
            }
            
            canRotate = true;
            powerPanel.SetActive(true);


        }
    }


    void updateScore()
    {
        attemptsText.text = Settings.LanguageManager.AttemptsText(attempts);
        errorsText.text = Settings.LanguageManager.ErrorsText(errors);
    }

    void win()
    {


        finishImage.sprite = winImage;

        finishedText.text = Settings.LanguageManager.FinishedText(attempts);
        finishedErrorsText.text = Settings.LanguageManager.FinishedErrorsText(errors);

        scorePanel.SetActive(false);
        finishPanel.SetActive(true);
        PauseBanner.SetActive(true);
        powerPanel.SetActive(false);

        finishedErrorsText.text = "";

        mainMenuTopButton.gameObject.SetActive(true);
        backButton.gameObject.SetActive(false);

        nextButton.gameObject.SetActive(true);
        adsButton.gameObject.SetActive(false);
        playAgainButton.gameObject.SetActive(true);




        if(errors < difficulties[Settings.Dificulty].NumberOfTiles / 2)
        {

            if (Settings.Dificulty <4)
            {
                Settings.Dificulty++;

            }
        }
        else
        {

            if (Settings.Dificulty > 0)
            {
               // Settings.Dificulty--;

            }
        }

        PlayerPrefs.SetInt("fase", Settings.Theme);
        PlayerPrefs.SetInt("dificuldade", Settings.Dificulty);


    }


}
