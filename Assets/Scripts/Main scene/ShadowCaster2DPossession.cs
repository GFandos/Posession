using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ShadowCaster2DPossession : MonoBehaviour
{

    private CompositeCollider2D tilemapCollider;

    public void Start()
    {
        tilemapCollider = GetComponent<CompositeCollider2D>();
        GameObject shadowCasterContainer = GameObject.Find("shadow_casters");
        for (int i = 0; i < tilemapCollider.pathCount; i++)
        {
            Vector2[] pathVertices = new Vector2[tilemapCollider.GetPathPointCount(i)];
            tilemapCollider.GetPath(i, pathVertices);
            GameObject shadowCaster = new GameObject("shadow_caster_" + i);
            PolygonCollider2D shadowPolygon = (PolygonCollider2D)shadowCaster.AddComponent(typeof(PolygonCollider2D));
            shadowCaster.transform.parent = shadowCasterContainer.transform;
            shadowPolygon.points = pathVertices;
            shadowPolygon.enabled = false;
            UnityEngine.Experimental.Rendering.Universal.ShadowCaster2D shadowCasterComponent = shadowCaster.AddComponent<UnityEngine.Experimental.Rendering.Universal.ShadowCaster2D>();
            shadowCasterComponent.selfShadows = true;
        }
    }

}
