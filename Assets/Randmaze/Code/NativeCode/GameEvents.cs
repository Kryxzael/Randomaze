using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

public class GameEvents
{
    public readonly static UnityEvent OnApplicationStart = new UnityEvent();

    public readonly UnityEvent OnGameStart = new UnityEvent();
    public readonly UnityEvent OnGameEnd = new UnityEvent();

    public readonly UnityEvent OnLevelStart = new UnityEvent();
    public readonly UnityEvent OnLevelEnd = new UnityEvent();

    public readonly PlayerUnityEvent OnPlayerStateChanged = new PlayerUnityEvent();
    public readonly PlayerMovedUnityEvent OnPlayerMoved = new PlayerMovedUnityEvent();
    public readonly PlayerKilledUnityEvent OnPlayerKilled = new PlayerKilledUnityEvent();

    public readonly TileUpdateUnityEvent OnTileUpdated = new TileUpdateUnityEvent();

    public readonly UnityEvent OnKeyCollected = new UnityEvent();
    public readonly UnityEvent OnCoinCollected = new UnityEvent();
}

public class PlayerUnityEvent: UnityEvent<PlayerState>
{

}

public class PlayerKilledUnityEvent : UnityEvent<PlayerState, CauseOfDeath>
{

}

public class PlayerMovedUnityEvent : UnityEvent<PlayerState, Vector2Int>
{

}

public class TileUpdateUnityEvent : UnityEvent<Vector2Int>
{

}