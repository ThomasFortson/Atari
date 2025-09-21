namespace Breakout.Blocks;

using System;
using DIKUArcade;
using DIKUArcade.Events;
using DIKUArcade.Input;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Entities;

public class Unbreakable : Block {

    public Unbreakable (DynamicShape shape, IBaseImage image) : base(shape, image, 1, 1) {
    }

    public override void Hit(int damage) {
    }


}