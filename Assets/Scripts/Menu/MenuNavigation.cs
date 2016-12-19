using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;

public class MenuNavigation : MonoBehaviour
{
    [SerializeField]
    MoveToMenu camera;
    [SerializeField]
    Transform currentOption, currentMenu;
    MenuOption optionSettings;
    Menu menu;

    // Use this for initialization
    private void Start()
    {
        menu = currentMenu.GetComponent<Menu>();
    }
    // Update is called once per frame
    void Update()
    {
        if (currentOption != null)
        {
            if (currentMenu != currentOption.parent)
            {
                menu.isCurrent = false;
            }

            if (optionSettings == null)
            {
                optionSettings = currentOption.GetComponent<MenuOption>();
            }
            else if (optionSettings.gameObject != currentOption)
            {
                optionSettings.selected = false;
                optionSettings = currentOption.GetComponent<MenuOption>();

            }

            if (optionSettings != null)
            {
                optionSettings.selected = true;
            }
        }
        if (optionSettings != null)
        {

            if (optionSettings.onUp != null)
            {

                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    currentOption = optionSettings.onUp;
                }
            }

            if (optionSettings.onDown != null)
            {

                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    currentOption = optionSettings.onDown;
                }
            }
            if (optionSettings.onLeft != null)
            {

                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    currentOption = optionSettings.onLeft;
                }
            }
            if (optionSettings.onRight != null)
            {

                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    currentOption = optionSettings.onRight;
                }
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                IMenu task = currentOption.GetComponent<IMenu>();
                if (task != null)
                {
                    task.OnEnter();
                }
                if (optionSettings.onEnter != null)
                {
                    currentOption = optionSettings.onEnter;
                }
                if (optionSettings.cameraPoint != null)
                {
                    camera.toMoveTo = optionSettings.cameraPoint;
                }
            }
        }
    }
}
