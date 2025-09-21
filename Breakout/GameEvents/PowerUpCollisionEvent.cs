namespace Breakout.GameEvents;

using System.Numerics;
using DIKUArcade.Events;
public readonly struct PowerUpCollisionEvent {
    public PowerUpType Type {
        get;
    }
    public PowerUpCollisionEvent(PowerUpType powerUpType) {
        Type = powerUpType;
    }

}