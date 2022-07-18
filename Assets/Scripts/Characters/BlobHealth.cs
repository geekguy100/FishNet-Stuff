namespace KpattGames.Characters
{
    public struct BlobHealth
    {
        public int currentHealth;
        public readonly int maxHealth;

        public BlobHealth(int currentHealth, int maxHealth)
        {
            this.currentHealth = currentHealth;
            this.maxHealth = maxHealth;
        }
    }
}