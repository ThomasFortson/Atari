namespace Breakout.GameEvents;

public readonly struct DoubleSizedBallsEvent {
    public int SizeMultiplier {
        get;
    }

    public DoubleSizedBallsEvent(int sizeMultiplier) {
        SizeMultiplier = sizeMultiplier;
    }
}