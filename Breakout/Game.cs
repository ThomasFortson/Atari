namespace Breakout;
using DIKUArcade;
using DIKUArcade.Input;
using DIKUArcade.GUI;

using Breakout.States;
using Breakout.GameTimer;

public class Game : DIKUGame {
    private StateMachine stateMachine;

    private IGameState gameState;
    private CheckTime timerCheck;


    public Game(WindowArgs windowArgs) : base(windowArgs) {
        stateMachine = new StateMachine();
        gameState = stateMachine.ActiveState;
        timerCheck = new CheckTime(stateMachine);
    }

    public override void Update() {
        gameState = stateMachine.ActiveState;
        timerCheck.Check();
        gameState.Update();
    }

    public override void Render(WindowContext context) {
        gameState.Render(context);
    }

    public override void KeyHandler(KeyboardAction action, KeyboardKey key) {
        gameState = stateMachine.ActiveState;
        gameState.HandleKeyEvent(action, key);
    }

}