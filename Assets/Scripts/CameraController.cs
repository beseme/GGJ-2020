using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _vCam = null;

    private Transform _playerLocation = null;
    private CinemachineConfiner _confiner = null;    

    private void Awake()
    {
        _confiner = GetComponent<CinemachineConfiner>();
        _playerLocation = _vCam.m_Follow;
    }

   public void Scroll(Transform target, Collider _confinerCol)
    {
        _confiner.enabled = false;
        _vCam.m_Follow = target;
        StartCoroutine(ResetRoutine(_confinerCol));
    }

    private IEnumerator ResetRoutine(Collider confine)
    {
        yield return new WaitForSeconds(2f);
        _vCam.m_Follow = _playerLocation;
        _confiner.m_BoundingVolume = confine;
        _confiner.enabled = true;
    }
}
