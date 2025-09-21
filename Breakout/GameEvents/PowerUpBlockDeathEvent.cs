using System;
using System.Numerics;
using Breakout.Blocks;

namespace Breakout.GameEvents;

public class PowerUpBlockDeathEvent {
    public Vector2 Position {
        get;
    }

    public PowerUpBlockDeathEvent(Vector2 position) {
        Position = position;
    }
}
