using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    [SerializeField] private GameObject[] _textScreens;
    [SerializeField] private float _delay;
    [SerializeField] private string _scene;

    private float _fadeTime = 1f;
    private float _timer = 0f;
    private CanvasRenderer _current;
    private int _fade = 1;

    void Start() {
        foreach (GameObject screen in _textScreens)
        {
            CanvasRenderer canvasRenderer = screen.GetComponent<CanvasRenderer>();
            canvasRenderer.SetAlpha(0f);
        }

        StartCoroutine(ShowScreens());
    }

    void Update()
    {
        if (_current != null) 
        {
            _timer += Time.deltaTime * _fade;
            float alpha = Mathf.Lerp(0f, 1f, _timer / _fadeTime);
            _current.SetAlpha(alpha);
        }
    }

    IEnumerator ShowScreens() {
        foreach (GameObject screen in _textScreens)
        {
            _current = screen.GetComponent<CanvasRenderer>();
            screen.SetActive(true);
            _fade = 1;
            yield return new WaitForSeconds(_fadeTime);
            _fade = 0;
            yield return new WaitForSeconds(_delay);
            _fade = -1;
            yield return new WaitForSeconds(_fadeTime);
            screen.SetActive(false);
        }

        SceneManager.LoadScene(_scene);
    }
}
