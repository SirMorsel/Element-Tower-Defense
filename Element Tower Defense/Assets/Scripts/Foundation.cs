using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foundation : MonoBehaviour
{
    private Color defaultColor;

    private GameObject tower;
    


    // Start is called before the first frame update
    void Start()
    {
        defaultColor = GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        if (tower != null)
        {
            // Update tower || sell tower
            print("TOWER FOUND");

        }
        else
        {
            if (GameManager.Instance.gameObject.GetComponent<BuildManager>().GetAmountOfTowers() < 
                GameManager.Instance.gameObject.GetComponent<BuildManager>().GetMaxAmountOfTowers())
            {
                // Build tower
                tower = (GameObject)Instantiate(GameManager.Instance.gameObject.GetComponent<BuildManager>().GetTowerToBuild(),
                                                transform.position + new Vector3(0f, 1f, 0f), transform.rotation);
                tower.GetComponent<TowerBehavior>().SetTowerElement(GameManager.Instance.gameObject.GetComponent<BuildManager>().GetTowerToBuildElement());
                GameManager.Instance.gameObject.GetComponent<BuildManager>().IncreasePlayerTowers();
            } else
            {
                print("Max amount of buildable towers reached!!!");
            }

        }
    }

    private void OnMouseEnter()
    {
        GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
    }

    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = defaultColor;
    }

}
