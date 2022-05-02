namespace URPMk2
{
    public interface IDamagableMaster
    {
        public int GetArmor();
        public void CallEventHitByGun(float dmg, float pen);
        public void CallEventHitByExplosion(float dmg, float pen);
    }
}
