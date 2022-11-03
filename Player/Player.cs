using Godot;
using System;

public class Player : KinematicBody2D
{
    [Export]
    public bool DebuggingEnabled;

    [Export]
    public int MaxSpeed = 80;
    [Export]
    public int MaxRunSpeed = 80;

    [Export]
    public int Acceleration = 500;
    [Export]
    public int Friction = 250;

    private int runSpeed;

    private Vector2 velocity;
    private Vector2 inputAxis;

    public override void _PhysicsProcess(float delta) {
        PlayerMovement(delta);
    }

    public override void _Process(float delta) {
        Debugging();
    }

    private void PlayerMovement(float delta) {
        inputAxis = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        inputAxis = inputAxis.Normalized();

        if (Input.IsActionPressed("Run"))
            runSpeed = MaxRunSpeed;
        else
            runSpeed = 0;

        if (inputAxis != Vector2.Zero) {
            velocity = velocity.MoveToward(inputAxis * (MaxSpeed + runSpeed), Acceleration * delta);
        } else {
            velocity = velocity.MoveToward(Vector2.Zero, Friction * delta);
        }

        velocity = MoveAndSlide(velocity);
    }

    private void Debugging() {
        if (!DebuggingEnabled) return;
        GD.Print(velocity);
    }
}
