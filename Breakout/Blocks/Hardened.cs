namespace Breakout.Blocks;

using System;
using DIKUArcade;
using DIKUArcade.Input;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Entities;
using Breakout.Score;

public class Hardened : Block {

    public bool NoSprite = false;
    public Hardened(DynamicShape shape, IBaseImage image) : base(shape, image, 40, 2) {
    }
    public override void Hit(int damage) {
        int newHealth = Health - damage;
        if (newHealth <= 0) {
            Health = 0;
            BlockDeath();
            DeleteEntity();
            

        } else {
            Health -= damage;
        }
        if (newHealth <= StartHealth / 2 && !NoSprite) {
            this.Image = new Image($"Breakout.Assets.Images.{RemoveFileExtension(SpritePath)}-damaged.png");
        }
    }

    public string RemoveFileExtension(string fileName) {
        int lastIndex = fileName.LastIndexOf('.');
        if (lastIndex == -1) {
            return fileName;
        }
        return fileName.Substring(0, lastIndex);
    }
}