namespace Breakout.Blocks;

using System;
using DIKUArcade;
using DIKUArcade.Events;
using DIKUArcade.Input;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Entities;

public class Factory {
    public Factory() {
    }

    private readonly Dictionary<BlockType, Func<DynamicShape, Image, Block>> blockFactories = new() {
        { BlockType.Normal, (shape, image) => new Normal(shape, image) },
        { BlockType.Hardened, (shape, image) => new Hardened(shape, image) },
        { BlockType.Unbreakable, (shape, image) => new Unbreakable(shape, image) },
        { BlockType.PowerUp, (shape, image) => new PowerUp(shape, image) },
        { BlockType.Teleport, (shape, image) => new Teleport(shape, image) }
    };

    public Block CreateBlock((string, BlockType) a, DynamicShape shape) {
        var image = new Image($"Breakout.Assets.Images.{a.Item1}");

        // argument is by reference rather than by value
        if (blockFactories.TryGetValue(a.Item2, out var block)) {
            return block(shape, image);
        }

        return new Normal(shape, image);
    }

}