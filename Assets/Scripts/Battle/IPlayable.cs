namespace Battle
{
    public interface IPlayable
    {
        int HealthPoint { get; }
        float MoveSpeed { get; }
        int AttackDamage { get; }
        float AttackSpeed { get; }
        float AttackRange { get; }
        string Name { get; }

        public void ReceiveDamage(int damage);

    }
}
