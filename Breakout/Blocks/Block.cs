namespace Breakout.Blocks;

using System;
using DIKUArcade;
using DIKUArcade.Events;
using DIKUArcade.Input;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Entities;
using Breakout.Score;
using Breakout.GameEvents;

public abstract class Block : Entity {
    public string SpritePath = "";
    public int Health;
    protected int StartHealth;
    public EntityContainer<Block>? BlockContainer;
    protected int Value;
    public int PosX;
    public int PosY;
    private bool hasHitDeath;

    public Block(DynamicShape shape, IBaseImage image, int health, int value) : base(shape, image) {
        this.StartHealth = health;
        this.Health = StartHealth;
        this.Value = value;
    }

    public virtual void Hit(int damage) {
        Health -= damage;
        if (Health <= 0 && !hasHitDeath) {
            hasHitDeath = true;
            BlockDeath();
            this.DeleteEntity();
        }
    }

    public void BlockDeath() {
        var e = new BlockDeathEvent(Shape.Position, 1);
        BreakoutBus.Instance.RegisterEvent(e);
    }

    public virtual void Update() {

    }
}