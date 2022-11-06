using Godot;
using System;

public class PerformanceUI : Control
{
    [Export]
    public NodePath FPSDisplayPath;
    public Label FPSDisplay;

    public override void _Ready()
    {
        FPSDisplay = GetNode<Label>(FPSDisplayPath);
    }

    public override void _Process(float delta)
    {
        FPSDisplay.Text = "FPS: " + Performance.GetMonitor(Performance.Monitor.TimeFps);
    }
}
