using System;
using FishNet.Object;
using KpattGames.Interaction;
using UnityEngine;
using Random = UnityEngine.Random;

public class ColorCircle : NetworkBehaviour, IInteractable
{
    private SpriteRenderer spriteRenderer;
    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Change the color of the circle when it spawns in.
    public override void OnStartClient()
    {
        base.OnStartClient();
        if (base.IsOwner)
        {
            OnSpawn();
        }
    }

    private void OnSpawn()
    {
        Color randomColor = Random.ColorHSV();
        randomColor.a = 1f;
        
        if (base.IsServer)
        {
            ChangeColor(randomColor);
        }
        else
        {
            RequestColorChange(randomColor);
        }
    }
    
    public void PerformAction()
    {
        // We don't want a server-only build to be able to run this.
        if (!base.IsClient)
            return;
        
        Color randomColor = Random.ColorHSV();
        randomColor.a = 1f;

        if (base.IsServer)
        {
            ChangeColor(randomColor);
        }
        else
        {
            RequestColorChange(randomColor);
        }
        
        BlobFactory.Instance.RequestSpawnBlob(base.Owner);
    }

    [ObserversRpc(IncludeOwner = true, BufferLast = true)]
    private void ChangeColor(Color c)
    {
        Debug.Log("Calling change color");
        spriteRenderer.color = c;
    }

    [ServerRpc(RequireOwnership = false)]
    private void RequestColorChange(Color c)
    {
        ChangeColor(c);
    }
}
