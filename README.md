# tdntg
This is the prototype for a 2D naval tactics game set vaguely in the 1890s-1900s period.

## Required systems/components
Initial thoughts, may not be up to date

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

### Objective System
Most of the prototype will be a deathmatch - destroy all ships to win. However, there may be scope to expand this for future work. As such, each objective will have the following attributes:

- Optional/Required
- Current status (complete/incomplete)
- Category (survive for X minutes, destroy X ships, destroy ALL ships, X ships must reach position A/B/C)
- Parameters (based on the Category)