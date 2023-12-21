using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Typewriter : MonoBehaviour
{
    public TextMeshProUGUI label;

    private void Start()
    {
        if(!PlayerPrefs.HasKey("level"))
        {
            PlayerPrefs.SetInt("level", 2);
        }
        else
        {
            PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
        }
        StartCoroutine(Typer(PlayerPrefs.GetString("nextLevelText")));
    }

    IEnumerator Typer(string text)
    {
        var waitTimer = new WaitForSeconds(.04f);
        foreach (char c in text)
        {
            label.text = label.text + c;
            yield return waitTimer;
        }

        yield return new WaitForSeconds(1.5f);
        NextLevel();
    }

    public void NextLevel()
    {
        StartCoroutine(LoadScene());
    }

    public Animator transitionAnim;
    IEnumerator LoadScene()
    {
        if (PlayerPrefs.GetInt("level") == 9)
        {
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene("MainMenu");
        }
        transitionAnim.SetTrigger("PanelOut");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("1-"+ PlayerPrefs.GetInt("level"));
    }
}
