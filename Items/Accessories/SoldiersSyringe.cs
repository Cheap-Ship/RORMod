using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RORMod.Items.Accessories
{
    [AutoloadEquip(EquipType.Shoes)]
    public class SoldiersSyringe : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            RORItem.WhiteTier.Add(Type);
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 36;
            Item.accessory = true;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 50);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Generic) -= 0.12f;
            player.GetAttackSpeed(DamageClass.Generic) += 0.2f;
        }
    }
}