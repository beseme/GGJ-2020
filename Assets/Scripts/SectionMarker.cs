using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionMarker : MonoBehaviour
{
    [SerializeField] private int _id = 0;
    public int ID => _id;  // 1 is left, 2 right, 3 top, 4 bottom
    [SerializeField] private CameraController _camera = null;
    [SerializeField] private Transform _camLoc = null;
    [SerializeField] private Collider _confinerCollider = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ThrowawayMovement>())
        {
            _camera.Scroll(_camLoc, _confinerCollider);
        }
    }
}
