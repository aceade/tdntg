
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
}
