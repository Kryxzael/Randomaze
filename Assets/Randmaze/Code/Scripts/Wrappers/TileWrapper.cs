using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        GetComponent<SpriteAnimator>().Animation = GameState.Settings.ResourceSet.GetResource<SpriteAnimation>(
            GameState.CurrentLevel.Map.GetTile(GridPosition.x, GridPosition.y).GetSpriteAnimationName());
    }
}
