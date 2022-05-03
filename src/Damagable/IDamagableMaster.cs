namespace URPMk2
{
    public interface IDamagableMaster
    {
        public int GetArmor();
        public void CallEventHitByGun(DamageInfo dmgInfo);
        public void CallEventHitByExplosion(DamageInfo dmgInfo);
    }
}
