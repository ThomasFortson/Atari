namespace Breakout.States;

using System;
using System.Collections.Generic;
using System.Numerics;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Timers;
public class GameWon : IGameState {
    private StateMachine stateMachine;
    private Shape menuShape = new StationaryShape(0.0f, 0.0f, 1.0f, 1.0f);
    private Image menuImage = new Image("Breakout.Assets.Images.SpaceBackground.png");
    private Text gameWon = new Text("Game Won", new Vector2(0.05f, 0.8f), 1.5f);
    private Text newGameText = new Text("New Game", new Vector2(0.2f, 0.5f));
    private Text quitText = new Text("Main Menu", new Vector2(0.2f, 0.35f));
    private KeyboardKey currentKey = KeyboardKey.Up;
    private WindowContext window;
    public GameWon(StateMachine stateMachine) {
        this.stateMachine = stateMachine;
        newGameText.SetColor(255, 0, 0, 255);
        gameWon.SetColor(0, 255, 0, 255);
    }
    public void Update() {

    }
    public void Render(WindowContext context) {
        window = context;
        menuImage.Render(context, menuShape);
        newGameText.Render(context);
        quitText.Render(context);
        gameWon.Render(context);

    }
    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        switch (key) {
            case KeyboardKey.Up:
                if (action == KeyboardAction.KeyPress) {
                    currentKey = KeyboardKey.Up;
                    newGameText.SetColor(255, 0, 0, 255);
                    quitText.SetColor(255, 255, 255, 255);
                }
                break;
            case KeyboardKey.Down:
                if (action == KeyboardAction.KeyPress) {
                    currentKey = KeyboardKey.Down;
                    quitText.SetColor(255, 0, 0, 255);
                    newGameText.SetColor(255, 255, 255, 255);
                }
                break;
            case KeyboardKey.Enter:
                if (currentKey == KeyboardKey.Up && action == KeyboardAction.KeyPress) {
                    stateMachine.ActiveState = new GameRunning(stateMachine);
                } else if (currentKey == KeyboardKey.Down && action == KeyboardAction.KeyPress) {
                    stateMachine.ActiveState = new MainMenu(stateMachine);
                }
                break;
        }
    }
}