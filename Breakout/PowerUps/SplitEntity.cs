namespace Breakout.PowerUps;
using System;


using System.Dynamic;
using System.Numerics;
using Breakout.GameEvents;
using Breakout.PlayerEntity;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

public class SplitEntity : PowerUpEntity {
    protected override PowerUpType PowerUpType => PowerUpType.Split;
    public SplitEntity(Vector2 position) : base(position, new Image("Breakout.Assets.Images.SplitPowerUp.png")) {
    }

}
