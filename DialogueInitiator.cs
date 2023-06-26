using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueInitiator : MonoBehaviour
{
    [SerializeField] private string researcherTag = "Researcher";
    [SerializeField] private string studentTag = "Student";
    public TMP_Text prompt;

    public ResearcherDialogue researcherDialogue;
    public static bool studentFainted;

    //mobile
    public Button clickButton;
    public bool buttonClicked = false;

    private void Start()
    {
        studentFainted = false;
    }

    public void click()
    {
        buttonClicked=true;
    }
    
    private void Update()
    {
        var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2f, Screen.height/2f, 0f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            if (selection.CompareTag(researcherTag))
            {
                prompt.text = "Talk";
                if (Input.GetMouseButtonDown(0))
                //mobile
                //if (buttonClicked)
                {
                    researcherDialogue.Speak();
                }
            }

            else if (selection.CompareTag(studentTag))
            {
                prompt.text = "Talk";
                if (Input.GetMouseButtonDown(0) && !studentFainted){
                //mobile
                //if (buttonClicked && !studentFainted){
                    FindObjectOfType<AudioManager>().Play("student_unsure");
                }
            }

            else
            {
                prompt.text = "";
            }
        }
        //mobile
        buttonClicked = false;
    }
}