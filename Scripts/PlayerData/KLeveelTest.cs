using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KLeveelTest : MonoBehaviour
{
    public void KNextLevel()
    {
        GameManager.Instance.UpdateGameState(GAMESTATE.VICTORY);
    }
}
