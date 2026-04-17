using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // Start is called before the first frame update
    public int ID;
    public string Name; //will be used later to say which name we have


    public virtual void UseItem()
    {
        Debug.Log("using item");
    }

}
