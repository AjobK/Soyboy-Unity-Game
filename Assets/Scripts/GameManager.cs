using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] int wavesAmount = 4;
    [SerializeField] int waveNumber = 1;
    [SerializeField] int coinsAmount = 0;
    [SerializeField] int heartsAmount = 6;
    [SerializeField] int ammoAmount = 5;

    Vector3 waveBackgroundRGB;

    private void Awake()
    {
        SetRandomBackgroundRGB();

        Debug.Log(FindObjectsOfType<GameManager>().Length);
        if (FindObjectsOfType<GameManager>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void SetRandomBackgroundRGB()
    {
        waveBackgroundRGB = new Vector3(
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f)
        );
    }

    public Vector3 GetRandomBackgroundRGB()
    {
        return waveBackgroundRGB;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name.Equals("End Screen"))
        {
            if (Input.GetButtonDown("Fire"))
            {
                GoToStartScreen();
            }
        }
    }

    public int GetWaveNumber()
    {
        return waveNumber;
    }

    public int GetHeartsAmount()
    {
        return heartsAmount;
    }

    public int GetCoinsAmount()
    {
        return coinsAmount;
    }

    public int GetAmmoAmount()
    {
        return ammoAmount;
    }

    public void SetCoinsAmount(int amount)
    {
        coinsAmount = amount;
    }

    public void SetHeartsAmount(int amount)
    {
        heartsAmount = amount;
    }

    public void SetAmmoAmount(int amount)
    {
        ammoAmount = amount;
    }

    public void AddCoinsAmount(int amount)
    {
        coinsAmount += amount;
    }

    public void GoToNextLevel()
    {
        StartCoroutine(GoToNextLevelRoutine());
    }

    public void GoToEndScreen()
    {
        StartCoroutine(GoToEndScreenRoutine());
    }

    public void GoToStartScreen()
    {
        StartCoroutine(GoToStartScreenRoutine());
    }

    public static void GoBackToStartScreen()
    {
        SceneManager.LoadScene("Start Screen");
    }

    public static void GoToIntroduction()
    {
        SceneManager.LoadScene("Introduction");
    }

    public static void StartGame()
    {
        SceneManager.LoadScene("Map 1");
    }

    public static void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator GoToNextLevelRoutine()
    {
        Player player = FindObjectOfType<Player>();

        if (player != null)
        {
            SetHeartsAmount((int)player.GetComponent<Health>().GetMaxHealth());
            SetCoinsAmount(player.GetCoinsAmount());
            SetAmmoAmount(player.GetComponent<PlayerShooter>().GetMaxAmmo());
        }

        yield return new WaitForSecondsRealtime(1.5f);
        
        string sceneName = SceneManager.GetActiveScene().name;

        if (waveNumber % wavesAmount != 0 || sceneName.Equals("Break Map"))
        {
            waveNumber++;

            if (waveNumber - 1 % wavesAmount == 0) SetRandomBackgroundRGB();

            SceneManager.LoadScene("Map " + ((waveNumber - 1) % wavesAmount + 1));
        }
        else
        {
            SceneManager.LoadScene("Break Map");
        }
    }

    IEnumerator GoToEndScreenRoutine()
    {
        yield return new WaitForSecondsRealtime(0.5f);

        SceneManager.LoadScene("End Screen");
    }

    IEnumerator GoToStartScreenRoutine()
    {
        yield return new WaitForSecondsRealtime(0f);

        SceneManager.LoadScene("Start Screen");
        Destroy(gameObject);
    }
}
