using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserMessage : MonoBehaviour
{
    private Text userMessageTextfield;
    private float userMessageResetTime = 5f;

    // Start is called before the first frame update
    void Awake()
    {
        userMessageTextfield = gameObject.GetComponent<Text>();
    }

    public void SetNewUserMessage(string message)
    {
        userMessageTextfield.text = message;
        Invoke("ResetUserMessage", userMessageResetTime);
    }

    private void ResetUserMessage()
    {
        userMessageTextfield.text = "";
    }
}
