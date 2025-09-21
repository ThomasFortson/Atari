namespace Breakout.GameEvents;

using System.Numerics;
using DIKUArcade.Events;
public readonly struct LoseHealthEvent {
    public Vector2 Position {
        get;
    }
    public Vector2 Extent {
        get;
    }
    public LoseHealthEvent(Vector2 position, Vector2 extent) {
        Position = position;
        Extent = extent;
    }

}