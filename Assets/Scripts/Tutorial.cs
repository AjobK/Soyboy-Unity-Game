using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    int currentSlideIndex = 0;
    int slidesAmount = 0;

    // Start is called before the first frame update
    void Start()
    {
        slidesAmount = transform.childCount;

        Debug.Log(slidesAmount);

        ShowSlide();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire"))
        {
            NextSlide();
        }
    }

    private void NextSlide()
    {
        HideSlide();

        if (++currentSlideIndex < slidesAmount)
            ShowSlide();
        else
            GameManager.GoBackToStartScreen();
    }

    private void ShowSlide()
    {
        transform.GetChild(currentSlideIndex).gameObject.SetActive(true);
    }

    private void HideSlide()
    {
        transform.GetChild(currentSlideIndex).gameObject.SetActive(false);
    }
}
