using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TileNeighbourState = ConnectedTile.TileNeighbourState;


/// <summary>
/// Wrapps the Tile class, and displays it on the game board
/// </summary>
[RequireComponent(typeof(SpriteAnimator))]
public class TileWrapper : MonoBehaviour
{
    public Vector2Int GridPosition;

    public void UpdateTile()
    {
        //Sets the animation
        GetComponent<SpriteAnimator>().Animation = GetAnimation();
    }

    /// <summary>
    /// Gets the SpriteAnimation to apply to this tile
    /// </summary>
    private SpriteAnimation GetAnimation()
    {
        //Gets the current tile
        Tile t = GameState.CurrentLevel.Map.GetTile(GridPosition.x, GridPosition.y);


        //The tile uses conneced textures
        if (t.IsDynamicallyConnectedTile())
        {
            TileNeighbourState neighbours = TileNeighbourState.None;

            Tile neighbour = null;

            //Tile matches to the left
            neighbour = GameState.CurrentLevel.Map.GetTile(GridPosition.x - 1, GridPosition.y);
            if (neighbour == t || neighbour == null)
            {
                neighbours |= TileNeighbourState.Left;
            }

            //Tile matches to the right
            neighbour = GameState.CurrentLevel.Map.GetTile(GridPosition.x + 1, GridPosition.y);
            if (neighbour == t || neighbour == null)
            {
                neighbours |= TileNeighbourState.Right;
            }

            //Tile matches to above
            neighbour = GameState.CurrentLevel.Map.GetTile(GridPosition.x, GridPosition.y + 1);
            if (neighbour == t || neighbour == null)
            {
                neighbours |= TileNeighbourState.Above;
            }

            //Tile matches to below
            neighbour = GameState.CurrentLevel.Map.GetTile(GridPosition.x, GridPosition.y - 1);
            if (neighbour == t || neighbour == null)
            {
                neighbours |= TileNeighbourState.Below;
            }


            return GameState.Settings.ResourceSet.GetResource<ConnectedTile>(t.GetSpriteAnimationName()).GetConnectedAnimation(neighbours);
        }

        //The tile does not use connected textures
        else
        {
            return GameState.Settings.ResourceSet.GetResource<SpriteAnimation>(t.GetSpriteAnimationName());
        }
    }

    
}
