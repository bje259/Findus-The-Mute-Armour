using HarmonyLib;
using Kingmaker;
using Kingmaker.AreaLogic.Etudes;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.Root;
using Kingmaker.Cheats;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UI.Common;
using Kingmaker.UI.MVVM._VM.ServiceWindows.Spellbook.Metamagic;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Buffs;
using Kingmaker.UnitLogic.Class.LevelUp.Actions;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Groups;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Parts;
using Kingmaker.Utility;
using Kingmaker.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.UI;
using Kingmaker.UI.MVVM._PCView.InGame;
using UnityEngine;
using UnityModManagerNet;
using static UnityModManagerNet.UnityModManager;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.Blueprints.Items.Equipment;
using Kingmaker.Items;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Blueprints.Items;
using Kingmaker.UI.MVVM._VM.ServiceWindows.Inventory;
using TabletopTweaks.Extensions;
using Kingmaker.Blueprints.Items.Components;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.UI.MVVM._PCView.ServiceWindows.Inventory;

namespace Findus
{
#if DEBUG
    [EnableReloading]
#endif
    public class Main
    {
        public static bool IsEnabled = false;
        public static UnityModManager.ModEntry.ModLogger logger;
        public static Findus.UI.InventorySmartItemVM InventorySmartItem;
        public static BlueprintHiddenItem itm;
        public static void Log(string ina)
        {
            logger.Log(ina);
        }
        public static void LogDebug(string ina)
        {
            logger.Log(ina);
        }
        static bool Load(UnityModManager.ModEntry modEntry)
        {
            try
            {
                //TabletopTweaks.Config.ModSettings.ModEntry = modEntry;
               
               // var harmony = new Harmony(modEntry.Info.Id);
               // harmony.PatchAll();
#if DEBUG
               // modEntry.OnGUI = OnGui;
#endif
                //modEntry.OnUnload = Unload;
                //modEntry.OnToggle = OnToggle;
                //TabletopTweaks.Config.ModSettings.ModEntry = modEntry;
                //TabletopTweaks.Config.ModSettings.LoadAllSettings();
                //TabletopTweaks.Config.ModSettings.ModEntry.OnSaveGUI = OnSaveGUI;
               // TabletopTweaks.Config.ModSettings.ModEntry.OnGUI = OnGui;
               // harmony.PatchAll();
               // PostPatchInitializer.Initialize();
                
                var harmony = new Harmony(modEntry.Info.Id);
                TabletopTweaks.Config.ModSettings.ModEntry = modEntry;
                logger = modEntry.Logger;
                TabletopTweaks.Config.ModSettings.LoadAllSettings();
                TabletopTweaks.Config.ModSettings.ModEntry.OnSaveGUI = OnSaveGUI;
                TabletopTweaks.Config.ModSettings.ModEntry.OnGUI = OnGui;
                IsEnabled = TabletopTweaks.Config.ModSettings.ModEntry.Enabled;
                harmony.PatchAll();
                //PostPatchInitializer.Initialize();
            }
            catch (Exception e)
            {
                throw e;
            }
            return true;
        }
        static void OnSaveGUI(UnityModManager.ModEntry modEntry)
        {
           // ModSettings.SaveSettings("Fixes.json", ModSettings.Fixes);
            //ModSettings.SaveSettings("AddedContent.json", ModSettings.AddedContent);
            //ModSettings.SaveSettings("Homebrew.json", ModSettings.Homebrew);
        }
        static bool OnToggle(UnityModManager.ModEntry modEntry, bool value /* active or inactive */)
        {
            IsEnabled = value;
            return true; // Permit or not.
        }
        static bool Unload(UnityModManager.ModEntry modEntry)
        {
            new Harmony(modEntry.Info.Id).UnpatchAll(modEntry.Info.Id);
            return true;
        }
#if DEBUG
        static void OnGui(ModEntry modentry)
        {
            if (GUILayout.Button("Bruh"))
            {
                try
                {
                    var fionaitem = Game.Instance.Player.Inventory.Add(itm.CreateEntity());
                    InventorySmartItem = new UI.InventorySmartItemVM(new UniRx.ReactiveProperty<UnitDescriptor>(Game.Instance.Player.AllCharacters.First().Descriptor), fionaitem, () => { });
                    InventorySmartItem.SmartItemSlotVM = new SmartItemSlotVM(InventorySmartItem.m_SmartItem,InventorySmartItem.EquipItem,InventorySmartItem.EquipItemInSlot);
                    InventorySmartItem.RefreshData();
                    InventorySmartItem.SelectItem(2);
                    InventorySmartItem.EquipItem(InventorySmartItem.CurrentPolymorph);
                }
                catch(Exception e)
                {
                    Main.logger.Log(e.ToString());
                }
            }
            if(GUILayout.Button("fdassfad"))
            {
                InventorySmartItem.SelectItem(1);
                InventorySmartItem.EquipItem(InventorySmartItem.SmartItemSlotVM.Item.Value);
                
            }
            if (GUILayout.Button("Generate Armor Enchant List"))
            {
                foreach (var enchantbp in Utilities.GetAllBlueprints().Entries.Where(b => b.Type == typeof(BlueprintArmorEnchantment)).Select(a => ResourcesLibrary.TryGetBlueprint<BlueprintArmorEnchantment>(a.Guid)))
                {
                      if(enchantbp.Description.Length > 0)
                    Main.logger.Log("m_EnchantsÄ[\"" + enchantbp.Name + "\"] = \"" + enchantbp.AssetGuidThreadSafe + "\";");
                }
            }
        }
#endif
    }
    [HarmonyPatch(typeof(Game),"OnAreaLoaded")]
    public static class onarealoaded
    {
        public static void Postfix()
        {
            var findus = ResourcesLibrary.TryGetBlueprint<BlueprintHiddenItem>("cd63240271f74379bfcf586ff29c34ba");
            if(!Game.Instance.Player.Inventory.Any(a => a.Blueprint.AssetGuid == "cd63240271f74379bfcf586ff29c34ba"))
            {
                Game.Instance.Player.Inventory.Add(findus);
            }
        }
    }
    [HarmonyPatch(typeof(BlueprintsCache), "Init")]
    public static class initpatch
    {
        public static void Postfix()
        {

            try
            {
                Main.logger.Log("Initializizizie");
                var newitem = ResourcesLibrary.TryGetBlueprint<BlueprintHiddenItem>("95c126deb99ba054aa5b84710520c035");
                /*foreach(var bidän in newitem.GetComponents<ItemPolymorph>())
                {
                    bidän.m_PolymorphItems.Clear();
                    foreach (var j in Findus.FionaHandler.ArmourTypes.Values)
                    {.
                        var ja = ResourcesLibrary.TryGetBlueprint<BlueprintItemArmor>(j);
                        bidän.m_PolymorphItems.Add(ja.ToReference<BlueprintItemReference>());
                    }

                }*/
                var jo = TabletopTweaks.Utilities.Helpers.CreateCopy<BlueprintHiddenItem>(newitem);
                //jo.SetName("Asdasdasd");
                //jo.SetDescription("jojojojo");
                // var bid = new List<BlueprintItemReference>() { };
                //jo.RemoveComponents<ItemPolymorph>();
                //jo.AddComponent<ItemPolymorph>();
                //var component = jo.GetComponent<ItemPolymorph>();
                //if (component.m_PolymorphItems == null) component.m_PolymorphItems = new List<BlueprintItemReference>();
                //component.m_PolymorphItems.Clear();
                var armortypes = new List<BlueprintItemArmor>();
                foreach(var armor in Findus.FionaHandler.ArmourTypes.Values)
                {
                    var ja = ResourcesLibrary.TryGetBlueprint<BlueprintItemArmor>(armor);
                    var newbp = TabletopTweaks.Utilities.Helpers.CreateCopy<BlueprintItemArmor>(ja);
                    newbp.SetDescription("This is a +" + newbp.Enchantments.Count() + " " + newbp.Name);
                    newbp.SetName("Findus the " + newbp.Name);
                    newbp.name = "FindusThe" + newbp.name;
                    newbp.AssetGuid = TabletopTweaks.Config.ModSettings.Blueprints.GetGUID(newbp.Name);
                    TabletopTweaks.Resources.AddBlueprint(newbp);
                    armortypes.Add(newbp);
                }
                foreach (var component in jo.GetComponents<ItemPolymorph>())
                {
                    if (component.m_PolymorphItems == null) component.m_PolymorphItems = new List<BlueprintItemReference>();
                    component.m_PolymorphItems.Clear();
                    foreach (var j in armortypes)
                    {


                        // bid.Add(ja.ToReference<BlueprintItemReference>());
                        component.m_PolymorphItems.Add(j.ToReference<BlueprintItemReference>());
                    }
                }
                jo.SetName("Findus the Mute Armour");
                jo.AssetGuid = TabletopTweaks.Config.ModSettings.Blueprints.GetGUID(jo.Name);
                TabletopTweaks.Resources.AddBlueprint(jo);
                //var asd = new Guid();
                //Main.itm = jo.CreateEntity<ItemEntitySimple>();
                //var part = Main.itm.Ensure<ItemPolymorph.ItemPolymorphPart>();
                //part.ApplyPostLoadFixes();
                Main.itm = jo;
               // ResourcesLibrary.BlueprintsCache.AddCachedBlueprint(new BlueprintGuid(new Guid("fhg93f7cf2b150e4f5jg2b3c389ba74h")),jo);
                //var dfsfd = jo.CreateEntity();
                
                //var dfa = new HashSet<BlueprintItemReference>(bid);
            }
            catch (Exception e)
            {
                Main.logger.Log(e.ToString());
            }
        }
    }
}