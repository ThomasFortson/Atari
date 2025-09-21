namespace Breakout.PlayerEntity;

using System.Numerics;
using Breakout.Ball;
using Breakout.GameEvents;
// using Breakout.PlayerEntity;
using Breakout.PowerUps;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Physics;

public class Player : Entity {
    const float MOVEMENT_SPEED = 0.015f;
    private float moveLeft = 0.0f;
    private float moveRight = 0.0f;
    private float moveUp = 0.0f;
    private float moveDown = 0.0f;
    private Vector2 extent = new Vector2(0.1f, 0.1f);
    private PlayerStrategy playerStrategy;
    private SpeedUpAnimation speedUpStride = new SpeedUpAnimation();
    private float speedMultiplier = 1.0f;
    public float SpeedMultiplier {
        get => speedMultiplier;
        set {
            speedMultiplier = value;
        }
    }

    public Player() : base(

        new DynamicShape(0.5f, 0.05f, 0.1f, 0.025f),
        new Image("Breakout.Assets.Images.player.png")) {

        BreakoutBus.Instance.Subscribe<PowerUpCollisionEvent>(RegisterPowerUpCollisionEvent);
        playerStrategy = new PlayerStrategy(this);

    }

    private void RegisterPowerUpCollisionEvent(PowerUpCollisionEvent e) {
        playerStrategy.CurrentPowerUp = e.Type;
    }

    public void Move() {
        playerStrategy.Update();
        Shape.Move();
        this.Shape.Position = new Vector2(Math.Clamp(Shape.Position.X, 0f, 1f - extent.X), Math.Clamp(Shape.Position.Y, 0f, 1f - extent.Y));
    }
    private void SetMoveLeft(bool val) {
        moveLeft = val ? -MOVEMENT_SPEED * SpeedMultiplier : 0f;
        UpdateVelocity();
    }

    private void SetMoveRight(bool val) {
        moveRight = val ? MOVEMENT_SPEED * SpeedMultiplier : 0f;
        UpdateVelocity();
    }

    private void UpdateVelocity() {
        Shape.AsDynamicShape().Velocity = new Vector2(moveRight + moveLeft, moveUp + moveDown);
    }

    public void KeyHandler(KeyboardAction action, KeyboardKey key) {
        switch (key) {
            case KeyboardKey.D:
                SetMoveRight(action == KeyboardAction.KeyPress);
                break;
            case KeyboardKey.A:
                SetMoveLeft(action == KeyboardAction.KeyPress);
                break;
        }
    }
}