using UnityEngine;

// this is a util to get the bounds of the level.
public class GetBounds : MonoBehaviour
{
    [ContextMenu("PrintBoxColliderBounds")]
    public void Test()
    {
        PrintBoxColliderBounds(GetComponent<Collider>());
    }

    public void PrintBoxColliderBounds(Collider Collider)
    {
        if (Collider == null)
        {
            Debug.LogError("Collider is null.");
            return;
        }

        Bounds bounds = Collider.bounds;
        Debug.Log("BoxCollider Bounds: " + bounds.ToString());
    }
}
