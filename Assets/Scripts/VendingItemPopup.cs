using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VendingItemPopup : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject popupBox;

    public void OnPointerEnter(PointerEventData eventData)
    {
        popupBox.SetActive(true);
        popupBox.transform.position = Input.mousePosition; // Position near cursor
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        popupBox.SetActive(false);
    }
}
