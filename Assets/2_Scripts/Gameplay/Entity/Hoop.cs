using UnityEngine;

public class Hoop : MonoBehaviour
{
    [SerializeField] private Net net;

    public Net Net
    {
        get { return net; }
    }
}