using System;
using System.Numerics;
using Breakout.PlayerEntity;
namespace Breakout.Ball.BallMovementStrategy;

public class PlayerMove : IMovementStrategy {
    public void Move(Player player, Ball ball) {
        float posX = player.Shape.Position.X + (player.Shape.Extent.X / 2) - (ball.Shape.Extent.X / 2);
        float posY = player.Shape.Position.Y + (player.Shape.Extent.Y);
        ball.Shape.Position = new Vector2(posX, posY);
        ball.Shape.Move();
    }
}
