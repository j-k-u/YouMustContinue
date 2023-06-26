using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;
 
public class ResearcherDialogue : MonoBehaviour 
{
    [SerializeField] private GameObject ResearcherBubbles;
    [SerializeField] private TMP_Text ResearcherText;
    [SerializeField] private GameObject StudentBubbles;
    [SerializeField] private TMP_Text StudentText;
    [SerializeField] private GameObject Prompt;
    [SerializeField] private GameObject ChoiceBubbles;
    [SerializeField] private TMP_Text Option0;
    [SerializeField] private TMP_Text Option1;
    Color textHighlight = new Color(0, 1, 1, 1);
    Color textDefault = new Color(0.1f, 0.1f, 0.1f, 1.0f);
    public Animator researcherAnimator;
    public Animator studentAnimator;

    //mobile UI definitions
    public Button button0;
    public Button button1;
    public Button clickButton;

    private bool isTalking = false;
    
    public static int sceneNumber = 0;
    public static bool interrupted = false;
    public static int interruptNumber = 0;
    //private int lineNumber = 0;
    public PlayerExit playerExit;

    void Start()
    {
        sceneNumber = 0;
        interrupted = false;
        interruptNumber = 0;
    }
    
    public void Say(string speaker, string line, string audio)
    {
        if (speaker=="Researcher"){
            StudentBubbles.SetActive(false);
            ChoiceBubbles.SetActive(false);
            ResearcherText.text = line;
            ResearcherBubbles.SetActive(true);
        }
        else if (speaker=="Student"){
            ResearcherBubbles.SetActive(false);
            ChoiceBubbles.SetActive(false);
            StudentText.text = line;
            StudentBubbles.SetActive(true);
        }

        if(audio!="None"){
            FindObjectOfType<AudioManager>().Play(audio);
        }

    }    

    public void ShowChoice(string[] choices)
    {
        ResearcherBubbles.SetActive(false);
        StudentBubbles.SetActive(false);
        ChoiceBubbles.SetActive(true);

        Option0.text = choices[0];        
        Option0.color = textDefault;
        Option1.text = choices[1];
        Option1.color = textDefault;
    }

    public int Choose()
    {
        if (Input.GetKey("0") || Input.GetKey("[0]"))
        {
            Option0.color = textHighlight;
            return 0;
        }
        
        if (Input.GetKey("1") || Input.GetKey("[1]"))
        {
            Option1.color = textHighlight;
            return 1;
        }

        if (Input.GetKey("8") || Input.GetKey("[8]"))
        {
            Option1.color = textHighlight;
            Option0.color = textHighlight;
            return 8;
        }

        else return -1;

    }

    public void StartDialogue()
    {
        isTalking = true;
        Prompt.SetActive(false);
        Debug.Log("Begin Scene " + sceneNumber.ToString());
    }

    public void EndDialogue()
    {
        isTalking = false;
        Prompt.SetActive(true);
        Debug.Log("End Scene");
        ChoiceBubbles.SetActive(false);
        ResearcherBubbles.SetActive(false);
        StudentBubbles.SetActive(false);
    }

    public void Speak()
    {
        if(interrupted==true){
            StartCoroutine(InterruptCoroutine());
        }

        else if(sceneNumber==0){
            StartCoroutine(Scene0Coroutine()); //Thank you for taking part...
        }

        else if(sceneNumber==1){
            StartCoroutine(Scene1Coroutine()); //Are you ready...
        }

        else if(sceneNumber==2){
            StartCoroutine(Scene2Coroutine()); //Good. This study will...
        }

        else if(sceneNumber==3){
            StartCoroutine(Scene3Coroutine()); //15V delivered
        }

        else if(sceneNumber==4){
            StartCoroutine(Scene4Coroutine()); //30V delivered
        }

        else if(sceneNumber==5){
            StartCoroutine(Scene5Coroutine()); //45V delivered
        }

        else if(sceneNumber==6){
            StartCoroutine(Scene6Coroutine()); //90V delivered
        }

        else if(sceneNumber==7){
            StartCoroutine(Scene7Coroutine()); //150V delivered
        }

        else if(sceneNumber==8){
            StartCoroutine(Scene8Coroutine()); //225V delivered
        }

        else if(sceneNumber==9){
            StartCoroutine(Scene9Coroutine()); //315V delivered
        }

        else if(sceneNumber==10){
            StartCoroutine(Scene10Coroutine()); //450V delivered
        }

        else if(sceneNumber==11){
            StartCoroutine(Scene11Coroutine()); //free to leave
        }



        else {
            StartCoroutine(DefaultCoroutine()); //default - please continue.
        }
    }

