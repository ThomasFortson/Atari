namespace Breakout.GameEvents;

using System.Numerics;
using DIKUArcade.Events;
public readonly struct NormalSizedBallsEvent {
    public int SizeMultiplier {
        get;
    }

    public NormalSizedBallsEvent(int sizeMultiplier) {
        SizeMultiplier = sizeMultiplier;
    }
}