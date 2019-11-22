using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
  private List<GameObject> inputList;
  public GameObject[] references;
  private void Awake()
  {
    inputList = new List<GameObject>();
  }

  public int AddCommand(GetInput.INPUTKEY command)
  {
    Debug.Log(references.Length);
    GameObject image = Instantiate(references[(int) command], transform);
    
    image.GetComponent<RectTransform>().anchoredPosition = new Vector3(25 + 30 * inputList.Count, 25);
    image.GetComponent<RectTransform>().sizeDelta = new Vector2(25, 25);
    
    inputList.Add(image);
    
    return inputList.Count;
  }

  public int RemoveCommand()
  {
    Destroy(inputList[inputList.Count - 1]);
    inputList.RemoveAt(inputList.Count - 1);
    return inputList.Count;
  }
}
