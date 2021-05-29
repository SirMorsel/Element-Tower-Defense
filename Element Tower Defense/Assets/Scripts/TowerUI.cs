using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUI : MonoBehaviour
{

    private Foundation target;
    private GameObject ui;

    // Start is called before the first frame update
    void Start()
    {
        ui = this.transform.GetChild(0).gameObject;
        print($"UI: {ui}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTarget(Foundation target)
    {
        this.target = target;
        transform.position = target.transform.position;
        ui.SetActive(true);
    }

    public void HideUiElement()
    {
        ui.SetActive(false);
    }

}
