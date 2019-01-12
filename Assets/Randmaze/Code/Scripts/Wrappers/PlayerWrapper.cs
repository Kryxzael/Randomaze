using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Wrapps the PlayerState class, and displays it on the game board
/// </summary>
[RequireComponent(typeof(SpriteAnimator))]
public class PlayerWrapper : MonoBehaviour
{
    /// <summary>
    /// The distance a smooth move will move the player per frame
    /// </summary>
    private const float SMOOTH_MOVE_SPEED = 1 / 3f;

    /// <summary>
    /// The PlayerState instance that this wrapper wraps
    /// </summary>
    public PlayerState Player;

    /// <summary>
    /// The name of the sprite resource the player will use
    /// </summary>
    public string PlayerAnimationName;


    /// <summary>
    /// Is the player currently moving?
    /// </summary>
    public bool Moving { get; private set; }

    private void Start()
    {
        GameState.Events.OnPlayerKilled.AddListener(OnKilled);
        GameState.Events.OnPlayerMoved.AddListener((p, v3) => StartCoroutine("CoOnMoved", v3));
        GameState.Events.OnPlayerStateChanged.AddListener(UpdatePlayer);

        //Sets the animations
        GetComponent<SpriteAnimator>().Animation = GameState.Settings.ResourceSet.GetResource<SpriteAnimation>(PlayerAnimationName);
        transform.Find("FXLayerEl").GetComponent<SpriteAnimator>().Animation =
            GameState.Settings.ResourceSet.GetResource<SpriteAnimation>("FXCharge");

        UpdatePlayer(Player);
    }

    private void UpdatePlayer(PlayerState pl)
    {
        if (!Player.Alive)
        {
            return;
        }

        transform.Find("FXLayerEl").GetComponent<SpriteRenderer>().color = Player.Electric ? Color.white : new Color();

        switch (Player.Fire)
        {
            case 3:
                transform.Find("FXLayerF").GetComponent<SpriteAnimator>().Animation =
                    GameState.Settings.ResourceSet.GetResource<SpriteAnimation>("FXFire1");
                break;
            case 2:
                transform.Find("FXLayerF").GetComponent<SpriteAnimator>().Animation =
                    GameState.Settings.ResourceSet.GetResource<SpriteAnimation>("FXFire2");
                break;
            case 1:
                transform.Find("FXLayerF").GetComponent<SpriteAnimator>().Animation =
                    GameState.Settings.ResourceSet.GetResource<SpriteAnimation>("FXFire3");
                break;
            default:
                transform.Find("FXLayerF").GetComponent<SpriteAnimator>().Animation = 
                    null;
                transform.Find("FXLayerF").GetComponent<SpriteRenderer>().sprite = null;
                break;
        }
    }

    private void OnKilled(PlayerState pl, CauseOfDeath obj)
    {
        GetComponent<SpriteAnimator>().Animation = GameState.Settings.ResourceSet.GetResource<SpriteAnimation>("PlayerDead");
    }

    /// <summary>
    /// Handles input
    /// </summary>
    private void Update() //BM Input handing
    {
        //Cancels any form of input if the player is currently moving
        if (Moving)
        {
            return;
        }

        //Was a key pressed
        if (Input.anyKeyDown)
        {
            //Is the level completed or died on
            if (GameState.GameplayStatus == GameplayStaus.ExitingLevel)
            {
                GameState.NextLevel();
            }
            //The player is not alive, the state will be reset
            else if (!Player.Alive)
            {
                GameState.RestartLevel();
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                {
                    Player.Move(Direction.Down);
                }

                else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                {
                    Player.Move(Direction.Up);
                }

                else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                {
                    Player.Move(Direction.Left);
                }

                else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                {
                    Player.Move(Direction.Right);
                }

                UIManager.HideTitles();
            }
        }


    }

    /// <summary>
    /// Coroutine: Called when the player moves
    /// </summary>
    /// <param name="target">Place to move player</param>
    /// <returns></returns>
    private IEnumerator CoOnMoved(Vector2Int lastPos)
    {
        Vector2Int target = Player.Position;

        //Fixes endless ice glitch
        if (target == lastPos)
        {
            yield break;
        }

        //Rotate the sprite and fix the rotations of the other layers
        transform.SetEuler2D(Player.Rotation.ToDegree());
        transform.Find("FXLayerF").transform.rotation = Quaternion.identity;
        transform.Find("FXLayerEl").transform.rotation = Quaternion.identity;

        /*
         * MOVE SPRITE
         * Lerps the sprite position of the player between its current position and the target position
         */

        Vector2 currentPosition = transform.position;
        Vector2 nextPosition = new Vector2(target.x, target.y);

        Moving = true;
        for (float i = 0; i <= 1; i += SMOOTH_MOVE_SPEED)
        {
            transform.position = Vector2.Lerp(currentPosition, nextPosition, i);
            yield return new WaitForFixedUpdate();
        }

        //Snaps position for error correction
        transform.position = nextPosition;
        //End sprite move

        //Apply tile step event
        GameState.CurrentLevel.Map.GetTile(Player.Position.x, Player.Position.y).OnStep(Player);

        //Tick fire counter
        Player.TickFire();
        Moving = false;
    }
}
