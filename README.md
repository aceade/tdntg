# tdntg
This is the prototype for a 2D naval tactics game set vaguely in the 1890s-1900s period.

## Required systems/components
These are initial thoughts and may not be up to date.

### Ship
The base unit of the game. Has the following attributes:

- Class
- Hitpoints
- Max speed
- Max turning speed
- Weapons
- Armour thickness (if any)
- Spotting radius (how far can the crew see)
- Detection radius (how far away can _it_ be seen)
- Systems (rudder, engine, smoke(if possible))

#### Ship class
Different ships will be available in the game:

- Monitor
	- Slow, lightly armoured, but heavily armed. Mainly for coastal defence or shore bombardment.
	- Short detection radius
	- Immune to torpedos due to shallow draught
- Torpedo boat
	- Fast and manouverable, no armour, limited range from shore. Only - armed with torpedos and smoke generators.
	- Very short detection radius
	- Hit-and-run: if being targeted, break off
- Destroyer
	- Fast, very limited armour, light armament. Specialise in hunting torpedo boats
	- Short detection radius
- Cruiser
	- Fast, limited armour, mostly light armament.
- Battlecruiser
	- Glass cannon - fast, no armour, heavily armed. Very vulnerable to - torpedos and battleship guns
	- 2nd highest detection radius
	- Favour alpha strikes
- Battleship
	- Slow, heavy armour, heavy armament. Vulnerable to torpedos
	- Highest detection radius
	- Favour alpha strikes
	
#### Ship weapons
Weapons have the following attributes:

- Type (Torpedos, Light, Heavy)
- Vertical firing angle
- Horiztonal rotation
- Reload time
- Ammo type (HE, AP)
- Dispersion

#### Ship systems
Rudders, engines, weapons, magazines.
Have maximum health and effect upon being disabled:
- Rudders lose some steering
- Engines lose some max speed and acceleration
- Weapons never come back online
- Blow up in spectactular fashion

### Objective System
Most of the prototype will be a deathmatch - destroy all ships to win. However, there may be scope to expand this for future work. As such, each objective will have the following attributes:

- Optional/Required
- Current status (complete/incomplete)
- Category (survive for X minutes, destroy X ships, destroy ALL ships, X ships must reach position A/B/C)
- Parameters (based on the Category)

### User Interface
The game plays from a top-down view of a map with ships models positioned across the screen. The player's models will be represented by red ships, and the enemy by yellow. Ships whose position is not immediately known will be dimmed.
Clicking on a player-controlled ship will allow the player to issue orders to that ship.

### Enemy AI
In the initial stage, the enemy ships will be stationary while attacking. Their criteria for choosing a target will be as follows:
- Range
- Threat to self
- Is it presenting a broadside? (especially for Battleships/Battlecruisers)
- Is it targeting me?
- Can I damage it?

### Friendly AI
Player-controlled ships will eventually have some autonomy, but can be overridden by the player.
This AI will initially handle collision avoidance, which can later be shared with moving enemies.

**Collision avoidance criteria:
Larger and faster ships have right-of-way over smaller ones


### Sound design
TBD

### Graphics
Minimalistic graphics

## Deployment
WebGL for prototype. If this expands to a larger piece, a standalone Windows/Linux/MacOS build may be made available.

## Schedule

**First release
- Interface
- Player-controlled ships: movement, attacking
	- Collision avoidance
	- Attacking requires player control for now
- Initial objective: destroy ALL enemies

**Second release
Enemy ships shoot back:
	- Target selection
	
**Third release
Enemy ships move