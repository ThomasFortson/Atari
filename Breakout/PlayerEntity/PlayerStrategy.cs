namespace Breakout.PlayerEntity;

using System;
using System.Runtime.Serialization;
using DIKUArcade.Timers;
using Breakout.PowerUps;
using Microsoft.FSharp.Core;
using Breakout.Blocks;

public class PlayerStrategy {
    private List<IBasePowerUp> powerUpStrategies = new List<IBasePowerUp>();

    private Player player;

    private PowerUpType currentPowerUp;
    public PowerUpType CurrentPowerUp {
        get => currentPowerUp;
        set {
            currentPowerUp = value;
            if (value == PowerUpType.Hazard) {
                foreach (var powerUpStrategy in powerUpStrategies) {
                    powerUpStrategy.Disengage();
                }
                powerUpStrategies = new List<IBasePowerUp>();
            }
            powerUpStrategies.Add(PowerUpStrategyGenerator.Generate(value, player));
            if (powerUpStrategies.Count > 0) {
                powerUpStrategies.Last().Setup();
                powerUpStrategies.Last().PowerUpTypeP = value;
            }
        }
    }

    public PlayerStrategy(Player p) {
        player = p;
        CurrentPowerUp = PowerUpType.NoPowerUp;
    }


    public void Update() {
        List<IBasePowerUp> newPowerUpStrategies = new List<IBasePowerUp>();
        foreach (var powerUpStrategy in powerUpStrategies) {
            newPowerUpStrategies.Add(powerUpStrategy);
        }

        int i = 0;

        foreach (var powerUpStrategy in powerUpStrategies) {
            powerUpStrategy.Update((float) StaticTimer.GetElapsedSeconds());
            if (powerUpStrategy.IsDeleted) {
                newPowerUpStrategies.RemoveAt(i);
                i--;
            }
            i++;
        }
        powerUpStrategies = newPowerUpStrategies;
    }
}
