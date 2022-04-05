using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreCollision(transform.parent.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
    }
}
