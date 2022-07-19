using FishNet.Object;
using KpattGames.Interaction;
using UnityEngine;
using Random = UnityEngine.Random;

public class ColorCircle : NetworkBehaviour, IInteractable
{
    public void PerformAction()
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

    [ObserversRpc(IncludeOwner = true, BufferLast = true)]
    private void ChangeColor(Color c)
    {
        GetComponent<SpriteRenderer>().color = c;
    }

    [ServerRpc(RequireOwnership = false)]
    private void RequestColorChange(Color c)
    {
        ChangeColor(c);
    }
}
