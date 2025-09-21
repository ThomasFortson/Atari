namespace Breakout.Ball;

using System;
using System.Numerics;
using Breakout.Blocks;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Physics;
using Breakout.GameEvents;
using DIKUArcade.GUI;
using Breakout.PlayerEntity;
using Breakout.Ball.BallMovementStrategy;
using DIKUArcade.Input;
using DIKUArcade.Timers;

public class Ball : Entity {
    private const int HIT_POINTS = 20;
    private const float MOVEMENT_SPEED = 0.015f;
    private Vector2 dir = new Vector2(1, 1);
    private IMovementStrategy strategy;
    private Vector2 startExtent;
    public static int checkStart = 0;
    private bool isBig;
    public IMovementStrategy MovementStrategy {
        get => strategy;
    }

    public Ball(Vector2 position, Vector2 direction, IBaseImage image, IMovementStrategy strategy) : base(new DynamicShape(position, new Vector2(0.05f, 0.05f)), image) {
        dir = new Vector2(direction.X * MOVEMENT_SPEED, direction.Y * MOVEMENT_SPEED);
        ((DynamicShape) this.Shape).Velocity = dir;
        this.strategy = strategy;
        startExtent = Shape.Extent;
        BreakoutBus.Instance.Subscribe<DoubleSizedBallsEvent>(RegisterBigBallsEvent);
        BreakoutBus.Instance.Subscribe<NormalSizedBallsEvent>(RegisterNormalSizeBallsEvent);

    }
    public void RegisterBigBallsEvent(DoubleSizedBallsEvent e) {
        if (isBig)
            return;

        isBig = true;
        Vector2 newExtent = new Vector2(startExtent.X * e.SizeMultiplier, startExtent.Y * e.SizeMultiplier);

        Shape.Position = new Vector2(
            Math.Clamp(Shape.Position.X+Shape.Extent.X/2 - newExtent.X / 2, 0, 1),
            Math.Clamp(Shape.Position.Y+Shape.Extent.Y/2 - newExtent.Y / 2, 0, 1));
        Shape.Extent = newExtent;

    }
    public void RegisterNormalSizeBallsEvent(NormalSizedBallsEvent e) {
        if (!isBig)
            return;

        isBig = false;
        Vector2 oldExtent = Shape.Extent;
        Vector2 newExtent = startExtent;

        Shape.Position = new Vector2(
            Math.Clamp(Shape.Position.X+oldExtent.X/2 - newExtent.X / 2, 0, 1),
            Math.Clamp(Shape.Position.Y+oldExtent.Y/2 - newExtent.Y / 2, 0, 1));
        Shape.Extent = newExtent;
    }


    public void DetectCollision(EntityContainer<Block> blocks, Player player) {
        DetectBlockCollision(blocks);
        DetectPlayerCollision(player);
        DetectWallCollision();
    }

    private void DetectBlockCollision(EntityContainer<Block> blocks) {
        foreach (Block block in blocks) {
            CollisionData collision = CollisionDetection.Aabb((DynamicShape) this.Shape, (DynamicShape) block.Shape);
            if (collision.Collision) {

                block.Hit(HIT_POINTS);

                switch (collision.CollisionDir) {
                    case CollisionDirection.CollisionDirUp:
                        this.dir.Y = -this.dir.Y;
                        break;
                    case CollisionDirection.CollisionDirDown:
                        this.dir.Y = -this.dir.Y;
                        break;
                    case CollisionDirection.CollisionDirLeft:
                        this.dir.X = -this.dir.X;
                        break;
                    case CollisionDirection.CollisionDirRight:
                        this.dir.X = -this.dir.X;
                        break;
                }
                ((DynamicShape) this.Shape).Velocity = dir;

            }
        }
    }

    public void AddDieEvent() {
        var e = new BallDeathEvent(this.Shape.Position, this.Shape.Extent);
        BreakoutBus.Instance.RegisterEvent<BallDeathEvent>(e);
        this.DeleteEntity();
    }


    private void DetectWallCollision() {
        if (this.Shape.Position.Y >= 1.0f - this.Shape.Extent.Y) {
            this.dir.Y = -this.dir.Y;
            ((DynamicShape) this.Shape).Velocity = dir;
        }
        if (this.Shape.Position.X >= 1.0f - this.Shape.Extent.X) {
            this.dir.X = -this.dir.X;
            ((DynamicShape) this.Shape).Velocity = dir;
        }
        if (this.Shape.Position.X <= 0.0f) {
            this.dir.X = -this.dir.X;
            ((DynamicShape) this.Shape).Velocity = dir;
        }
        if (this.Shape.Position.Y <= 0.0f) {
            this.dir = new Vector2(0f, 0f);
            ((DynamicShape) this.Shape).Velocity = dir;
            AddDieEvent();
        }
    }

    private void DetectPlayerCollision(Player player) {
        CollisionData collision = CollisionDetection.Aabb((DynamicShape) this.Shape, (DynamicShape) player.Shape);
        CollisionData plCollisionData = CollisionDetection.Aabb(player.Shape.AsDynamicShape(), Shape.AsDynamicShape());
        // Collision with player
        if (collision.Collision || plCollisionData.Collision) {
            float paddleCenter = player.Shape.Position.X + player.Shape.Extent.X / 2;
            float ballCenter = this.Shape.Position.X + this.Shape.Extent.X / 2;
            float relativeIntersect = (ballCenter - paddleCenter) / (player.Shape.Extent.X / 2);

            relativeIntersect = Math.Clamp(relativeIntersect, -1f, 1f);

            float maxAngleOffset = MathF.PI / 6;

            float angleOffset = relativeIntersect * maxAngleOffset;
            float currentAngle = MathF.Atan2(this.dir.Y, this.dir.X);
            float newAngle = currentAngle + angleOffset;

            float speed = dir.Length();

            this.dir = new Vector2(
                speed * MathF.Cos(newAngle),
                speed * MathF.Sin(newAngle)
            );

            this.dir = new Vector2(this.dir.X, -this.dir.Y);
            ((DynamicShape) Shape).Velocity = dir;
        }
    }

    public void KeyHandler(KeyboardAction action, KeyboardKey key) {
        switch (key) {
            case KeyboardKey.Space:
                if (KeyboardAction.KeyPress == action) {
                    if (checkStart == 0) {
                        checkStart = 1;
                    }
                    this.strategy = new AutoMove();
                }
                break;
        }
    }
}
