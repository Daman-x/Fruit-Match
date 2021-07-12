using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneManager : MonoBehaviour
{
    [SerializeField]
    Animation anim;
    [SerializeField]
    Image startbtn;
    [SerializeField]
    Text hint;
    private void Start()
    {
        StartCoroutine(Hint());
    }

    public void StartToMain()
    {
        anim.Play();
        StartCoroutine(loadscene());  
    }
    IEnumerator loadscene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MainScene");
    }
    
    IEnumerator Hint()
    {
        yield return new WaitForSeconds(20f);
        startbtn.color = new Color(1f,1f,1f);
        hint.gameObject.SetActive(true);
        hint.text = "Click on the Start button to begin the game";

    }
}
