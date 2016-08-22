using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{
    public GameObject defaultSelect;
    public GameObject currentlySelected;
    MenuNavigation directions;
    Animator animation;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!currentlySelected)
        {
            currentlySelected = defaultSelect;
        }
        if (directions == null || directions.gameObject != currentlySelected)
        {
            directions = currentlySelected.GetComponent<MenuNavigation>();
            animation = currentlySelected.GetComponent<Animator>();
            animation.SetBool("Highlighted", true);


        }
        if (directions.up != null && Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentlySelected = directions.up;
            animation.SetBool("Highlighted", false);
        }

        if (directions.down != null && Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentlySelected = directions.down;
            animation.SetBool("Highlighted", false);
        }

        if (directions.right != null && Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentlySelected = directions.right;
            animation.SetBool("Highlighted", false);
        }

        if (directions.left != null && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentlySelected = directions.left;
            animation.SetBool("Highlighted", false);
        }
    }
}
