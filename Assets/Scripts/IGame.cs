using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGame
{
    void StartGame();
    void AbortGame();
    void PauseGame();
    void UnpauseGame();
}
