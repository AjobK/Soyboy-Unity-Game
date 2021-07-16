using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveCountUI : MonoBehaviour
{
    // Cached reference
    GameManager gm;
    int waveNumber;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();

        if (gm)
        {
            waveNumber = gm.GetWaveNumber();
            GetComponent<TextMeshProUGUI>().text = "WAVE " + waveNumber;
        }
    }

    // Update is called once per frame
    void Update()
    {
        gm = FindObjectOfType<GameManager>();

        if (gm && waveNumber != gm.GetWaveNumber())
        {
            waveNumber = gm.GetWaveNumber();
            GetComponent<TextMeshProUGUI>().text = "WAVE " + waveNumber;
        }
    }
}