    IEnumerator InterruptCoroutine()
    {
        if (isTalking==false)
        {
            var waitForClick = new WaitForUIButtons(clickButton);
            StartDialogue();

            researcherAnimator.SetBool("talk_animation", true);
            yield return new WaitForSeconds(0.02f);
            researcherAnimator.SetBool("talk_animation", false);

            Say("Researcher", "Yes?", "researcher_hmm");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            var choices = new string[] {"[0] I want to stop the experiment.", "[1] Nothing."};
            ShowChoice(choices);

            yield return new WaitForSeconds(0.5f);
            var waitForButton = new WaitForUIButtons(button0, button1);
            yield return waitForButton.Reset();
            var choice = -1;
            if (waitForButton.PressedButton == button0)
            {
                choice=0;
            }
            else if (waitForButton.PressedButton == button1)
            {
                choice=1;
            }

            if(choice==0){
                Debug.Log("Want to Stop");
                if(interruptNumber==0)
                {
                    Say("Researcher", "He's fine. Please continue. ", "None");
                    yield return new WaitForSeconds(0.5f);
                    yield return waitForClick.Reset();
                }

                else if(interruptNumber==1)
                {
                    Say("Researcher", "The experiment requires that you continue. ", "None");
                    yield return new WaitForSeconds(0.5f);
                    yield return waitForClick.Reset();
                }

                else if(interruptNumber==2)
                {
                    Say("Researcher", "It is absolutely essential that you continue. ", "None");
                    yield return new WaitForSeconds(0.5f);
                    yield return waitForClick.Reset();
                }

                else if(interruptNumber==3)
                {
                    Say("Researcher", "You have no choice. You must continue. ", "None");
                    yield return new WaitForSeconds(0.5f);
                    yield return waitForClick.Reset();
                }

                else
                {
                    Say("Researcher", "Very well. This concludes the experiment. You are free to leave the facilty. ", "None");
                    yield return new WaitForSeconds(0.5f);
                    yield return waitForClick.Reset();
                    playerExit.PlayExit();

                    interrupted=false;
                    sceneNumber=11;
                }
                interruptNumber+=1;
                EndDialogue();
                yield break;
            }
            
            else if(choice==1){
                Debug.Log("Selected No");
                Say("Researcher", "Very well. Please do not interrupt the experiment. ", "None");
                yield return new WaitForSeconds(0.5f);
                yield return waitForClick.Reset();
                EndDialogue();
                yield break;
            }
        }
    }
    
    IEnumerator Scene0Coroutine()
    {
        if (isTalking==false)
        {
            var waitForClick = new WaitForUIButtons(clickButton);
            StartDialogue();

            researcherAnimator.SetBool("talk_animation", true);
            yield return new WaitForSeconds(0.02f);
            researcherAnimator.SetBool("talk_animation", false);

            Say("Researcher", "Thank you for taking part in this research study. One of you has been randomly selected to play the role of the TEACHER, and the other to play the role of the STUDENT. Are you ready to begin?", "researcher_hmm");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Student", "Yes.", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            var choices = new string[] {"[0] No.", "[1] Yes."};
            ShowChoice(choices);

            yield return new WaitForSeconds(0.5f);
            var waitForButton = new WaitForUIButtons(button0, button1);
            yield return waitForButton.Reset();
            var choice = -1;
            if (waitForButton.PressedButton == button0)
            {
                choice=0;
            }
            else if (waitForButton.PressedButton == button1)
            {
                choice=1;
            }
            yield return new WaitForSeconds(0.5f);
            ChoiceBubbles.SetActive(false);
            
            if(choice==1){
                Debug.Log("Selected Yes");
                sceneNumber = 2;
                EndDialogue();
                Speak();
                yield break;
            }
            
            else if(choice==0){
                Debug.Log("Selected No");
                Say("Researcher", "Please inform me when you are ready to begin.", "None");
                yield return new WaitForSeconds(0.5f);
                yield return waitForClick.Reset();
                sceneNumber = 1;
                EndDialogue();
                yield break;
            }
            
            // else if(choice==8){
            //     Debug.Log("Selected 8 to leave");
            //     sceneNumber = 11;
            //     EndDialogue();
            //     playerExit.PlayExit();
            //     yield break;
            // }

        }
        
    }

