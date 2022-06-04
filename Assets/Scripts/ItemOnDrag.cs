using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemOnDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform originalParent;
    public CreateBag myBag;
    private int currentItemID;//当前物品ID

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        currentItemID = originalParent.GetComponent<Slot>().slotID;
        transform.SetParent(transform.parent.parent);
        transform.position = eventData.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;//射线阻挡关闭
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        //Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);//输出鼠标当前位置下到第一个碰到到物体名字
    }

    public void OnEndDrag(PointerEventData eventData) {

        
        if(eventData.pointerCurrentRaycast.gameObject==null) {
            transform.SetParent(originalParent.transform);
            transform.position = originalParent.transform.position;
        }else if (eventData.pointerCurrentRaycast.gameObject.name == "Item Image")//判断下面物体名字是：Item Image 那么互换位置
        {
            transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent);
            transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;
            //itemList的物品存储位置改变
            var temp = myBag.items[currentItemID];
            myBag.items[currentItemID] = myBag.items[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID];
            myBag.items[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID] = temp;

            eventData.pointerCurrentRaycast.gameObject.transform.parent.position = originalParent.position;
            eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(originalParent);     
        }
        else if (eventData.pointerCurrentRaycast.gameObject.name == "slot(Clone)") {
            //否则直接挂在检测到到Slot下面
            transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
            transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;

            //itemList的物品存储位置改变
            myBag.items[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID] = myBag.items[currentItemID];
            myBag.items[currentItemID] = null;
        
        }
        else {
            transform.SetParent(originalParent.transform);
            transform.position = originalParent.transform.position;
        }
        

        


        GetComponent<CanvasGroup>().blocksRaycasts = true;//射线阻挡开启，不然无法再次选中移动的物品
    }


}
