namespace Breakout.Lives;

using System.ComponentModel.DataAnnotations;
using System.Net.WebSockets;
using System.Numerics;
using DIKUArcade.Entities;
using DIKUArcade.Font;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;

public class LifeContainer {

    private int lives;
    int startLives = 3;
    Vector2 extent = new Vector2(0.05f, 0.05f);

    Vector2 padding = new Vector2(0.025f, 0.025f);

    private EntityContainer<Entity> hearts = new EntityContainer<Entity>();

    public LifeContainer(int lives) {

        this.lives = lives;
        startLives = lives;
        UpdateLives();

    }
    public int Lives {
        get {
            return lives;
        }
        set {
            if (value <= 0) {
                lives = 0;
            } else {
                lives = value;
            }
            UpdateLives();
        }
    }

    public void Update() {

    }

    private void UpdateLives() {
        hearts.ClearContainer();
        foreach (int life in Enumerable.Range(0, Math.Max(startLives, Lives))) {
            var newShape = new DynamicShape(new Vector2(life * (extent.X + padding.X) + padding.X, 1 - extent.Y - padding.Y), extent);
            var newImage = life < Lives ? new Image("Breakout.Assets.Images.heart_filled.png") : new Image("Breakout.Assets.Images.heart_empty.png");
            if (life >= startLives) {
                newImage = new Image("Breakout.Assets.Images.heart_yellow.png");
            }
            Entity e = new Entity(newShape, newImage);
            hearts.AddEntity(e);
        }
    }

    public void Render(WindowContext context) {
        hearts.RenderEntities(context);
    }
}