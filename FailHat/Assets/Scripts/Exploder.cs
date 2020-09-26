using UnityEngine;
public class Exploder : MonoBehaviour
{
    public Color GizmoColor = Color.white;

    public void OnDrawGizmos()
    {
        var exploderPoints = transform.GetComponentsInChildren<Transform>();

        Gizmos.color = GizmoColor;

        foreach (var t in exploderPoints)
        {
            var activePoint = t.position;

            Gizmos.DrawWireSphere(activePoint, 0.3f);
        }
    }

}