using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    private GameStateManager gameStateManager;

    public Canvas pauseCanvas;

    public CanvasRenderer shipStatusPanel;

    private Text shipDetailsText, shipAttributesDisplay;

    private Slider engineSlider, rudderSlider;

    private Ship lastSelectedShip;

    private WaitForSeconds shipUpdateCycle = new WaitForSeconds(0.1f);
    private bool trackingShip;

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

    /// <summary>
    /// Display details of the specified ship, passed via the ship's command structure.
    /// Only called from player-controlled factions.
    /// </summary>
    /// <param name="ship"></param>
    public void showShipDetails(Ship ship) {
        DeselectShip();

        lastSelectedShip = ship;
        shipDetailsText.text = ship.name;
        shipAttributesDisplay.text = string.Format("Health: {0}\nSpeed: {1}", ship.getHitPoints(), ship.getCurrentSpeed());

        rudderSlider.enabled = true;
        engineSlider.enabled = true;

        engineSlider.minValue = ship.maxReverseSpeed;
        engineSlider.maxValue = ship.maxSpeed;
        engineSlider.value = ship.getCurrentSpeed();

        rudderSlider.minValue = -ship.maxTurningSpeed;
        rudderSlider.maxValue = ship.maxTurningSpeed;
        rudderSlider.value = ship.getCurrentRudder();

        if (!trackingShip) {
            trackingShip = true;
            StartCoroutine(trackShipDetails());
        }
   }

    /// <summary>
    /// Attached to an EventTrigger on the map background
    /// </summary>
   public void DeselectShip() {
        if (lastSelectedShip != null) {
            lastSelectedShip.Deselect();
            lastSelectedShip = null;
            shipAttributesDisplay.text = "";
            shipDetailsText.text = "";
            rudderSlider.enabled = false;
            engineSlider.enabled = false;
            trackingShip = false;
        }   
   }

    /// <summary>
    /// Track the ship and update it's rudder/engine
    /// </summary>
    /// <returns></returns>
   private IEnumerator trackShipDetails() {
       while (trackingShip) {
            shipAttributesDisplay.text = string.Format("Health: {0}\nSpeed: {1}", 
                lastSelectedShip.getHitPoints(), lastSelectedShip.getCurrentSpeed());
            lastSelectedShip.setTurningSpeed(rudderSlider.value);
            lastSelectedShip.setTargetSpeed(engineSlider.value);
            yield return shipUpdateCycle;
       }
   }

}
