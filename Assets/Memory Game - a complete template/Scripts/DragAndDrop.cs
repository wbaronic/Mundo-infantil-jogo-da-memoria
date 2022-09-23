using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour
{
    public GameObject dragItem;
    public Canvas dragCanvas;
    public Image[] figuras;
    public AudioClip[] somFiguras;
    public Text[] respostas;
    ProgressMeters progressMeters;
    public AudioClip CorrectSoundA;
    public AudioClip CorrectSoundB;


    GameObject selectedObject;
    private bool tirarDrag;
    private int acertos;


    private void Start()
    {


        progressMeters = gameObject.GetComponent<ProgressMeters>();
        progressMeters.totalHealth = 5;
        progressMeters.health = 5;



        int whichTheme = Settings.Theme;

        if (whichTheme == 0)
        {
            whichTheme = 3;
        }


        // string language = Settings.LanguageManager.CurrentLanguage;
        string language = Settings.LanguageManager.CurrentLanguage;

        string caminho = "Themes/" + Settings.Themes[whichTheme] + "/";

        string caminhoFiguras = "Themes/";
        Sprite[] sprites = Resources.LoadAll<Sprite>(caminhoFiguras);



        respostas[0].text = Settings.Themes[whichTheme];
        respostas[1].text = Settings.Themes[whichTheme - 1];
        string caminho2 = "Themes/" + Settings.Themes[whichTheme - 1] + "/";



        string caminhoSOm1 = "Themes/" + Settings.Themes[whichTheme] + "/" + Settings.Themes[whichTheme] + "-" + language;
        CorrectSoundA = (Resources.Load<AudioClip>(caminhoSOm1));

        string caminhoSOm2 = "Themes/" + Settings.Themes[whichTheme - 1] + "/" + Settings.Themes[whichTheme - 1] + "-" + language;
        CorrectSoundB = (Resources.Load<AudioClip>(caminhoSOm2));


        //for (int i = 0; i < themes.Length; i++)
        //{
        //    if (themes[i].Length > 1)
        //    {
        //        card.somAceto = (Resources.Load<AudioClip>("Themes/" + themes[i] + "/" + img.name.Split("-")[0] + "-" + language));
        //        if (card.somAceto != null)
        //        {
        //            break;
        //        }

        //    }
        //}




        int figurasint = 0;
        for (int i = 0; i < sprites.Length; i++)
        {
            if (sprites[i].name.StartsWith(Settings.Themes[whichTheme], System.StringComparison.OrdinalIgnoreCase) || sprites[i].name.StartsWith(Settings.Themes[whichTheme - 1], System.StringComparison.OrdinalIgnoreCase))
            {
                figuras[figurasint].sprite = sprites[i];
                //somFiguras[figurasint] = (Resources.Load<AudioClip>("Themes/" + Settings.Themes[i] + "/" + sprites[i].name.Split("-")[0] + "-" + language));



                figurasint++;
                if (figurasint == 6)
                {
                    break;
                }
            }



        }










    }
    IEnumerator PlaySound(AudioClip sound)
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<AudioSource>().PlayOneShot(sound);
    }


    public void PlayCorrect(AudioClip CorrectSound)
    {



        StartCoroutine(PlaySound(CorrectSound));

    }

    public void StartDrag(GameObject selectedObject)
    {





        this.selectedObject = selectedObject;
        if (GetComponent<ShowHidePanels>().pauseUp == false)
        {
            dragItem = Instantiate(selectedObject, Input.mousePosition, selectedObject.transform.rotation) as GameObject;


            if (dragItem.GetComponent < Image >().sprite.name.StartsWith(respostas[0].text,System.StringComparison.OrdinalIgnoreCase))
            {
                PlayCorrect(CorrectSoundA);

            }
            else
            {
                PlayCorrect(CorrectSoundB);

            }
            //if (!dragItem.GetComponent<Image>().sprite.name.Equals("chestSpriteSheet_2"))
            //{

                dragItem.transform.SetParent(dragCanvas.transform);
                dragItem.GetComponent<Image>().SetNativeSize();

            

                dragItem.transform.localScale = 2.1f * dragItem.transform.localScale;
                dragItem.GetComponent<Image>().raycastTarget = false;
            //}

        }
        


      
    }

    public void Drag()
    {


        if (GetComponent<ShowHidePanels>().pauseUp == false )
        {
            dragItem.transform.position = Input.mousePosition;
        }
    }

    public void StopDrag()
    {
        if (GetComponent<ShowHidePanels>().pauseUp == false)
        {
            Destroy(dragItem);

            if (tirarDrag == true)
            {
                selectedObject.GetComponent<EventTrigger>().enabled = false;
                tirarDrag = false;
            }


            if (acertos == 6)
            {

               // SceneManager.LoadScene("premio");
            }

        }

    }

    public void Drop(Image dropSlot)
    {

        if (GetComponent<ShowHidePanels>().pauseUp == false)
        {

            GameObject droppedItem = dragCanvas.transform.GetChild(0).gameObject;
            Image imageDropped = droppedItem.GetComponent<Image>();

            string letraInicial = imageDropped.sprite.name.Substring(0, 1);
            if (dropSlot.GetComponentInChildren<Text>().text.StartsWith(letraInicial,System.StringComparison.OrdinalIgnoreCase))
            {


                selectedObject.GetComponent<Image>().sprite = dropSlot.GetComponent<Image>().sprite;
                tirarDrag = true;
                acertos++;

             //   Destroy(selectedObject);

                progressMeters.health++;
            }
            else
            {
                progressMeters.health--;
            }


        }

    }
}