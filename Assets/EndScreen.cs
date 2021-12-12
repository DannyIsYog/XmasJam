using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    public GameObject textMeshObj;
    TextMeshProUGUI textMesh;
    public static bool win = true;

    private void Start()
    {
        textMesh = textMeshObj.GetComponent<TextMeshProUGUI>();
        textMesh.text = win ? "Congratz you won, want a croquet?" : "lol u died :)";
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene("Fight");
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
