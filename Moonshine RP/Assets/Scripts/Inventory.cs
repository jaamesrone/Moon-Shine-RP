using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    [SerializeField]
    public GameObject StatusPage;
    public bool StatusEnabled;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && StatusEnabled) 
        {
            ToggleStatusPage();
        }
    }

    private void ToggleStatusPage()
    {
        if (StatusPage.activeInHierarchy)
        {
            StatusPage.SetActive(false);
        }
        else
        {
            StatusPage.SetActive(true);
        }
        
    }
}
