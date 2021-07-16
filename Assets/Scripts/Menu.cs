using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour
{
    int currentChosenIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        SetActive();
    }

    void SelectNextMenuItem(bool goToPrevious = false)
    {
        Transform prevChild = transform.GetChild(currentChosenIndex).transform;
        prevChild.position = new Vector2(prevChild.position.x - 20, prevChild.position.y);
        prevChild.GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, 0.75f);

        currentChosenIndex += goToPrevious ? (currentChosenIndex <= 0 ? transform.childCount-1 : -1) : 1;
        currentChosenIndex %= transform.childCount;

        SetActive();
    }

    private void SetActive()
    {
        Transform nextChild = transform.GetChild(currentChosenIndex).transform;
        nextChild.position = new Vector2(nextChild.position.x + 20, nextChild.position.y);
        nextChild.GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire"))
        {
            MenuItemEvent(transform.GetChild(currentChosenIndex).name);
        }

        if (Input.GetButtonDown("Vertical"))
        {
            SelectNextMenuItem(Input.GetAxis("Vertical") < 0 ? false : true);
        }
    }

    void MenuItemEvent(string menuItemName)
    {
        switch (menuItemName)
        {
            case "START GAME":
                GameManager.StartGame();
                break;
            case "QUIT GAME":
                GameManager.QuitGame();
                break;
            case "INTRODUCTION":
                GameManager.GoToIntroduction();
                break;
            default:
                Debug.Log("'" + menuItemName + "' not made yet");
                break;
        }
    }
}
