using Godot;
using System;

public partial class UI : Node
{
    public override void _Ready()
    {
        GetNode<Sprite2D>("./background").Modulate = new Color(0, 0, 0, 0.3f);
        GetNode<Button>("./StartBtn").Pressed += () =>
        {
            Manager.instance.Start();
            if (Manager.instance.isStart)
            {
                GetNode<Button>("./StartBtn").Text = "Pause";
            }
            else
            {
                GetNode<Button>("./StartBtn").Text = "Play";
            }
        };
        GetNode<Button>("./StartBtn").Text = "Play";
        GetNode<Button>("./ResizeBtn").Pressed += () =>
        {
            Manager.instance._Ready();
        };
        var interval = GetNode<TextEdit>("./interval");
        interval.Text = Manager.instance.interval.ToString();
        interval.TextChanged += () =>
        {
            if (interval.Text.Length > 0)
            {
                try
                {
                    float value = float.Parse(interval.Text);
                    Manager.instance.interval = value;
                }
                catch
                {
                    interval.Text = Manager.instance.interval.ToString();
                }
            }
        };

        var size = GetNode<TextEdit>("./size");
        size.Text = Manager.instance.size.ToString();
        size.TextChanged += () =>
        {
            if (size.Text.Length > 0)
            {
                try
                {
                    float value = float.Parse(size.Text);
                    Manager.instance.size = value;
                }
                catch
                {
                    size.Text = Manager.instance.size.ToString();
                }
            }
        };

        var row = GetNode<TextEdit>("./row");
        row.Text = Manager.instance.row.ToString();
        row.TextChanged += () =>
        {
            if (row.Text.Length > 0)
            {
                try
                {
                    int value = int.Parse(row.Text);
                    Manager.instance.row = value;
                }
                catch
                {
                    row.Text = Manager.instance.row.ToString();
                }
            }
        };

        var column = GetNode<TextEdit>("./column");
        column.Text = Manager.instance.column.ToString();
        column.TextChanged += () =>
        {
            if (column.Text.Length > 0)
            {
                try
                {
                    int value = int.Parse(column.Text);
                    Manager.instance.column = value;
                }
                catch
                {
                    column.Text = Manager.instance.column.ToString();
                }
            }
        };
    }

}
