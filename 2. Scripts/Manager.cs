using Godot;
using System;
using System.Collections.Generic;

public partial class Manager : Node2D
{
    public int offset = 11;
    public int row = 50;
    public int column = 50;
    public float scaleValue = 1.1f;
    Node2D squareParent;
    public float size = 1;
    public static Manager instance;
    bool middleInput = false;
    Vector2 clickInitPos;
    public bool isStart = false;
    public Square[,] squares;
    public float interval = 0.1f;
    float count = 0;
    public bool mouseInput = false;
    public override void _EnterTree()
    {
        instance = this;
    }
    public override void _Ready()
    {
        squares = new Square[row, column];
        PackedScene squarePrefab = GD.Load<PackedScene>("res://3. Prefabs/square.tscn");
        squareParent = GetNode<Node2D>("./squareParent");
        var childs = squareParent.GetChildren();
        foreach(var item in childs){
            squareParent.RemoveChild(item);
            item.QueueFree();
        }
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                var square = squarePrefab.Instantiate();
                squareParent.AddChild(square);
                var sq = square.GetNode<Square>(".");
                squares[i, j] = sq;
                sq.SetInit(i, j, offset);
            }
        }
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                squares[i, j].SetNeightBoard(i, j);
            }
        }

        GD.Print(GetChildCount());
        float middleX = (row + 1) / 2.0f;
        float middleY = (column + 1) / 2.0f;
        var cameraNode = GetNode<Node2D>("./Camera2D");
        cameraNode.Position = new Vector2(middleX * offset, middleY * offset);
    }

    void SetNum()
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                squares[i, j].SetNum();
            }
        }
    }
    void SetChange()
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                squares[i, j].ChangeActive();
            }
        }
    }
    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        if (@event is InputEventMouseButton mouseBtn)
        {
            if (mouseBtn.ButtonIndex == MouseButton.WheelUp)
            {
                squareParent.Scale *= scaleValue;
            }
            if (mouseBtn.ButtonIndex == MouseButton.WheelDown)
            {
                squareParent.Scale /= scaleValue;
            }

            if (mouseBtn.ButtonIndex == MouseButton.Middle)
            {
                if (mouseBtn.Pressed)
                {
                    clickInitPos = squareParent.Position - GetLocalMousePosition();
                    middleInput = true;
                }
                else
                {
                    middleInput = false;
                }
            }
            if (mouseBtn.ButtonIndex == MouseButton.Left)
            {
                if (mouseBtn.Pressed)
                {
                    mouseInput = true;
                }
                else
                {
                    mouseInput = false;
                }
            }
        }
    }
    public override void _Process(double delta)
    {
        if (middleInput)
        {
            squareParent.Position = GetLocalMousePosition() + clickInitPos;
        }
        if (isStart)
        {
            count += (float)delta;
            if (count >= interval)
            {
                count = 0;
                SetNum();
                SetChange();
            }
        }
    }
    public void Start()
    {
        isStart = !isStart;
    }
}
