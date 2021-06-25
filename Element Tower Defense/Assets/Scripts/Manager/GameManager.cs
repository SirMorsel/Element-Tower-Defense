using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    // Basic Game informations
    private int baseGameSpeed = 1;

    public static GameManager Instance { get { return _instance; } }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public int GetGameSpeed()
    {
        return baseGameSpeed;
    }

    public void ChangeGameSpeed()
    {
        baseGameSpeed++;
        if (baseGameSpeed > 3)
        {
            baseGameSpeed = 1;
        }
        gameObject.GetComponent<GameUI>().ChangeSpeedButtonText(baseGameSpeed);
    }
}
