namespace Breakout.PowerUps;

using System;
using System.Collections.Generic;
using Breakout.PlayerEntity;

public static class PowerUpStrategyGenerator {

    private static Dictionary<PowerUpType, Func<Player, IBasePowerUp>> powerUpStrategyFactory = new Dictionary<PowerUpType, Func<Player, IBasePowerUp>>() {

        { PowerUpType.DoubleSpeed, (Player p) => new DoubleSpeedStrategy(p) },
        { PowerUpType.NoPowerUp, (Player p) => new NoPowerUpStrategy(p) },
        { PowerUpType.AddHealth, (Player p) => new AddHealthStrategy(p) },
        { PowerUpType.Split, (Player p) => new SplitStrategy(p) },
        { PowerUpType.Hazard, (Player p) => new HazardPowerUp(p) },
        { PowerUpType.DoubleTime, (Player p) => new DoubleTimeStrategy(p) },
        { PowerUpType.HalfTimeHazard, (Player p) => new HalfTimeHazardStrategy(p) },
        { PowerUpType.BiggerBall, (Player p) => new BiggerBallStrategy() },

    };

    public static IBasePowerUp Generate(PowerUpType type, Player p) {
        if (powerUpStrategyFactory.TryGetValue(type, out var factory)) {
            return factory(p);
        }

        return new NoPowerUpStrategy(p);
    }
}
