﻿using Microsoft.Xna.Framework;
using RiskOfTerrain.Buffs;
using RiskOfTerrain.Content.Accessories;
using RiskOfTerrain.Projectiles.Misc;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace RiskOfTerrain.Items
{
    public class RORItem : GlobalItem
    {
        public static List<ChestDropInfo> WhiteTier { get; private set; }
        public static List<ChestDropInfo> GreenTier { get; private set; }
        public static List<ChestDropInfo> RedTier { get; private set; }
        public static List<ChestDropInfo> BossTier { get; private set; }
        public static List<ChestDropInfo> LunarTier { get; private set; }
        public static List<ChestDropInfo> VoidTier { get; private set; }
        public static List<ChestDropInfo> Equipment { get; private set; }

        internal static readonly string[] TooltipNames = new string[]
        {
                "ItemName",
                "Favorite",
                "FavoriteDesc",
                "Social",
                "SocialDesc",
                "Damage",
                "CritChance",
                "Speed",
                "Knockback",
                "FishingPower",
                "NeedsBait",
                "BaitPower",
                "Equipable",
                "WandConsumes",
                "Quest",
                "Vanity",
                "Defense",
                "PickPower",
                "AxePower",
                "HammerPower",
                "TileBoost",
                "HealLife",
                "HealMana",
                "UseMana",
                "Placeable",
                "Ammo",
                "Consumable",
                "Material",
                "Tooltip#",
                "EtherianManaWarning",
                "WellFedExpert",
                "BuffTime",
                "OneDropLogo",
                "PrefixDamage",
                "PrefixSpeed",
                "PrefixCritChance",
                "PrefixUseMana",
                "PrefixSize",
                "PrefixShootSpeed",
                "PrefixKnockback",
                "PrefixAccDefense",
                "PrefixAccMaxMana",
                "PrefixAccCritChance",
                "PrefixAccDamage",
                "PrefixAccMoveSpeed",
                "PrefixAccMeleeSpeed",
                "SetBonus",
                "Expert",
                "Master",
                "JourneyResearch",
                "BestiaryNotes",
                "SpecialPrice",
                "Price",
        };

        public override void Load()
        {
            WhiteTier = new List<ChestDropInfo>();
            GreenTier = new List<ChestDropInfo>();
            RedTier = new List<ChestDropInfo>();
            BossTier = new List<ChestDropInfo>();
            LunarTier = new List<ChestDropInfo>();
            VoidTier = new List<ChestDropInfo>();
            Equipment = new List<ChestDropInfo>();
        }

        public override void Unload()
        {
            WhiteTier?.Clear();
            WhiteTier = null;
            GreenTier?.Clear();
            GreenTier = null;
            RedTier?.Clear();
            RedTier = null;
            BossTier?.Clear();
            BossTier = null;
            LunarTier?.Clear();
            LunarTier = null;
            VoidTier?.Clear();
            VoidTier = null;
            Equipment?.Clear();
            Equipment = null;
        }

        public static int GetIndex(List<TooltipLine> tooltips, string lineName)
        {
            int myIndex = FindLineIndex(lineName);
            int i = 0;
            for (; i < tooltips.Count; i++)
            {
                if (tooltips[i].Mod == "Terraria" && FindLineIndex(tooltips[i].Name) >= myIndex)
                {
                    if (lineName == "Tooltip#")
                    {
                        int finalIndex = i;
                        while (i < tooltips.Count)
                        {
                            if (tooltips[i].Name.StartsWith("Tooltip"))
                            {
                                finalIndex = i;
                            }
                            i++;
                        }
                        return finalIndex;
                    }
                    return i;
                }
            }
            return i;
        }

        private static int FindLineIndex(string name)
        {
            if (name.StartsWith("Tooltip"))
            {
                name = "Tooltip#";
            }
            for (int i = 0; i < TooltipNames.Length; i++)
            {
                if (name == TooltipNames[i])
                {
                    return i;
                }
            }
            return -1;
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            player.ROR().Accessories.AddItemStack(item);
        }

        public override bool? UseItem(Item item, Player player)
        {
            var ror = player.ROR();
            if (Main.myPlayer == player.whoAmI && !item.IsAir && item.damage > 0 && ror.accShuriken != null && ror.shurikenCharges > 0 && (ror.shurikenRechargeTime > 20 || ror.shurikenCharges == ror.shurikenChargesMax))
            {
                ror.shurikenCharges--;
                ror.shurikenRechargeTime = 0;
                if (ror.shurikenCharges <= 0)
                {
                    player.ClearBuff(ModContent.BuffType<ShurikenBuff>());
                }
                var p = Projectile.NewProjectileDirect(player.GetSource_Accessory(ror.accShuriken), player.Center, Vector2.Normalize(Main.MouseWorld - player.Center) * 20f, 
                    ModContent.ProjectileType<ReloadingShurikenProj>(), Math.Clamp(player.GetWeaponDamage(item) * 2, 10, 200), 1f, player.whoAmI);
                p.DamageType = item.DamageType;
            }
            return null;
        }
    }
}