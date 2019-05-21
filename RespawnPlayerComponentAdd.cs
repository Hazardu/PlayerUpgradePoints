using TheForest.Utils;
using TheForest.Save;

namespace PlayerUpgradePoints
{
    public class RespawnPlayerComponentAdd : PlayerRespawnMP
    {
        protected override void Respawn()
        {
            base.Respawn();
            Invoke("AddModComponent", 3);

        }
        void AddModComponent()
        {
            if (UpgradePointsMod.instance != null)
            {
                Destroy(UpgradePointsMod.instance);
            }
            UpgradePointsMod spawn = LocalPlayer.GameObject.AddComponent<UpgradePointsMod>();
            UpgradePointsMod.instance = spawn;
            UpgradePointsMod.instance.Respawn = true;
        }
    }
}


    

