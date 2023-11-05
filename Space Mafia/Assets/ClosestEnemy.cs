
using UnityEngine;
using System.Collections.Generic;

public class ClosestEnemy : MonoBehaviour
{

    public readonly static HashSet<ClosestEnemy> Pool = new HashSet<ClosestEnemy>();

    private void OnEnable()
    {
        ClosestEnemy.Pool.Add(this);
    }

    private void OnDisable()
    {
        ClosestEnemy.Pool.Remove(this);
    }



    public static ClosestEnemy FindClosestEnemy(Vector3 pos)
    {
        ClosestEnemy result = null;
        float dist = float.PositiveInfinity;
        var e = ClosestEnemy.Pool.GetEnumerator();
        while (e.MoveNext())
        {
            float d = (e.Current.transform.position - pos).sqrMagnitude;
            if (d < dist)
            {
                result = e.Current;
                dist = d;
            }
        }
        return result;
    }

}
