using System;
using System.Collections;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using FishNet.Transporting;
using KpattGames.Interaction;
using UnityEngine;
using Random = UnityEngine.Random;

public class ColorCircle : NetworkBehaviour, IInteractable
{
    // We don't care who the owner of the circle is;
    // Allow anyone to change the color.
    [ServerRpc(RequireOwnership = false)]
    private void PushToServer(Color c)
    {
        ChangeColor(c);
    }

    [ObserversRpc(BufferLast = true)]
    private void ChangeColor(Color c)
    {
        GetComponent<SpriteRenderer>().color = c;
    }

    /// <summary>
    /// This changes the syncvar when the circle is clicked.
    /// </summary>
    public void PerformAction()
    {
        Debug.Log("Changing syncvar...");
        
        Color randomColor = Random.ColorHSV();
        randomColor.a = 1f;
        
        PushToServer(randomColor);
    }
}
