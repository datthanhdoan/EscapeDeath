using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public GameObject gameOverUi;
    private PlayerMoverment playerMoverment;
    [SerializeField] private Animator anim;
    [SerializeField] private Animator[] animRev;
    //[SerializeField] private AudioManagerScript audioManagerScript;
    void Start()
    {
        playerMoverment = FindObjectOfType<PlayerMoverment>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMoverment.getDead() && !gameOverUi.activeSelf)
        {
            gameOverUI();
        }
        if (playerMoverment.getDead() && gameOverUi.activeSelf && Input.GetKeyDown("space"))
        {
            StartCoroutine(Load(SceneManager.GetActiveScene().buildIndex));
        }
    }
    public void gameOverUI()
    {
        gameOverUi.SetActive(true);
    }
    public void gameRestart()
    {
        StartCoroutine(Load((int)SceneManager.GetActiveScene().buildIndex));

    }
    public void StartOnce()
    {
        Debug.Log("Load scene once");
        StartCoroutine(Load(1));

    }
    public void nextScreen()
    {
        StartCoroutine(Load((int)SceneManager.GetActiveScene().buildIndex + 1));

    }
    public void previousScreen()
    {
        Debug.Log("Load Preious Screen");
        StartCoroutine(Load((int)SceneManager.GetActiveScene().buildIndex - 1));
    }
    public void Quit()
    {
        Application.Quit();
    }
    IEnumerator Load(int screenname)
    {
        anim.CrossFade(TransEnd, 0, 0, 0);
        if (animRev != null)
        {
            foreach (Animator animR in animRev)
            {
                animR.CrossFade(TransStart, 0, 0, 0);
            }
        }
        yield return new WaitForSeconds(1);

        if (screenname != (int)SceneManager.GetActiveScene().buildIndex)
        {
            //    AudioManagerScript.instance.StopBackgroundMusic();
            Destroy(AudioManagerScript.instance.gameObject);

            //}
            //else
            //{
            //    AudioManagerScript.instance = null;
        }
        SceneManager.LoadScene(screenname);
        anim.CrossFade(TransStart, 0, 0, 0);
        if (animRev != null)
        {
            foreach (Animator animR in animRev)
            {
                animR.CrossFade(TransEnd, 0, 0, 0);
            }
        }

    }
    #region Animation
    private static readonly int TransStart = Animator.StringToHash("Trans_Start");
    private static readonly int TransEnd = Animator.StringToHash("Trans_End");

    #endregion
}
