using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using MBT;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class VisionScene : MonoBehaviour
{
    [SerializeField, Min(0f)] private float distanse = 10f;
    [SerializeField, Range(0, 90f)] private float coneHalfAnge = 30f;
    [TagField]
    [SerializeField] private string[] tags = { "Player" };
    [SerializeField] private LayerMask layer = 1;
    private float ConeHalfAngle => Mathf.Cos(coneHalfAnge);
    public IEnumerable<Transform> GetTriggers()
    {
        var position = transform.position;
        var right = transform.right;
        var resalts = Physics2D.CircleCastAll(position, distanse, right, 0f, layer);
        return from result in resalts
               where tags.Contains(result.transform.tag)
               let location = result.transform.position
               let direction = location - position
               let dot = Vector3.Dot(direction.normalized, right)
               where dot >= ConeHalfAngle
               let hit = Physics2D.Linecast(position, location, layer)
               where hit.transform == hit.transform
               select result.transform;
    }

}
