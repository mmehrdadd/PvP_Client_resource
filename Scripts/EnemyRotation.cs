using RiptideNetworking;
using UnityEngine;

public class EnemyRotation : MonoBehaviour
{
    [SerializeField] public  Transform lookRoot, playerRoot;

    public void setRotation(Quaternion lookRoot, Quaternion playerRoot)
    {
        this.lookRoot.localRotation = lookRoot;
        this.playerRoot.localRotation = playerRoot;
    }

    
}
