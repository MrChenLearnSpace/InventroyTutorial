using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Bag",menuName = "Inventory/New Bag")]
public class CreateBag : ScriptableObject
{
    // Start is called before the first frame update
    public List<CreateItem> items = new List<CreateItem>();
}
