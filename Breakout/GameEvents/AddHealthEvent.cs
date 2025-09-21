namespace Breakout.GameEvents;

public readonly struct AddHealthEvent {
    public int Health {
        get;
    }

    public AddHealthEvent(int i) {
        Health += i;
    }

}