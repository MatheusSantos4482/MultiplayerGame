using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereArea
{
    public Vector3 position { get; private set; }
    public float radius { get; private set; }

    public SphereArea(Vector3 pos, float r)
    {
        position = pos;
        radius = r;
    }

    public bool CheckRoom(SphereArea sphere)
    {
        float radiusSoma = radius + sphere.radius;
        float distance = Vector2.Distance(position, sphere.position);
        return distance<radiusSoma;
    }
}

public class RoomUI : MonoBehaviour
{
    [HideInInspector] public PlController player;
    [HideInInspector] public Transform panel;
    public bool locked;
    [SerializeField] RectTransform rect;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        DetectOtherRooms();
        if (!locked)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0)) { 
                locked = true; 
            }
            transform.localPosition = panel.InverseTransformPoint(player.hit.point);
        }
    }

    void DetectOtherRooms()
    {
        if (locked)
        {
            SphereArea mouseArea = new SphereArea(player.hit.point, 1);

            SphereArea leftSensor = new SphereArea(transform.position + Vector3.left * rect.sizeDelta.x / 10, 1);

            Debug.Log(leftSensor.CheckRoom(mouseArea));
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + Vector3.left*rect.sizeDelta.x / 10, 0.1f);
    }
}
