using FishNet.Object;
using KpattGames.Characters;
using KpattGames.Interaction;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(ServerHostCircleBehaviour))]
[RequireComponent(typeof(ClientCircleBehaviour))]
public class ColorCircle : NetworkBehaviour, IInteractable
{
    private CircleBehaviour behaviour;

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (base.IsServer)
        {
            behaviour = gameObject.GetComponent<ServerHostCircleBehaviour>();
        }
        else
        {
            behaviour = gameObject.GetComponent<ClientCircleBehaviour>();
        }
    }

    public void PerformAction()
    {
        behaviour.ChangeColor();
        behaviour.SpawnDuplicate();
    }
}
