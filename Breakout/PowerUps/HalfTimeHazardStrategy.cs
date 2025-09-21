namespace Breakout.PowerUps;


using System;
using System.Dynamic;
using System.Numerics;
using Breakout.GameEvents;
using Breakout.GameTimer;
using Breakout.PlayerEntity;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Timers;

public class HalfTimeHazardStrategy : IBasePowerUp {
    private Player player;
    private TimerDisplay timer = new TimerDisplay();
    private bool hit = false;

    public HalfTimeHazardStrategy(Player p) {
        player = p;
    }
    public override void Setup() {
        if (hit == false) {
            timer.SetTime((int) timer.GetSeconds() / 2);
            hit = true;

        }

    }

}