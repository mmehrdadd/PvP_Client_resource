using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tagholder : MonoBehaviour
{
    
}
public class AnimationTags
{
    public const string zoomInAnim = "ZoomIn";
    public const string zoomOutAnim = "ZoomOut";

    public const string shootTrigger = "Shoot";
    public const string aimParameter = "Aim";

    public const string walkParameter = "Walk";
    public const string runParameter = "Run";
    public const string attackTrigger = "Attack";
    public const string deadTrigger = "Death";
    
}
public class Tags
{
    public const string lookRoot = "LookRoot";
    public const string zoomCamera = "FPcamera";
    public const string crossHair = "Crosshair";
    public const string arrowTag = "Arrow";
    public const string axeTag = "Axe";
    public const string playerTag = "Player";
    public const string enemyTag = "Enemy";
}