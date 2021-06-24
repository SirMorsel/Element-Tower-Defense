using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private int baseGameSpeed = 1;
    public static GameManager Instance { get { return _instance; } }

    private void Awake()
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
        print($"Game speed {baseGameSpeed}");
        if (baseGameSpeed > 3)
        {
            baseGameSpeed = 1;
        }
        gameObject.GetComponent<GameUI>().ChangeSpeedButtonText(baseGameSpeed);
    }
}
