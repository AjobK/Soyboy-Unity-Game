using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BackgroundTilemap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager gm = FindObjectOfType<GameManager>();

        if (gm)
        {
            Vector3 randomBG = gm.GetRandomBackgroundRGB();
            //GetComponent<Tilemap>().color = new Color(randomBG[0], randomBG[1], randomBG[2]);
            GetComponent<Tilemap>().color = new Color(gm.GetWaveNumber() / 10f, 0.5f, 1f - (gm.GetWaveNumber() / 10f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
