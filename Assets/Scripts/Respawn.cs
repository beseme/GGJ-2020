using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private Vector2 _respawnLoc = Vector2.zero;

    public void SetCheckpoint(Vector2 loc) => _respawnLoc = loc;

    public void Reset() => transform.position = _respawnLoc;
}
