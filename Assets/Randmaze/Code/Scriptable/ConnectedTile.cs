using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using TNS = ConnectedTile.TileNeighbourState;

/// <summary>
/// Represents a tile that can seamlessly connect to other identical tiles
/// </summary
[CreateAssetMenu(menuName = "ConnectedTile")]
public class ConnectedTile : ScriptableObject
{

    //The different sprite animations
    [SerializeField]
    public SpriteAnimation 
        TopLeft,
        Top, 
        TopRight,

        Left, 
        Middle, 
        Right, 

        BottomLeft,
        Bottom, 
        BottomRight,

        Single, 
        Vertical, 
        TopVertical, 
        BottomVertical, 
        Horizontal, 
        LeftHorizontal, 
        RightHorizontal;

    /// <summary>
    /// Gets the animation to use for a tile that is adjacent to the identical tiles in the specified directions
    /// </summary>
    /// <param name="tileStates"></param>
    /// <returns></returns>
    public SpriteAnimation GetConnectedAnimation(TileNeighbourState tileStates)
    {
        switch (tileStates)
        {
            case TNS.Below | TNS.Right: return TopLeft;
            case TNS.Below | TNS.Left: return TopRight;
            case TNS.Below | TNS.Left | TNS.Right: return Top;
            case TNS.Above | TNS.Below | TNS.Right: return Left;
            case TNS.Above | TNS.Below | TNS.Left: return Right;
            case TNS.Above | TNS.Below | TNS.Left | TNS.Right: return Middle;
            case TNS.Above | TNS.Right: return BottomLeft;
            case TNS.Above | TNS.Left: return BottomRight;
            case TNS.Above | TNS.Left | TNS.Right: return Bottom;

            case TNS.Right: return LeftHorizontal;
            case TNS.Left | TNS.Right: return Horizontal;
            case TNS.Left: return RightHorizontal;

            case TNS.Below: return TopVertical;
            case TNS.Below | TNS.Above: return Vertical;
            case TNS.Above: return BottomVertical;

            case TNS.None: return Single;

            default: throw new ArgumentException();

        }
    }

    /// <summary>
    /// Represents any combination of neighbour tiles
    /// </summary>
    [Flags]
    public enum TileNeighbourState
    {
        /// <summary>
        /// No neighbours
        /// </summary>
        None = 0,

        /// <summary>
        /// There is a neighbour above this tile
        /// </summary>
        Above = 1,

        /// <summary>
        /// There is a neighbour to the right of this tile
        /// </summary>
        Right = 2,

        /// <summary>
        /// There is a neighbour below this tile
        /// </summary>
        Below = 4,

        /// <summary>
        /// There is a neighbour to the left of this tile
        /// </summary>
        Left = 8
    }
}
