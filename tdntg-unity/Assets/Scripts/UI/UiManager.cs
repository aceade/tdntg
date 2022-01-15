using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    private GameStateManager gameStateManager;

    public Canvas pauseCanvas;

    public CanvasRenderer shipStatusPanel;

    private Text shipDetailsText, shipAttributesDisplay;

    private Slider engineSlider, rudderSlider;

    // Start is called before the first frame update
    void Start()
    {
        gameStateManager = GetComponent<GameStateManager>();
        shipDetailsText = shipStatusPanel.transform.Find("ShipDisplayText").GetComponent<Text>();
        shipAttributesDisplay = shipStatusPanel.transform.Find("ShipAttributesDisplay").GetComponent<Text>();
        engineSlider = shipStatusPanel.transform.Find("SpeedSlider").GetComponent<Slider>();
        rudderSlider = shipStatusPanel.transform.Find("RudderSlider").GetComponent<Slider>();
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

   public void showShipDetails(Ship ship) {
        shipDetailsText.text = ship.name;
        shipAttributesDisplay.text = string.Format("Health: {0}\nSpeed: {1}", ship.getHitPoints(), ship.getCurrentSpeed());

        engineSlider.minValue = ship.maxReverseSpeed;
        engineSlider.maxValue = ship.maxSpeed;
        engineSlider.value = ship.getCurrentSpeed();
        engineSlider.onValueChanged.RemoveAllListeners();
        engineSlider.onValueChanged.AddListener(delegate {
            ship.setTargetSpeed(engineSlider.value);
        });

        rudderSlider.minValue = -ship.maxTurningSpeed;
        rudderSlider.maxValue = ship.maxTurningSpeed;
        rudderSlider.value = ship.getCurrentRudder();
        rudderSlider.onValueChanged.RemoveAllListeners();
        rudderSlider.onValueChanged.AddListener(delegate {
            ship.setTurningSpeed(rudderSlider.value);
        });
        
   }

}