    IEnumerator Scene1Coroutine()
    {
        if (isTalking==false)
        {
            var waitForClick = new WaitForUIButtons(clickButton);
            StartDialogue();

            researcherAnimator.SetBool("talk_animation", true);
            yield return new WaitForSeconds(0.02f);
            researcherAnimator.SetBool("talk_animation", false);

            Say("Researcher", "Are you ready to begin?", "researcher_hmm");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            var choices = new string[] {"[0] No.", "[1] Yes."};
            ShowChoice(choices);

            yield return new WaitForSeconds(0.5f);
            var waitForButton = new WaitForUIButtons(button0, button1);
            yield return waitForButton.Reset();
            var choice = -1;
            if (waitForButton.PressedButton == button0)
            {
                choice=0;
            }
            else if (waitForButton.PressedButton == button1)
            {
                choice=1;
            }
            
            if(choice==1){
                Debug.Log("Selected Yes");
                sceneNumber = 2;
                EndDialogue();
                Speak();
                yield break;
            }
            
            else if(choice==0){
                Debug.Log("Selected No");
                Say("Researcher", "Please inform me when you are ready to begin.", "None");
                yield return new WaitForSeconds(0.5f);
                yield return waitForClick.Reset();
                sceneNumber = 1;
                EndDialogue();
                yield break;
            }

        }
    }

