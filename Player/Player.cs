using Godot;
using System;

public class Player : KinematicBody2D
{
    [Export]
    public bool DebuggingEnabled;

    [Export]
    public NodePath AnimatedSpritePath;
    private AnimatedSprite animatedSprite;

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

    public override void _Ready()
    {
        animatedSprite = GetNode<AnimatedSprite>(AnimatedSpritePath);
    }

    public override void _PhysicsProcess(float delta) {
        PlayerMovement(delta);
    }

    public override void _Process(float delta) {
        Debugging();
    }

    private void PlayerMovement(float delta) {
        inputAxis = Input.GetVector("MoveLeft", "MoveRight", "MoveUp", "MoveDown");
        inputAxis = inputAxis.Normalized();

        if (Input.IsActionPressed("Run"))
            runSpeed = MaxRunSpeed;
        else
            runSpeed = 0;

        if (inputAxis != Vector2.Zero) {
            velocity = velocity.MoveToward(inputAxis * (MaxSpeed + runSpeed), Acceleration * delta);
            animatedSprite.Animation = "Walk";
            AnimationRotation();
        } else {
            velocity = velocity.MoveToward(Vector2.Zero, Friction * delta);
            animatedSprite.Animation = "Idle";
        }

        velocity = MoveAndSlide(velocity);
    }

    // Lazy, just did this because of the Sprites we are currently using (There's probably another way to do it with the current sprites,
    // but I don't know what that could be atm)
    private void AnimationRotation()
    {
        if (inputAxis.y < 0)
            animatedSprite.RotationDegrees = -90;
        if (inputAxis.y > 0)
            animatedSprite.RotationDegrees = 90;

        if (inputAxis.x > 0)
            animatedSprite.RotationDegrees = 0;
        if (inputAxis.x < 0)
            animatedSprite.RotationDegrees = -180;
    }

    private void Debugging() {
        if (!DebuggingEnabled) return;
        GD.Print(velocity);
    }
}
