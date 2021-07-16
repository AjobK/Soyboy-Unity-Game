using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    // Cached reference
    Animator myAnimator;
    Player player;

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        player = collision.gameObject.GetComponent<Player>();
        
        if (player && player.GetKeysAmount() >= 1)
        {
            player.UseKey();
            GetComponent<BoxCollider2D>().enabled = false;
            myAnimator.SetTrigger("open");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            if (SceneManager.GetActiveScene().name.Equals("Break Map"))
            {
                FindObjectOfType<LightPulsate>().Dim(2f);
                Debug.Log("Dimming hard bro");
            }
            else
            {
                FindObjectOfType<LightPulsate>().Dim();
            }

            GetComponent<BoxCollider2D>().enabled = true;
            player.SetVictoryWalk(true);
            myAnimator.SetTrigger("close");
            Debug.Log("Going to next level");
            FindObjectOfType<GameManager>().GoToNextLevel();
        }
    }
}
