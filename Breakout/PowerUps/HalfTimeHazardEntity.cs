namespace Breakout.PowerUps;


using System;
using System.Dynamic;
using System.Numerics;
using Breakout.GameEvents;
using Breakout.PlayerEntity;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

public class HalfTimeHazardEntity : PowerUpEntity {
    protected override PowerUpType PowerUpType => PowerUpType.HalfTimeHazard;
    public HalfTimeHazardEntity(Vector2 position) : base(position, new Image("Breakout.Assets.Images.watch.png")) {
    }

}
