using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightPulsate : MonoBehaviour
{
    [SerializeField] float minRadius = 11f;
    [SerializeField] float maxRadius = 13f;
    [SerializeField] float radiusPerSecond = 2f;
    [SerializeField] float shrinkFactor = 1f; // 1 unit shrinked per second

    Light2D currentLight;
    [SerializeField] bool expanding = false;
    [SerializeField] bool canShrink = true;
    bool isDimming = false;

    // Start is called before the first frame update
    void Start()
    {
        currentLight = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDimming)
        {
            currentLight.pointLightOuterRadius -= currentLight.pointLightOuterRadius > 0 ? radiusPerSecond * 5f * Time.deltaTime : 0;

            if (currentLight.pointLightOuterRadius <= 0) currentLight.gameObject.SetActive(false);

            return;
        }

        currentLight.pointLightOuterRadius += radiusPerSecond * (expanding ? Time.deltaTime : -Time.deltaTime);

        if (currentLight.pointLightOuterRadius >= maxRadius) expanding = false;
        else if (currentLight.pointLightOuterRadius <= minRadius) expanding = true;
    }

    public void Dim()
    {
        Debug.Log("Dim the lights sum");
        isDimming = true;
    }

    public void Dim(float speedMultiplier)
    {
        Dim();
        radiusPerSecond *= speedMultiplier;
    }
}
