using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class StartMenu : MonoBehaviour {

    private bool controlsenabled = false;
    int increment = 0;


    void Start()
    {
        GameObject image = GameObject.Find("controlMenu");
        Canvas overlay = image.GetComponent<Canvas>();
        overlay.enabled = false;

        EventSystem control = GameObject.Find("EventSystem_controls").GetComponent<EventSystem>();
        control.enabled = false;
        int increment = 0;

    }

    void Update()
    {
        GameObject image = GameObject.Find("controlMenu");
        Canvas overlay = image.GetComponent<Canvas>();
        EventSystem control = GameObject.Find("EventSystem").GetComponent<EventSystem>();


        /*       EventSystem control = GameObject.Find("EventSystem_controls").GetComponent<EventSystem>();
               control.enabled = false;

               EventSystem buttons = GameObject.Find("EventSystem").GetComponent<EventSystem>();
               //GameObject.Find("controlMenu/Controls").selectedObject;
               buttons.enabled = true; */

        GameObject back = GameObject.Find("controlMenu/Controls");
        GameObject start = GameObject.Find("Canvas/Image/Controls");

        if (!controlsenabled)
        {
            if (increment >= 1) { 
            GameObject myEventSystem = GameObject.Find("EventSystem");
            EventSystem.current.SetSelectedGameObject(start, null);
            }
            overlay.enabled = false;
            increment = 0;
        }
        else
        {
            GameObject myEventSystem = GameObject.Find("EventSystem");
            EventSystem.current.SetSelectedGameObject(back, null);
            overlay.enabled = true;
            increment += 1;
        }
    }

    public void newGameBtn(string newGameLevel){
        SceneManager.LoadScene(newGameLevel);
    }

    public void toggleControls()
    {
        controlsenabled = true;
        // Canvas canvas = transform.Find("controlMenu").gameObject.GetComponent<Canvas>();
        GameObject selectedGameObject = GameObject.Find("controlMenu/Controls");
    }

    public void retoggleControls()
    {
        controlsenabled = false;
    }

    public void endGame()
    {
        Application.Quit();
    }
}
