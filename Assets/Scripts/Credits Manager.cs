using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    public float creditsDuration = 5.0f; // Time before returning to the main menu

    void Start()
    {
        Invoke("ReturnToMainMenu", creditsDuration);
    }

    void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Menu"); // Make sure "MainMenu" is the correct scene name
    }
}
