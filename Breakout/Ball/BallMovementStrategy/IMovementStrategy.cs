using System;
using Breakout.PlayerEntity;

namespace Breakout.Ball.BallMovementStrategy;

public interface IMovementStrategy {
    public void Move(Player player, Ball ball);
}
