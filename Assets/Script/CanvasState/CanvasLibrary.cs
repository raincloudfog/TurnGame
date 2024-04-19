using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class CanvasLibrary : MonoBehaviour
{
    [SerializeField]
    Dictionary<CanvasName, CanvasState> CanvasDic = new Dictionary<CanvasName, CanvasState>();    

    public CanvasState _CanvasStat;

    public enum CanvasName
    {
        TalkCanvas,
        InvenCanvas,
        BattleCanvas
    }

    public void SetCanvas(CanvasName canvasName)
    {
        if(CanvasDic.ContainsKey(canvasName))
            _CanvasStat = CanvasDic[canvasName];
    }

    public void AddCanvas(CanvasName name,CanvasState canvas)
    {
        if (!CanvasDic.ContainsKey(name))
        {
            CanvasDic.Add(name,canvas);
        }
        else if (CanvasDic.ContainsKey (name))
        {
            CanvasDic[name] = canvas;
        }
    }
}
