using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool correct = false;

    public AudioSource tick;
    private float timer = 60f;

    public Image clock;

    public GameObject block;

    public GameObject win, lose, pause;
    public Text hint;
    public bool chance = false;

    public static bool touched = false;

    private void Start()
    {
        correct = false;
        touched = false;
        StartCoroutine(Timer());
    }
    public void Submit()
    {
        if(correct == true)
        {
            StopAllCoroutines();
            win.SetActive(true);
            block.SetActive(false);
            StartCoroutine(Win());
        }
    }

    public void Pause()
    {
        if(pause.activeSelf == true)
        {
            StartCoroutine(Timer());
            block.SetActive(true);
            pause.SetActive(false);
        }
        else
        {
            block.SetActive(false);
            StopAllCoroutines();
            pause.SetActive(true);
        }
    }
    public void Quit()
    {
        Application.Quit();
    }


    IEnumerator Win()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("StartScene");
    }


    IEnumerator Timer()
    {
        while(timer >= 0)
        {

            if(timer == 0)
            {

                if(Tracker.tries == 0)
                {
                    lose.SetActive(true);
                    block.SetActive(false);
                    yield return new WaitForSeconds(3f);
                    Destroy(this.gameObject);
                    SceneManager.LoadScene("StartScene");
                }
                else
                {

                    if(touched == false)
                    {
                        if(chance)
                        {
                            lose.SetActive(true);
                            block.SetActive(false);
                            yield return new WaitForSeconds(3f);
                            SceneManager.LoadScene("MainScene");
                        }
                        Tracker.tries--;
                        hint.text = "Oops! time is over " +
                            "Try to put Red Apples Together in next 20 sec";
                        yield return new WaitForSeconds(3f);
                        timer = 60f;
                        clock.fillAmount = 1f;
                        StartCoroutine(Timer());
                        chance = true;
                        break;
                    }
                    else
                    {
                        Tracker.tries--;
                        hint.text = "Sorry! you lost the game";
                        yield return new WaitForSeconds(3f);
                        lose.SetActive(true);
                        block.SetActive(false);
                        yield return new WaitForSeconds(2f);
                        SceneManager.LoadScene("MainScene");
                        break;
                    }

                  
                }
            }
            timer = timer - 3f;
            clock.fillAmount -= 0.05f;
            tick.Play();
            yield return new WaitForSeconds(1f);
        }

    }
}
