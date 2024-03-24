using Godot;
using System;
using System.Collections.Generic;
public partial class Square : Sprite2D
{
    public bool isActive = false;
    Color activeColor;
    Color inActiveColor;
    List<Square> neightboard = new List<Square>();
    int neightboardNum;
    public void SetInit(int row, int column, int offset)
    {
        activeColor = new Color(0, 0, 0, 1);
        inActiveColor = new Color(1, 1, 1, 1);
        this.Position = new Vector2(row * offset, column * offset);
        Change(false);
    }
    public void SetNeightBoard(int row, int column)
    {
        try
        {
            neightboard.Add(Manager.instance.squares[row - 1, column - 1]);

        }
        catch { }
        try
        {
            neightboard.Add(Manager.instance.squares[row - 1, column]);

        }
        catch { }
        try
        {
            neightboard.Add(Manager.instance.squares[row - 1, column + 1]);

        }
        catch { }
        try
        {
            neightboard.Add(Manager.instance.squares[row + 1, column - 1]);

        }
        catch { }
        try
        {
            neightboard.Add(Manager.instance.squares[row + 1, column]);

        }
        catch { }
        try
        {
            neightboard.Add(Manager.instance.squares[row + 1, column + 1]);

        }
        catch { }
        try
        {
            neightboard.Add(Manager.instance.squares[row, column - 1]);

        }
        catch { }
        try
        {

            neightboard.Add(Manager.instance.squares[row, column + 1]);
        }
        catch { }

    }
    public void Change(bool active)
    {
        isActive = active;

        Modulate = (isActive ? activeColor : inActiveColor);
    }
    public override void _Process(double delta)
    {
        if (Manager.instance.mouseInput)
        {
            var mousePos = GetParent<Node2D>().GetLocalMousePosition();
            if ((mousePos - Position).Length() < Scale[0] / 2 * Manager.instance.size)
            {
                Change(true);
            }
        }
    }
    public void SetNum()
    {
        int a = 0;
        neightboard.ForEach((x) =>
        {
            if (x.isActive)
            {
                a++;
            }
        });
        neightboardNum = a;
    }
    public void ChangeActive()
    {
        if (neightboardNum == 3)
        {
            Change(true);
        }
        else if(neightboardNum != 2)
        {
            Change(false);
        }
    }
}
