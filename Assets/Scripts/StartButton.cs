using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{

    public Animator transitionAnim;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(ButtonClicked);
    }

    private void ButtonClicked()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        transitionAnim.SetTrigger("PanelOut");
        yield return new WaitForSeconds(0.5f);
        PlayerPrefs.SetInt("level", 1);
        SceneManager.LoadScene("1-1");
        
    }
}
