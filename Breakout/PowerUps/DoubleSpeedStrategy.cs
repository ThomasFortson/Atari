namespace Breakout.PowerUps;


using System;
using System.Dynamic;
using System.Numerics;
using Breakout.GameEvents;
using Breakout.PlayerEntity;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Timers;

public class DoubleSpeedStrategy : IBasePowerUp {

    const float SPEED_MULTIPLIER = 1.5f;

    private Player player;

    public DoubleSpeedStrategy(Player p) {
        player = p;
        TimeCreated = (float) StaticTimer.GetElapsedSeconds();
    }

    public override void Setup() {
        player.SpeedMultiplier = SPEED_MULTIPLIER;
        player.Image = new SpeedUpAnimation().Stride();

    }

    public override void Update(float t) {
        t = (float) StaticTimer.GetElapsedSeconds();
        if (t - TimeCreated > 5) {
            Disengage();
        }
    }

    public override void Disengage() {
        player.SpeedMultiplier = 1.0f;
        player.Image = new Image("Breakout.Assets.Images.player.png");
        IsDeleted = true;
    }
}
