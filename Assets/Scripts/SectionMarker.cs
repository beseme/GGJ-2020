using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionMarker : MonoBehaviour
{
    [SerializeField] private int _id = 0;
    public int ID => _id;  // 0 is left, 1 right, 2 top, 3 bottom
    [SerializeField] private int _cornerIndex; // 0 bottomleft; 1 topleft; 2 bottomright; 3 topleft; --> same as colliderbounds min max
    [SerializeField] private CameraController _camera = null;
    [SerializeField] private Transform _camLoc = null;
    [SerializeField] private Collider _confinerCollider = null;
    [SerializeField] private GameObject _neighbour = null;
    [SerializeField] private bool _forward = false;
    [SerializeField] private GameObject _player = null;
    private void Awake()
    { 
        float camOffset = Camera.main.orthographicSize * Camera.main.aspect + .5f;
       if(!_forward)
           camOffset = -camOffset;
       //_camLoc.transform.position = new Vector3(transform.position.x + camOffset, transform.position.y, 0);    
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            FindPosition();
            float playerOffset = 2.5f;
            if (!_forward)
                playerOffset = -playerOffset;
            
            var newLoc = new Vector3(transform.position.x + playerOffset, transform.position.y - (transform.localScale.y/2 - .5f));
            //_player.transform.position = newLoc;
            _player.GetComponent<PushUp>().Push(_id);
            _player.GetComponent<Respawn>().SetCheckpoint(newLoc);
            _camera.Scroll(_camLoc, _confinerCollider);
            _neighbour.SetActive(false);
            StartCoroutine(ReactivateRoutine());
        }
    }

    void FindPosition()
    {
        Vector2[] bounds =
        {
            _confinerCollider.bounds.min,
            new Vector2(_confinerCollider.bounds.min.x, _confinerCollider.bounds.max.x),
            new Vector2(_confinerCollider.bounds.max.x, _confinerCollider.bounds.min.y),
            _confinerCollider.bounds.max
        };
        
        Vector2 camSize = new Vector2(Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize);

        Vector2[] camOffset =
        {
            camSize,
            new Vector2(camSize.x, -camSize.y),
            new Vector2(-camSize.x, camSize.y),
            -camSize
        };
        
        _camLoc.transform.position = bounds[_cornerIndex] + camOffset[_cornerIndex];
    }

    private IEnumerator ReactivateRoutine()
    {
        yield return new WaitForSeconds(1f);
        _player.GetComponent<PushUp>().Finish();
        _neighbour.SetActive(true);
    }
}
