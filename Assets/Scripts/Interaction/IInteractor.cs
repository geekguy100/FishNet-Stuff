namespace KpattGames.Interaction
{
    public interface IInteractor
    {
        public void OnInteractableNearby(IInteractable interactable);
        public IInteractable OnInteractableLeft();
        public void Interact();
    }
}