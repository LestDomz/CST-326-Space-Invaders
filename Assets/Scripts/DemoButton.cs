using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoButton : MonoBehaviour
{
    public GameObject someReference;

    private void Awake()
    {
        GameObject.DontDestroyOnLoad(gameObject);
    }

    public void loadGameScene()
    {
        StartCoroutine(_LoadGameScene());

        IEnumerator _LoadGameScene()
        {
            AsyncOperation loadOp = SceneManager.LoadSceneAsync("Game");
            while (!loadOp!.isDone) yield return null;

            GameObject player = GameObject.Find("Player");
            Debug.Log(player.name);
        }
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }
}