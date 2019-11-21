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
      Vector3 position = player.transform.position;
      transform.position = new Vector3(position.x, position.y, position.z - 11);
    }
  }
}
