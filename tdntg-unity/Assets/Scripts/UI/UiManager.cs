using UnityEngine;

public class UiManager : MonoBehaviour
{
    private GameStateManager gameStateManager;

    public Canvas pauseCanvas;

    // Start is called before the first frame update
    void Start()
    {
        gameStateManager = GetComponent<GameStateManager>();
        HidePanel(pauseCanvas);
    }

   public void Pause() {
       gameStateManager.Pause();
       ShowPanel(pauseCanvas);
   }

   public void Resume() {
       gameStateManager.Resume();
       HidePanel(pauseCanvas);
   }

   public void RestartLevel() {
       gameStateManager.Restart();
   }

   public void Exit() {
       gameStateManager.Exit();
   }

   private void HidePanel(Canvas canvas) {
       canvas.gameObject.SetActive(false);
   }

   private void ShowPanel(Canvas canvas) {
       canvas.gameObject.SetActive(true);
   }
}
