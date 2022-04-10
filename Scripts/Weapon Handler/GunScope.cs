using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScope : MonoBehaviour
{
    [SerializeField]
    private Camera fpCamera, mainCamera;
    private GameObject sniperScope;
    private float cameraFov = 75f;
    public LayerMask sniperMask, weaponMask;

    private void Awake()
    {
        sniperScope = GameObject.FindWithTag("SniperScope").transform.GetChild(0).gameObject;
    }
    IEnumerator OnScoped()
    {
        yield return new WaitForSeconds(0.0f);
        sniperScope.SetActive(true);
        fpCamera.cullingMask = sniperMask;
        mainCamera.fieldOfView = cameraFov / 3f;
    }
    void OnUnscoped()
    {

        sniperScope.SetActive(false);
        fpCamera.cullingMask |= 1 << LayerMask.NameToLayer("Lightign");
        fpCamera.cullingMask = weaponMask;
        mainCamera.fieldOfView = cameraFov;
    }
}
