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
using Breakout.Ball;
public class GamePaused : IGameState {

    private StateMachine stateMachine;
    private Shape menuShape = new StationaryShape(0.0f, 0.0f, 1.0f, 1.0f);
    private Image menuImage = new Image("Breakout.Assets.Images.shipit_titlescreen.png");
    private Text continueGame = new Text("Continue: Press Enter", new Vector2(0.2f, 0.6f), 0.5f);
    private Text goMenu = new Text("Go to Main Menu", new Vector2(0.2f, 0.4f), 0.5f);
    private KeyboardKey currentKey = KeyboardKey.Up;
    public GamePaused(StateMachine stateMachine) {
        this.stateMachine = stateMachine;
        continueGame.SetColor(255, 0, 0, 255);
    }
    public void Update() {

    }
    public void Render(WindowContext context) {
        menuImage.Render(context, menuShape);
        continueGame.Render(context);
        goMenu.Render(context);
    }

    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        switch (key) {
            case KeyboardKey.Enter:
                if(action == KeyboardAction.KeyPress){
                    if (currentKey == KeyboardKey.Up) {
                        stateMachine.ActiveState = stateMachine.PreviousState;

                    } else if (currentKey == KeyboardKey.Down) {
                        stateMachine.ActiveState = new MainMenu(stateMachine);
                    }

                }
                break;
            case KeyboardKey.Up:
                if(action == KeyboardAction.KeyPress){
                    currentKey = KeyboardKey.Up;
                    goMenu.SetColor(255, 255, 255, 255);
                    continueGame.SetColor(255, 0, 0, 255);
                }
                break;
            case KeyboardKey.Down:
                if(action == KeyboardAction.KeyPress) {
                    currentKey = KeyboardKey.Down;
                    goMenu.SetColor(255, 0, 0, 255);
                    continueGame.SetColor(255, 255, 255, 255);
                }
                break;
        }
    }
}