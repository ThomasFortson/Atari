namespace Breakout.PowerUps;


using System;
using System.Dynamic;
using System.Numerics;
using Breakout.GameEvents;
using Breakout.PlayerEntity;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Timers;

public class BiggerBallStrategy : IBasePowerUp {
    public BiggerBallStrategy() {
        TimeCreated = (float) StaticTimer.GetElapsedSeconds();
    }

    public override void Setup() {
        BreakoutBus.Instance.RegisterEvent(new DoubleSizedBallsEvent(2));

    }

    public override void Update(float t) {
        t = (float) StaticTimer.GetElapsedSeconds();
        if (t - TimeCreated > 5) {

            Disengage();
        }
    }

    public override void Disengage() {
        BreakoutBus.Instance.RegisterEvent(new NormalSizedBallsEvent(2));
        IsDeleted = true;
    }
}
