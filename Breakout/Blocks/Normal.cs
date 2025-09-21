namespace Breakout.Blocks;

using System;
using DIKUArcade;
using DIKUArcade.Events;
using DIKUArcade.Input;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Entities;

public class Normal : Block {
    public Normal(DynamicShape shape, IBaseImage image) : base(shape, image, 20, 1) {
    }
}