using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour
{

    public GameObject textBoxPanel;

    public Image[] images;

    public Text theText;

    public TextAsset textFile;
    public string[] textLines;

    public Sprite theDefaultPortrait;
    public Sprite PortraitAngry;
    public Sprite PortraitHappy;
    public Sprite PortraitDisappointed;
    public Image thePortraitImage;


    private Sprite theChatterPortrait;

    public int currentLine;
    public int endAtLine;

    public PlayerController thePlayer;

    public bool isActive;

    public bool stopPlayerMovement;

    // Use this for initialization
    void Start()
    {
        images = textBoxPanel.GetComponentsInChildren<Image>();

        foreach(Image img in images)
        {
            if(img.tag == "Portrait")
            {
                thePortraitImage = img;
            }
        }

        thePortraitImage.sprite = theDefaultPortrait;

        thePlayer = FindObjectOfType<PlayerController>();

        if (textFile != null)
        {
            textLines = (textFile.text.Split('\n'));

        }

        if(endAtLine == 0)
        {
            endAtLine = textLines.Length - 1;
        }

        if(isActive)
        {
            EnableTextBox();
        }
        else
        {
            DisableTextBox();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(!isActive)
        {
            return; //prevents lower code from running when textbox isnt active, saves resources
        }

        if(currentLine == 0)
        {
            theChatterPortrait = thePortraitImage.sprite;
        }

        if (textLines[currentLine].Contains("PORTRAIT_PLAYER_ANGRY")) //use same logic to convey emotions??
        {
            thePortraitImage.sprite = PortraitAngry;
            currentLine++;
            return;
        }


        if (textLines[currentLine].Contains("PORTRAIT_PLAYER_CONTENT")) //use same logic to convey emotions??
        {
            thePortraitImage.sprite = PortraitDisappointed;
            currentLine++;
            return;
        }

        if (textLines[currentLine].Contains("PORTRAIT_PLAYER_HAPPY")) //use same logic to convey emotions??
        {
            thePortraitImage.sprite = PortraitHappy;
            currentLine++;
            return;
        }

        if (textLines[currentLine].Contains("PORTRAIT_PLAYER"))
        {
            thePortraitImage.sprite = theDefaultPortrait;
            currentLine++;
            return;
        }
           
        if(textLines[currentLine].Contains("PORTRAIT_PERSON")) //use same logic to convey emotions??
        {
            thePortraitImage.sprite = theChatterPortrait;
            currentLine++;
            return;
        }
       

        theText.text = textLines[currentLine];

        if (Input.GetKeyDown(KeyCode.Z))
        {
            currentLine += 1;
        }

        if(currentLine > endAtLine)
        {
            DisableTextBox();
        }
    }

    public void EnableTextBox()
    {
       
        textBoxPanel.SetActive(true);
        isActive = true;

        if(stopPlayerMovement)
        {
            thePlayer.canMove = false;
        }
    }

    public void DisableTextBox()
    {
        //can see flash of last dialogue, mutt fix or something!!!
        textBoxPanel.SetActive(false);
        isActive = false;

        thePlayer.canMove = true;
    }

    public void ReloadScript(TextAsset theTextFile, Sprite portrait)
    {
        if(theTextFile != null && portrait != null)
        {
           
            thePortraitImage.sprite = portrait;
            textLines = new string[1]; //geting rid of redundant lines?
            textLines = (theTextFile.text.Split('\n'));

            

        }
    }
}
