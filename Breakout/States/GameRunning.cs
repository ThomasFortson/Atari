namespace Breakout.States;

using System.Collections.Generic;
using System.Numerics;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using Breakout.Blocks;
using Breakout.PlayerEntity;
using Breakout.States;
using Breakout.Level;
using Breakout.Ball;
using Breakout.Score;
using Breakout.Lives;
using Breakout.GameEvents;
using Breakout.PowerUps;
using Breakout.GameTimer;
using DIKUArcade.Timers;
using Breakout.Ball.BallMovementStrategy;

public class GameRunning : IGameState {
    private List<string> levelOrder = new List<string> {
        "level1", "level2", "level3"

    };

    private EntityContainer<Block> level;
    private Player player;
    const int SPLIT_LIMIT = 50;
    private EntityContainer<Ball> ballContainer = new EntityContainer<Ball>();
    private Text pauseGame = new Text("Press E to pause", new Vector2(0.1f, 0.02f), 0.2f);
    private Text launchText = new Text("Press space to launch ball", new Vector2(0.65f, 0.02f), 0.2f);
    private Shape backgroundShape = new StationaryShape(0.0f, 0.0f, 1.0f, 1.0f);
    private int levelNumber = 0;
    private WinLossStatus gameStatus;
    private Image backgroundImage = new Image("Breakout.Assets.Images.SpaceBackground.png");
    private ScoreContainer scoreContainer;
    private StateMachine stateMachine;
    private LifeContainer lifeContainer;
    private BallAnimation ballAnimation;
    private TimerDisplay timerDisplay;
    private EntityContainer<PowerUps.PowerUpEntity> powerUpContainer = new EntityContainer<PowerUpEntity>();
    private const int LIVES = 3;
    private IMovementStrategy movementStrategy = new PlayerMove();
    private CheckTime checkTime;

    public GameRunning(StateMachine stateMachine) {
        level = new EntityContainer<Block>();
        timerDisplay = new TimerDisplay(120);
        checkTime = new(timerDisplay);
        IncrementLevel();
        Ball.checkStart = 0;
        lifeContainer = new LifeContainer(LIVES);
        gameStatus = new WinLossStatus(lifeContainer, stateMachine, level, levelOrder.Count);

        player = new Player();
        scoreContainer = new ScoreContainer();

        ballContainer = new GenerateBalls().Generate(3, movementStrategy);
        ballAnimation = new BallAnimation();

        this.stateMachine = stateMachine;

        Setup();
    }

    private void RegisterBallDeath(BallDeathEvent e) {
        ballAnimation.TriggerAnimation(e.Position, e.Extent);
        if (ballContainer.CountEntities() <= 0) {
            lifeContainer.Lives -= 1;
            ballContainer = new GenerateBalls().Generate(1, movementStrategy);
        }
    }

    private void RegisterAddHealth(AddHealthEvent e) {
        lifeContainer.Lives += e.Health;
    }

    private void RegisterBlockDeath(PowerUpBlockDeathEvent e) {
        PowerUpEntity powerUp = PowerUpGenerator.Generate(e.Position);
        powerUpContainer.AddEntity(powerUp);
    }

    private void RegisterSplitBall(SplitBallEvent e) {
        if (ballContainer.CountEntities() >= SPLIT_LIMIT)
            return;
        int i = 0;
        EntityContainer<Ball> newBallContainer = new EntityContainer<Ball>();
        ballContainer.Iterate(ball => {
            new GenerateBalls().SplitBalls(Math.Clamp(SPLIT_LIMIT - newBallContainer.CountEntities(), 1, 3), ball.MovementStrategy, ball.Shape.Position, ball.Shape.AsDynamicShape().Velocity).Iterate(ball2 => {
                i += Math.Clamp(SPLIT_LIMIT - newBallContainer.CountEntities(), 1, 3);
                newBallContainer.AddEntity(ball2);
            });
        });
        ballContainer.ClearContainer();
        newBallContainer.Iterate(ballContainer.AddEntity);
        newBallContainer.ClearContainer();


    }

    private void RegisterGameWon(GameWonEvent e) {
        ballContainer.ClearContainer();
        powerUpContainer.ClearContainer();
        level.ClearContainer();
        player.DeleteEntity();

    }

    private void Setup() {

        BreakoutBus.Instance.Subscribe<BallDeathEvent>(RegisterBallDeath);
        BreakoutBus.Instance.Subscribe<PowerUpBlockDeathEvent>(RegisterBlockDeath);
        BreakoutBus.Instance.Subscribe<AddHealthEvent>(RegisterAddHealth);
        BreakoutBus.Instance.Subscribe<SplitBallEvent>(RegisterSplitBall);
        BreakoutBus.Instance.Subscribe<GameWonEvent>(RegisterGameWon);
    }

    public void Update() {
        // checks for loss
        gameStatus.Check(levelNumber, level);
        // updates seconds in the timer
        timerDisplay.Update();

        scoreContainer.Update();
        player.Move();

        powerUpContainer.Iterate(powerUp => {
            powerUp.Move();
            powerUp.DetectCollision(player);
        }
        );

        ballContainer.Iterate(ball => {
            ball.DetectCollision(level, player);
            ball.MovementStrategy.Move(player, ball);
        });

        level.Iterate(block =>
            block.Update()
        );

        if (level.CountEntities() == 0) {
            IncrementLevel();
        }

        BreakoutBus.Instance.ProcessEvents();
    }

    public void Render(WindowContext context) {
        backgroundImage.Render(context, backgroundShape);
        pauseGame.Render(context);
        launchText.Render(context);
        timerDisplay.Render(context);
        lifeContainer.Render(context);

        player.RenderEntity(context);

        scoreContainer.RenderScore(context);
        ballAnimation.RenderAnimations(context);

        level.RenderEntities(context);
        ballContainer.RenderEntities(context);
        powerUpContainer.RenderEntities(context);
    }

    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        player.KeyHandler(action, key);
        ballContainer.Iterate(ball => ball.KeyHandler(action, key));
        switch (key) {
            case KeyboardKey.E:
                if (action == KeyboardAction.KeyPress) {
                    stateMachine.ActiveState = new GamePaused(stateMachine);
                }
                break;
            case KeyboardKey.L:
                if (action == KeyboardAction.KeyPress) {
                    IncrementLevel();
                }
                break;
            case KeyboardKey.P:
                if (action == KeyboardAction.KeyPress) {
                    foreach (Block i in level) {
                        if (i is not Unbreakable) {
                            i.DeleteEntity();
                            break;
                        }
                    }
                }
                break;
        }
    }

    private void IncrementLevel() {
        if (levelNumber < levelOrder.Count) {
            levelNumber++;
        }
        ballContainer.ClearContainer();
        ballContainer = new GenerateBalls().Generate(3, new PlayerMove());
        level = LevelLoader.LoadLevel($"Breakout.Assets.Levels.{levelOrder[levelNumber - 1]}.txt");
        int newTime = LevelLoader.GetTimerTime($"Breakout.Assets.Levels.{levelOrder[levelNumber - 1]}.txt");
        timerDisplay.ResetTimer(newTime);

    }

    public LifeContainer GetLifeContainer() {
        return lifeContainer;
    }

    public EntityContainer<Ball> GetBallContainer() {
        return ballContainer;
    }
}