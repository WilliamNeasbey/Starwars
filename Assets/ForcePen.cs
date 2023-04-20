using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcePen : MonoBehaviour
{
    [Header("Pen Properties")]
    public Transform tip;
    public Material drawingMaterial;
    public Material tipMaterial;
    [Range(0.01f, 0.1f)]
    public float penWidth = 0.01f;
    public Color[] penColors;

    [Header("Hands")]
    public OVRInput.Controller inputSource;

    private LineRenderer currentDrawing;
    private int index;
    private int currentColorIndex;
    private List<GameObject> drawings = new List<GameObject>();

    private void Start()
    {
        currentColorIndex = 0;
        tipMaterial.color = penColors[currentColorIndex];
    }

    private void Update()
    {
        bool isRightHandDrawing = OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger);
        if (isRightHandDrawing)
        {
            Draw();
        }
        else if (currentDrawing != null)
        {
            currentDrawing = null;
        }
        else if (OVRInput.GetDown(OVRInput.Button.One))
        {
            SwitchColor();
        }
        else if (OVRInput.GetDown(OVRInput.Button.Four))
        {
            DeleteAllDrawings();
        }
    }

    private void Draw()
    {
        if (currentDrawing == null)
        {
            index = 0;
            currentDrawing = new GameObject().AddComponent<LineRenderer>();
            currentDrawing.material = drawingMaterial;
            currentDrawing.startColor = currentDrawing.endColor = penColors[currentColorIndex];
            currentDrawing.startWidth = currentDrawing.endWidth = penWidth;
            currentDrawing.positionCount = 1;
            currentDrawing.SetPosition(0, tip.position);
            drawings.Add(currentDrawing.gameObject);
        }
        else
        {
            var currentPos = currentDrawing.GetPosition(index);
            if (Vector3.Distance(currentPos, tip.position) > 0.01f)
            {
                index++;
                currentDrawing.positionCount = index + 1;
                currentDrawing.SetPosition(index, tip.position);
            }
        }
    }

    private void SwitchColor()
    {
        if (currentColorIndex == penColors.Length - 1)
        {
            currentColorIndex = 0;
        }
        else
        {
            currentColorIndex++;
        }
        tipMaterial.color = penColors[currentColorIndex];
    }

    private void DeleteAllDrawings()
    {
        foreach (GameObject drawing in drawings)
        {
            Destroy(drawing);
        }
        drawings.Clear();
    }
}
