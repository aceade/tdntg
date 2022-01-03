using System.Collections.Generic;
using UnityEngine;

public class Faction : MonoBehaviour
{
    public List<Ship> myShips;

    protected bool visibleAtStart = false;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        myShips.ForEach(x => x.setCommander(this));
        if (!visibleAtStart) {
            myShips.ForEach(x => x.toggleRendering(false));
        }
    }

    public virtual void ShipSpotted(Ship enemyShip) {
        // no-op by default
    }

    public virtual void ShipDetectionLost(Ship enemyShip) {
        // no-op for dummy
    }

    public void alliedShipDestroyed(Ship alliedShip) {
        myShips.Remove(alliedShip);
    }

    public void enemyShipDestroyed(Ship enemyShip) {
        // TODO
    }
}
