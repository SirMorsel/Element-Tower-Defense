using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town : MonoBehaviour
{
    // Check if the fire particles should be activated
    public void UpdatePlayerTownStatus(int playerHealth)
    {
        if (!transform.GetChild(0).gameObject.activeInHierarchy)
        {
            if (playerHealth <= 5)
            {
                ActivateFireHouseParticle(0);
            }
            else if (playerHealth <= 10)
            {
                ActivateFireHouseParticle(1);
            }
            else if (playerHealth <= 15)
            {
                ActivateFireHouseParticle(2);
            }
        }
    }

    public void ActivateFireHouseParticle(int particleChildID)
    {
        transform.GetChild(particleChildID).gameObject.SetActive(true);
    }
}
