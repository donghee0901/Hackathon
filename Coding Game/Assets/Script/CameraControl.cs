using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script
{
  public class CameraControl : MonoBehaviour
  {
    private GameObject player;
    
    // Start is called before the first frame update
    private void Start()
    {
      player = GameObject.FindWithTag("player");
    }

    private void LateUpdate()
    {
      gameObject.transform.position = player.transform.position;
    }
  }
}
