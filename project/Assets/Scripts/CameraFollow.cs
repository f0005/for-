using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//相机跟随玩家
public class CameraFollow : MonoBehaviour
{
    public Transform target;

    private void Update() {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);//z轴不动
    }
}
