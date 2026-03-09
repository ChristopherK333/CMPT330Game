using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{

    public Image[] tabImages; //array of images for the tabs
    public GameObject[] pages; //array of game objects for the pages
    // Start is called before the first frame update
    void Start()
    {
        ActivateTab(0); //activate the first tab when the game starts, remove this if you want it to keep on the same tab
    }

    // Update is called once per frame
    public void ActivateTab(int tabNo)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
            tabImages[i].color = Color.grey;
        }
        pages[tabNo].SetActive(true); //the one we have clicked on will be true
        tabImages[tabNo].color = Color.white; //the one we have clicked on will be white //transparent and the color it is. 
    }

}