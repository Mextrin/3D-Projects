using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PauseMenu : MonoBehaviour
{
    public bool menuOpened;

    public GameObject currentUI;
    public GameObject mainPauseMenu;
    public Image pauseBackground;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuOpened = !menuOpened;

            if (menuOpened)
            {
                Time.timeScale = 0;
                currentUI = mainPauseMenu;
                currentUI.gameObject.SetActive(true);
                
                foreach (RectTransform child in currentUI.transform)
                {
                    child.gameObject.SetActive(true);
                }
            }
            else if (!menuOpened)
            {
                Time.timeScale = 1;
            }
        }

        if (menuOpened)
        {
            if (pauseBackground != null && !pauseBackground.gameObject.activeInHierarchy)
            {
                pauseBackground.gameObject.SetActive(true);
            }
        }
        else if (!menuOpened)
        {
            if (pauseBackground != null && pauseBackground.gameObject.activeInHierarchy)
            {
                pauseBackground.gameObject.SetActive(false);
            }

            foreach (RectTransform menu in transform)
            {
                menu.gameObject.SetActive(false);
            }
        }
    }
}
