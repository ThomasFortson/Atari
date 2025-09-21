namespace Breakout;

using DIKUArcade.Events;
public sealed class BreakoutBus {
    private static GameEventBus? instance = null;

    public static GameEventBus Instance {
        get {
            if (instance == null) {
                instance = new GameEventBus();
            }
            return instance;
        }
    }
}