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

    public void ShowOptionsPanel() {
        throw new System.NotImplementedException("Options will be added later");
    }

    public void ShowLevelsMenu() {
        throw new System.NotImplementedException("Only one level for now");
    }
}
