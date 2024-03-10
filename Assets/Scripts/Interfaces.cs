using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eTurnType
{
    AWake = 0,
    PlayerTurn = 1,
    PlayerMoveEnd = 2,
    GameOver = 3
}
public interface ITurnSystem 
{

    void TurnAwake();

    void StartPlayerTurn();

    void EndPlayerMove();
}

// 플레이어의 피격 이벤트에 반응하는 객체 
public interface IPlayerHit
{
    void HitPlayer();
}

