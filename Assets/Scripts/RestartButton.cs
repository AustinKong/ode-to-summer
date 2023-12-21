using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    private void Start() => GetComponent<Button>().onClick.AddListener(ButtonClicked);

    private void ButtonClicked()
    {
        SceneTransition.instance.RestartLevel();
    }
}
