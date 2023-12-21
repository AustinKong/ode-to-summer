using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition instance; private void Awake() => instance = this;


    public Animator transitionAnim;

    public string nextSceneText;

    public void NextLevel()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(0.5f);
        AudioManager.instance.PlayComplete();
        yield return new WaitForSeconds(2f);
        AudioManager.instance.Fade();
        transitionAnim.SetTrigger("PanelOut");
        PlayerPrefs.SetString("nextLevelText", nextSceneText);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("TextScene");
    }

    public void RestartLevel()
    {
        StartCoroutine(RestartScene());
    }

    IEnumerator RestartScene()
    {
        transitionAnim.SetTrigger("PanelOut");
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
