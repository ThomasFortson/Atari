namespace Breakout.Blocks;

using System;
using DIKUArcade;
using DIKUArcade.Events;
using DIKUArcade.Input;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Entities;
using System.Numerics;

public class Teleport : Block {

    public Teleport(DynamicShape shape, IBaseImage image) : base(shape, image, 40, 3) {

    }

    public override void Hit(int damage) {
        int newHealth = Health - damage;
        if (newHealth <= 0) {
            Health = 0;
            BlockDeath();
            DeleteEntity();
        } else {
            Health -= damage;
            TeleportFunc();
        }
        if (newHealth <= StartHealth / 2) {
            this.Image = new Image($"Breakout.Assets.Images.{RemoveFileExtension(SpritePath)}-damaged.png");
        }
    }


    private void TeleportFunc() {
        Random random = new Random();
        this.Shape.Position = new Vector2(random.NextSingle(), random.NextSingle() / 2f + 0.5f);
        this.Shape.Move();
    }

    public string RemoveFileExtension(string fileName) {
        int lastIndex = fileName.LastIndexOf('.');
        if (lastIndex == -1) {
            return fileName;
        }
        return fileName.Substring(0, lastIndex);
    }

}