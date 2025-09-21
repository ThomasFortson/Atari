namespace Breakout.GameEvents;

using System.Numerics;
using DIKUArcade.Events;
public readonly struct GameWonEvent {
    public int Health {
        get;
    }

    public GameWonEvent() {
        
    }

}