using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    [SerializeField] private GameObject[] _textScreens;
    [SerializeField] private float _delay;

    void Start() {
        StartCoroutine(ShowScreens());
    }

    IEnumerator ShowScreens() {
        foreach (GameObject screen in _textScreens)
        {
            screen.SetActive(true);
            yield return new WaitForSeconds(_delay);
            screen.SetActive(false);
        }

        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
