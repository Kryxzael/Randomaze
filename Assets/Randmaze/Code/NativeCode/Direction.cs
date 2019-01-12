using UnityEngine;
/// <summary>
/// Represents one of the four basic directions
/// </summary>
public enum Direction
{
    Up,
    Right,
    Down,
    Left
}

/// <summary>
/// Extension Methods
/// </summary>
public static class DirectionUtility
{
    /// <summary>
    /// Converts this direction to a vector with the same orientation
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    public static Vector2Int ToVector(this Direction dir)
    {
        switch (dir)
        {
            case Direction.Up:
                return Vector2Int.up;
            case Direction.Right:
                return Vector2Int.right;
            case Direction.Down:
                return Vector2Int.down;
            case Direction.Left:
                return Vector2Int.left;
            default:
                throw new System.ArgumentOutOfRangeException();
        }
    }    

    /// <summary>
    /// Converts a direction to a degree, 0, 90, 180 or 270 respectivly
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    public static int ToDegree(this Direction dir)
    {
        return 360 - (int)(dir) * 90;
    }

    /// <summary>
    /// Flips a direction
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    public static Direction Reverse(this Direction dir)
    {
        return (Direction)(((int)dir + 2) % 4);
    }
}