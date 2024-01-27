using Gotchi.Gotchis.DTOs;
using static Gotchi.Gotchis.DTOs.GotchiDTO;
using System.Drawing;

namespace GotchiWeb.Areas.Users.Pages.Gotchis;

public class GotchiRender
{
    public int X;
    public int Y;
    public Direction Dir;
    public int Moves;
    public int Speed = 20;
    public int Width = 64;
    public int Height = 64;
    public int TickCounter = 0;
}

public interface IGotchiRenderObjState
{
    public IGotchiRenderObjState? Move(GotchiRender renderObj, GotchiDTO gotchi, Rectangle rect);
    public Point GetImage(GotchiRender renderObj, GotchiDTO gotchi);
}


public static class PathPicker
{
    public static IGotchiRenderObjState? Create()
    {
        Random random = new Random();
        int randomNumber = random.Next(0, 2);

        if (randomNumber == 0)
        {
            return new IdlePath(firstCall: true);
        }
        else 
        {
            return new LeftRightPath(firstCall: true);
        }
    }
}

public class IdlePath : PathBase, IGotchiRenderObjState
{
    private readonly bool _firstCall;
    public IdlePath(bool firstCall)
    {
        _firstCall = firstCall;
    }
    public IGotchiRenderObjState? Move(GotchiRender renderObj, GotchiDTO gotchi, Rectangle rect)
    {
        if (_firstCall) 
        {
            renderObj.Dir = PickDir();
            renderObj.Moves = PickMoves();
        }

        if (gotchi.State == GotchiStateDTO.Dead)
            return new DeadPath();

        if (renderObj.Moves == 0)
            return null;

        renderObj.Moves--;
        renderObj.TickCounter++;
        return new IdlePath(firstCall: false);
    }

    public Point GetImage(GotchiRender renderObj, GotchiDTO gotchi)
    {
        var x = renderObj.TickCounter % 5;
        if (renderObj.Dir == Direction.Left)
        {
            return TranslateXY(x, 3, renderObj);
        }
        else
        {
            return TranslateXY(x, 2, renderObj);
        }
    }
}

public class LeftRightPath : PathBase, IGotchiRenderObjState
{
    private readonly bool _firstCall;
    public LeftRightPath(bool firstCall) 
    {
        _firstCall = firstCall;
    }

    public IGotchiRenderObjState? Move(GotchiRender renderObj, GotchiDTO gotchi, Rectangle rect)
    {
        if (_firstCall)
        {
            renderObj.Dir = PickDir();
            renderObj.Moves = PickMoves();
        }

        if (gotchi.State == GotchiStateDTO.Dead)
            return new DeadPath();

        if (renderObj.Moves == 0)
            return null;

        renderObj.Moves --;
        renderObj.TickCounter++;

        if (renderObj.Dir == Direction.Left)
        {
            if (CanMoveLeft(renderObj, rect))
                renderObj.X -= renderObj.Speed;

            return new LeftRightPath(firstCall: false);
        }
        else
        {
            if (CanMoveRight(renderObj, rect))
                renderObj.X += renderObj.Speed;

            return new LeftRightPath(firstCall: false);
        }

    }

    public Point GetImage(GotchiRender renderObj, GotchiDTO gotchi)
    {
        var x = renderObj.TickCounter % 5;
        if (renderObj.Dir == Direction.Left)
        {
            return TranslateXY(x, 1, renderObj);
        }
        else 
        {
            return TranslateXY(x, 0, renderObj);
        }
    }
}

public class DeadPath : PathBase, IGotchiRenderObjState
{

    public IGotchiRenderObjState? Move(GotchiRender renderObj, GotchiDTO gotchi, Rectangle rect)
    {
        renderObj.TickCounter++;
        return new DeadPath();
    }

    public Point GetImage(GotchiRender renderObj, GotchiDTO gotchi)
    {
        return TranslateXY(0, 5, renderObj);
    }
}

public enum Direction : int
{
    Left = 0,
    Right = 1
}

public abstract class PathBase
{
    protected static readonly int DirNo = 2;
    protected static readonly int BaseMoves = 3;
    protected static readonly int AddedMovesMax = 3;

    protected static Direction PickDir()
    {
        Random random = new Random();
        var r = random.Next(0, DirNo);

        if (Enum.IsDefined(typeof(Direction), r))
        {
            return (Direction)Enum.ToObject(typeof(Direction), r);
        }
        else
        {
            throw new Exception("random int r is not in range");
        }

    }

    protected static int PickMoves()
    {
        Random random = new Random();
        return BaseMoves + random.Next(0, AddedMovesMax);
    }

    protected static bool CanMoveLeft(GotchiRender renderObj, Rectangle rect)
    {
        var newXPos = renderObj.X - renderObj.Speed;

        if (newXPos > rect.Left)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected static bool CanMoveRight(GotchiRender renderObj, Rectangle rect)
    {
        var newXPos = renderObj.X + renderObj.Speed;

        if ((newXPos + renderObj.Width) < rect.Right)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected static Point TranslateXY(int cellX, int cellY, GotchiRender renderObj) 
    {
        var x = cellX * renderObj.Width;
        var y = cellY * renderObj.Height;
        return new Point(x, y); 
    }

}