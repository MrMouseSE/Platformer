using UnityEngine;

public class Shape : MonoBehaviour
{
    public Color colour = Color.white;
    [Range(0,1)]
    public float blendStrength;

    public Vector3 Position {
        get {
            return transform.position;
        }
    }

    public Vector3 Scale {
        get {
            return transform.localScale;
        }
    }
}