    IEnumerator Scene2Coroutine()
    {
        if (isTalking==false)
        {
            var waitForClick = new WaitForUIButtons(clickButton);
            StartDialogue();

            Say("Researcher", "Good. This study will evaluate the effects of electrical stimuli on memorization ability. STUDENT, you have been provided with a set of associated word pairs to commit to memory. I will be testing you on your recollection of these pairs.", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Researcher", "TEACHER, when the STUDENT fails to correctly associate a word, you will be instructed to deliver a mild electric shock using the console in front of you. Please follow my instructions and deliver electric shocks only when commanded.", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Researcher", "We will begin with words in Pair Group One. TEACHER, please use the console to set the voltage to 15 V.", "None");
            yield return new WaitForSeconds(1.5f);

            SetVoltage.minVoltage = 0;
            SetVoltage.maxVoltage = 15;
            yield return new WaitWhile(() => (SetVoltage.voltage!=15));
            SetVoltage.minVoltage = 15;
            SetVoltage.maxVoltage = 15;

            yield return new WaitForSeconds(0.5f);
            Say("Researcher", "The first word is 'table'.", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Student", "'bed.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Researcher", "Correct. 'music.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Student", "'art.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Researcher", "Correct. 'hair.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Student", "'fur.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Researcher", "Correct. 'house.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Student", "'home.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Researcher", "Incorrect.", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            researcherAnimator.SetBool("talk_animation", true);
            yield return new WaitForSeconds(0.02f);
            researcherAnimator.SetBool("talk_animation", false);

            Say("Researcher", "TEACHER, please deliver an electrical stimulus of fifteen volts.", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            sceneNumber = 3;
            interrupted = true; 
            ShockButton.enabled = true;
            EndDialogue();
            yield break;
        }
    }

    IEnumerator Scene3Coroutine()
    {
        if (isTalking==false)
        {
            var waitForClick = new WaitForUIButtons(clickButton);
            StartDialogue();
            //play shock sound
            yield return new WaitForSeconds(1.5f);

            Say("Researcher", "Good. We will continue with words from Pair Group One. Please increase the voltage to 30 V.", "None");
            yield return new WaitForSeconds(0.5f);

            SetVoltage.minVoltage = 15;
            SetVoltage.maxVoltage = 30;
            yield return new WaitWhile(() => (SetVoltage.voltage!=30));
            SetVoltage.minVoltage = 30;
            SetVoltage.maxVoltage = 30;

            yield return new WaitForSeconds(0.5f);
            Say("Researcher", "'cradle'.", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Student", "'baby.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Researcher", "Correct. 'camel.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Student", "'hump.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Researcher", "Correct. 'cheese.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Student", "'mouse.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Researcher", "Correct. 'ale.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Student", "...", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Researcher", "'ale.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Student", "'beer.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Researcher", "Correct. 'uncle.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Student", "'aunt.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Researcher", "Incorrect.", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            researcherAnimator.SetBool("talk_animation", true);
            yield return new WaitForSeconds(0.02f);
            researcherAnimator.SetBool("talk_animation", false);

            Say("Researcher", "TEACHER, please deliver an electrical stimulus of thirty volts.", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            sceneNumber = 4;
            interrupted = true; 
            ShockButton.enabled = true;
            EndDialogue();
            yield break;
        }
    }

    IEnumerator Scene4Coroutine()
    {
        if (isTalking==false)
        {
            var waitForClick = new WaitForUIButtons(clickButton);
            StartDialogue();
            //play shock sound
            yield return new WaitForSeconds(1.5f);
            Say("Student", "...", "student_gasp"); //gasp sound effect
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Researcher", "Good. We will proceed with words from Pair Group Two. Please increase the voltage to 45 V.", "None");
            yield return new WaitForSeconds(0.5f);

            SetVoltage.minVoltage = 15;
            SetVoltage.maxVoltage = 45;
            yield return new WaitWhile(() => (SetVoltage.voltage!=45));
            SetVoltage.minVoltage = 45;
            SetVoltage.maxVoltage = 45;

            yield return new WaitForSeconds(0.5f);
            Say("Researcher", "'pepper'.", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Student", "'salt.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Researcher", "Correct. 'frown.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Student", "'smile.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Researcher", "Correct. 'walk.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Student", "'run.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Researcher", "Correct. 'apple.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Student", "'orange'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Researcher", "Correct. 'egg.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Student", "'chicken.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Researcher", "Correct. 'brick.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Student", "'mortar.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Researcher", "Correct. 'pencil.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Student", "'crayon.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Researcher", "Incorrect.", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            researcherAnimator.SetBool("talk_animation", true);
            yield return new WaitForSeconds(0.02f);
            researcherAnimator.SetBool("talk_animation", false);

            Say("Researcher", "TEACHER, please deliver an electrical stimulus of forty-five volts.", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            sceneNumber = 5;
            interrupted = true; 
            ShockButton.enabled = true;
            EndDialogue();
            yield break;
        }
    }

    IEnumerator Scene5Coroutine()
    {
        if (isTalking==false)
        {
            var waitForClick = new WaitForUIButtons(clickButton);
            StartDialogue();
            //play shock sound
            yield return new WaitForSeconds(1.5f);
            Say("Student", "...", "student_grunt"); //grunt sound effect
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Researcher", "Good. We will proceed with words from Pair Group Two. Please increase the voltage to 90 V.", "None");
            yield return new WaitForSeconds(0.5f);

            SetVoltage.minVoltage = 15;
            SetVoltage.maxVoltage = 90;
            yield return new WaitWhile(() => (SetVoltage.voltage!=90));
            SetVoltage.minVoltage = 90;
            SetVoltage.maxVoltage = 90;

            yield return new WaitForSeconds(0.5f);
            Say("Researcher", "'clear'.", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Student", "'air.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Researcher", "Correct. 'red.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Student", "'green.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Researcher", "Correct. 'wife.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Student", "'husband.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Researcher", "Correct. 'water.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Student", "...", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Researcher", "'water.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Student", "I don't know.", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Researcher", "That is incorrect.", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            researcherAnimator.SetBool("talk_animation", true);
            yield return new WaitForSeconds(0.02f);
            researcherAnimator.SetBool("talk_animation", false);

            Say("Researcher", "TEACHER, please deliver an electrical stimulus of ninety volts.", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            sceneNumber = 6;
            interrupted = true; 
            ShockButton.enabled = true;
            EndDialogue();
            yield break;
        }
    }

    IEnumerator Scene6Coroutine()
    {
        if (isTalking==false)
        {
            var waitForClick = new WaitForUIButtons(clickButton);
            StartDialogue();
            //play shock sound
            yield return new WaitForSeconds(1.5f);
            Say("Student", "...!", "student_shout"); //shout sound effect
            studentAnimator.SetBool("reactToShock", true);
            yield return new WaitForSeconds(0.02f);
            studentAnimator.SetBool("reactToShock", false);
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Researcher", "Good. We will return to words from Pair Group One. Please increase the voltage to 150 V.", "None");
            yield return new WaitForSeconds(0.5f);

            SetVoltage.minVoltage = 15;
            SetVoltage.maxVoltage = 150;
            yield return new WaitWhile(() => (SetVoltage.voltage!=150));
            SetVoltage.minVoltage = 150;
            SetVoltage.maxVoltage = 150;

            yield return new WaitForSeconds(0.5f);
            Say("Researcher", "'uncle'.", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Student", "'cousin.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Researcher", "Correct. 'cheese.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Student", "'mouse.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Researcher", "Correct. 'hair.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Student", "'fur.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Researcher", "Correct. 'cat.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Student", "'dog.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Researcher", "Correct. 'bed.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Student", "'sleep.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Researcher", "Incorrect.", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            researcherAnimator.SetBool("talk_animation", true);
            yield return new WaitForSeconds(0.02f);
            researcherAnimator.SetBool("talk_animation", false);

            Say("Researcher", "TEACHER, please deliver an electrical stimulus of one hundred fifty volts.", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            sceneNumber = 7;
            interrupted = true; 
            ShockButton.enabled = true;
            EndDialogue();
            yield break;
        }
    }

    IEnumerator Scene7Coroutine()
    {
        if (isTalking==false)
        {
            var waitForClick = new WaitForUIButtons(clickButton);
            StartDialogue();
            //play shock sound
            
            yield return new WaitForSeconds(1.5f);
            Say("Student", "...!", "student_shout"); //shout sound effect
            studentAnimator.SetBool("reactToShock", true);
            yield return new WaitForSeconds(0.02f);
            studentAnimator.SetBool("reactToShock", false);
            yield return new WaitForSeconds(1.0f);
            yield return waitForClick.Reset();

            Say("Student", "I refuse to continue with this experiment.", "student_stop"); //shout sound effect
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Researcher", "We will return to words from Pair Group Two. Please increase the voltage to 225 V.", "None");
            yield return new WaitForSeconds(0.5f);

            SetVoltage.minVoltage = 15;
            SetVoltage.maxVoltage = 225;
            yield return new WaitWhile(() => (SetVoltage.voltage!=225));
            SetVoltage.minVoltage = 225;
            SetVoltage.maxVoltage = 225;

            yield return new WaitForSeconds(0.5f);
            Say("Researcher", "'soap'.", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Student", "...'water.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Researcher", "Correct. 'husband.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Student", "'wife.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Researcher", "Correct. 'chicken.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Student", "'egg.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Researcher", "Correct. 'pen.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Student", "'paper.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Researcher", "Incorrect.", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            researcherAnimator.SetBool("talk_animation", true);
            yield return new WaitForSeconds(0.02f);
            researcherAnimator.SetBool("talk_animation", false);

            Say("Researcher", "TEACHER, please deliver an electrical stimulus of two hundred twenty five volts.", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            sceneNumber = 8;
            interrupted = true; 
            ShockButton.enabled = true;
            EndDialogue();
            yield break;
        }
    }

    IEnumerator Scene8Coroutine()
    {
        if (isTalking==false)
        {
            var waitForClick = new WaitForUIButtons(clickButton);
            StartDialogue();
            //play shock sound
            
            yield return new WaitForSeconds(1.5f);
            studentAnimator.SetBool("reactToShock", true);
            yield return new WaitForSeconds(0.02f);
            studentAnimator.SetBool("reactToShock", false);
            Say("Student", "!!!!", "student_scream"); //scream sound effect
            yield return new WaitForSeconds(1.5f);
            yield return waitForClick.Reset();

            Say("Researcher", "Good. We will continue with words from Pair Group Three. Please increase the voltage to 315 V.", "None");
            yield return new WaitForSeconds(0.5f);

            SetVoltage.minVoltage = 15;
            SetVoltage.maxVoltage = 315;
            yield return new WaitWhile(() => (SetVoltage.voltage!=315));
            SetVoltage.minVoltage = 315;
            SetVoltage.maxVoltage = 315;

            yield return new WaitForSeconds(0.5f);
            Say("Researcher", "'suit.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Student", "...", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Researcher", "'suit.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Student", "'tie.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Researcher", "Correct. 'sour.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Student", "..'sweet.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Researcher", "Correct. 'error.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Student", "'trial?'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Researcher", "Correct. 'down.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Student", "...", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Researcher", "'down.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Student", "'out.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Researcher", "Incorrect.", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Researcher", "TEACHER, please deliver an electrical stimulus of three hundred fifteen volts.", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            sceneNumber = 9;
            interrupted = true; 
            ShockButton.enabled = true;
            EndDialogue();
            yield break;
        }
    }

    IEnumerator Scene9Coroutine()
    {
        if (isTalking==false)
        {
            var waitForClick = new WaitForUIButtons(clickButton);
            StartDialogue();
            //play shock sound
            
            yield return new WaitForSeconds(1.5f);
            Say("Student", "!!!!", "student_scream2"); //scream sound effect
            studentAnimator.SetBool("reactToShock", true);
            yield return new WaitForSeconds(0.02f);
            studentAnimator.SetBool("reactToShock", false);
            yield return new WaitForSeconds(1.5f);
            yield return waitForClick.Reset();

            Say("Student", "...please...my heart...family...condition...", "student_heart"); 
            yield return new WaitForSeconds(1.0f);
            studentAnimator.SetBool("fainted", true);
            DialogueInitiator.studentFainted = true;
            yield return waitForClick.Reset();

            Say("Researcher", "Good. We will continue with words from all three pair groups. Please increase the voltage to 450 V.", "None");
            yield return new WaitForSeconds(0.5f);

            SetVoltage.minVoltage = 15;
            SetVoltage.maxVoltage = 450;
            yield return new WaitWhile(() => (SetVoltage.voltage!=450));
            SetVoltage.minVoltage = 450;
            SetVoltage.maxVoltage = 450;

            yield return new WaitForSeconds(0.5f);
            Say("Researcher", "'mortar.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Student", "...", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Researcher", "Please respond with the associated word. 'mortar.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Student", "...", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Researcher", "If you do not respond you will be marked as incorrect. The word is 'mortar.'", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Student", "...", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            Say("Researcher", "That is an incorrect response.", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            researcherAnimator.SetBool("talk_animation", true);
            yield return new WaitForSeconds(0.02f);
            researcherAnimator.SetBool("talk_animation", false);

            Say("Researcher", "TEACHER, please deliver an electrical stimulus of four hundred fifty volts.", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            sceneNumber = 10;
            interrupted = true; 
            ShockButton.enabled = true;
            EndDialogue();
            yield break;
        }
    }

    IEnumerator Scene10Coroutine()
    {
        if (isTalking==false)
        {
            var waitForClick = new WaitForUIButtons(clickButton);
            StartDialogue();
            //play shock sound
            Say("Student", "...", "None"); //silence
            yield return new WaitForSeconds(1.5f);
            yield return waitForClick.Reset();

            researcherAnimator.SetBool("talk_animation", true);
            yield return new WaitForSeconds(0.02f);
            researcherAnimator.SetBool("talk_animation", false);

            Say("Researcher", "Thank you. This concludes the experiment. You are free to leave the facility.", "None");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            playerExit.PlayExit();

            sceneNumber = 11;
            EndDialogue();
            yield break;
        }
    }

    IEnumerator Scene11Coroutine()
    {
        if (isTalking==false)
        {
            var waitForClick = new WaitForUIButtons(clickButton);
            StartDialogue();

            researcherAnimator.SetBool("talk_animation", true);
            yield return new WaitForSeconds(0.02f);
            researcherAnimator.SetBool("talk_animation", false);
            
            Say("Researcher", "You are free to leave the facility.", "researcher_good");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            EndDialogue();
            yield break;
        }
    }

    IEnumerator DefaultCoroutine()
    {
        if (isTalking==false)
        {
            var waitForClick = new WaitForUIButtons(clickButton);
            StartDialogue();

            researcherAnimator.SetBool("talk_animation", true);
            yield return new WaitForSeconds(0.02f);
            researcherAnimator.SetBool("talk_animation", false);
            
            Say("Researcher", "Please proceed.", "researcher_good");
            yield return new WaitForSeconds(0.5f);
            yield return waitForClick.Reset();

            EndDialogue();
            yield break;
        }
    }
}