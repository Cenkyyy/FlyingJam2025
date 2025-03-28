using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Letter : MonoBehaviour
{
    public string GetLetter()
    {
        return FindObjectOfType<Letter>().GetComponent<TextMeshPro>().text;
    }
}
