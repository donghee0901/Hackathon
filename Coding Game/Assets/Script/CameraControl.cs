using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script
{
  public class CameraControl : MonoBehaviour
  {
    private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
      player = GameObject.FindWithTag("player");
    }

    // Update is called once per frame
    void Update()
    {
      gameObject.transform.position = player.transform.position;
    }
  }
}
