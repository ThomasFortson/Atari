namespace Breakout.GameEvents;

using System.Numerics;
public readonly struct BallDeathEvent {
    public Vector2 Position {
        get;
    }
    public Vector2 Extent {
        get;
    }
    public BallDeathEvent(Vector2 position, Vector2 extent) {
        Position = position;
        Extent = extent;
    }

}