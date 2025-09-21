namespace Breakout.GameEvents;

using System.Numerics;
using DIKUArcade.Events;
public readonly struct BlockDeathEvent {
    public Vector2 Position {
        get;
    }

    public int Points {
        get;
    }

    public BlockDeathEvent(Vector2 position, int points) {
        Position = position;
        Points = points;
    }
}