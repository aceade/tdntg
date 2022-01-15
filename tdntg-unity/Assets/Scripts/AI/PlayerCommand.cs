using UnityEngine;

public class PlayerCommand : Faction
{
    
    public UiManager uiManager;

    protected override void Start() {
        base.visibleAtStart = true;
        base.Start();
    }

    public override void ShipSpotted(Ship ship) {
        ship.toggleRendering(true);
    }

    public override void ShipDetectionLost(Ship enemyShip) {
        enemyShip.toggleRendering(false);
    }

    public override void shipSelected(Ship ship) {
        Debug.LogFormat("Ship {0} selected", ship);
        uiManager.showShipDetails(ship);
    }
}
