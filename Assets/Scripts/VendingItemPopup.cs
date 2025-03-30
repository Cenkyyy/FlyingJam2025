using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VendingItemPopup : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject popupBox;

    bool follow = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        popupBox.SetActive(true);
        follow = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        popupBox.SetActive(false);
        follow = false;
    }

    private void Update()
    {
        if (follow)
        {
            popupBox.transform.position = Input.mousePosition + new Vector3(0, 170, 0);
        }
    }
}
