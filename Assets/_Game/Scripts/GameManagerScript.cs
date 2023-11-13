using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public GameObject gameOverUi;
    public GameObject VictoryScene;
    private PlayerMoverment playerMoverment;
    public Boss_1 boss1;

    //[SerializeField] private AudioManagerScript audioManagerScript;
    void Start()
    {
        playerMoverment = FindObjectOfType<PlayerMoverment>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMoverment._currentHealth <= 0 && !gameOverUi.activeSelf)
        {
            gameOverUI();
        }
        if (boss1.getCurrentHealth() <= 0 && !VictoryScene.activeSelf)
        {
            VictoryScene.SetActive(true);
        }
        if (playerMoverment._currentHealth <= 0 && gameOverUi.activeSelf && Input.GetKeyDown("space"))
        {
            StartCoroutine(Load(SceneManager.GetActiveScene().buildIndex));
        }
        if (boss1.getCurrentHealth() <= 0 && VictoryScene.activeSelf && Input.GetKeyDown("space"))
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
        SceneManager.LoadScene("FirstScreen");

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

        yield return new WaitForSeconds(0);

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


    }

}
