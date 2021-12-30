using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    /// <summary>
    /// At the moment, this will use a single level.
    /// </summary>
    public void StartTheGame() {
        SceneManager.LoadScene("Main");
    }
}
