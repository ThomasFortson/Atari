namespace Breakout.PowerUps;


using System;
using System.Dynamic;
using System.Numerics;
using Breakout.GameEvents;
using Breakout.PlayerEntity;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

public class DoubleSpeedEntity : PowerUpEntity {
    protected override PowerUpType PowerUpType => PowerUpType.DoubleSpeed;
    public DoubleSpeedEntity(Vector2 position) : base(position, new Image("Breakout.Assets.Images.SpeedPickUp.png")) {
    }

}
