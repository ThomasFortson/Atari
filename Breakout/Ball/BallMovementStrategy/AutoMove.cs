namespace Breakout.Ball.BallMovementStrategy;

using System;
using System.Numerics;
using Breakout.PlayerEntity;
using DIKUArcade.Entities;
using DIKUArcade.Physics;


public class AutoMove : IMovementStrategy {
    public void Move(Player player, Ball ball) {
        ball.Shape.Move();
    }
}
