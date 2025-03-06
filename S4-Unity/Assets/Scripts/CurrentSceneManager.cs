using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CurrentSceneManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public VoidEventChannel onPlayerDeath;
    public VoidEventChannel onPause;
    public GameObject pauseScreen;
    private void OnEnable()
    {    
        onPlayerDeath.OnEventRaised+= Die;
        onPause.OnEventRaised+= Die;
    }
    private void OnDisable()
    {    
        onPlayerDeath.OnEventRaised-= Die;
        onPause.OnEventRaised-= Die;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Die(){
        gameOverScreen.SetActive(true);
    }
    void Start()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }

    public void pause(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            if(Time.timeScale == 0){
                Time.timeScale = 1;
                pauseScreen.SetActive(false);
            } else {
                Time.timeScale = 0;
                pauseScreen.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        pause();
    }

    public void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
