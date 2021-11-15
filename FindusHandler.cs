using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Items;
using Kingmaker.UI.MVVM._VM.ServiceWindows.Inventory;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.Utility;
using Owlcat.Runtime.Core.Logging;
using Owlcat.Runtime.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Findus
{
    //Add item with ItemPolyMorphPart


    public static class FindusHandler
    {
       /* public static void RefreshFinneanItems(this InventorySmartItemVM SmartItemVM)
        {
            try
            {
                if (SmartItemVM != null && SmartItemVM.m_SmartItem != null)
                    FinneanEnchantmentHandler.AddEnchantments(SmartItemVM.m_SmartItem);
                if (SmartItemVM.PolymorphItems != null)
                    foreach (var iteme in SmartItemVM?.PolymorphItems)
                    {
                        FinneanEnchantmentHandler.AddEnchantments(iteme);
                    }
            }
            catch (Exception e)
            {
                Main.logger.Error(e.ToString());
            }
        }*/
        public static Dictionary<string, string> EnchantsTier1
        {
            //Do scaling resistance too
            get
            {
                if (m_Enchants1 == null)
                {
                    try
                    {
                        m_Enchants1 = new Dictionary<string, string>();
                        m_Enchants1["Adamantine"] = "933456ff83c454146a8bf434e39b1f93";
                        m_Enchants1["Adamantine"] = "5faa3aaee432ac444b101de2b7b0faf7";
                        m_Enchants1["Adamantine"] = "aa25531ab5bb58941945662aa47b73e7";
                        m_Enchants1["Enhancement +1"] = "a9ea95c5e02f9b7468447bc1010fe152";
                        m_Enchants1["Enhancement +2"] = "758b77a97640fd747abf149f5bf538d0";
                        m_Enchants1["Enhancement +3"] = "9448d3026111d6d49b31fc85e7f3745a";
                        m_Enchants1["Enhancement +4"] = "eaeb89df5be2b784c96181552414ae5a";
                        m_Enchants1["Enhancement +5"] = "6628f9d77fd07b54c911cd8930c0d531";
                        m_Enchants1["Enhancement +6"] = "de15272d1f4eb7244aa3af47dbb754ef";
                        m_Enchants1["Arrow Deflection"] = "faf86f0f02b30884c860b675a0df8e2e";
                        m_Enchants1["Light Fortification"] = "1e69e9029c627914eb06608dad707b36";
                        m_Enchants1["Moderate Fortification"] = "62ec0b22425fb424c82fd52d7f4c02a5";
                        m_Enchants1["Heavy Fortification"] = "9b1538c732e06544bbd955fee570a2be";
                        m_Enchants1["Conditioning"] = "d884882b038db4741a12b006e9de3e8b";
                        m_Enchants1["Greater Shadow"] = "6b090a291c473984baa5b5bb07a1e300";
                        m_Enchants1["Hundred Pockets"] = "710c2a979241a5a4d846b3d6232190fb";
                        m_Enchants1["Mithral"] = "7b95a819181574a4799d93939aa99aff";
                        m_Enchants1["Shadow"] = "d64d7aa52626bc24da3906dce17dbc7d";
                        m_Enchants1["Singing Steel"] = "451601816a45311419b77b83f253b75b";
                        m_Enchants1["Tower Shield"] = "f6b1f4378dd64044db145a1c2afa589f";
                        m_Enchants1["Arcane Armor Balanced"] = "53fba8eec3abd214b98a57b12d7ad0a7";
                        m_Enchants1["Arcane Armor Invulnerability"] = "4ffa3c3d5f6cdfb4eaf15f11d8e55bd1";
                        m_Enchants1["Arcane Armor Shadow"] = "4e916cbeced676f4e83e02ac65dc562c";
                        m_Enchants1["Arcane Armor Shadow Greater"] = "dd8f2032f05d72740961fc95201a5b15";
                        m_Enchants1["Electric Aura"] = "8e7f512654a46d949a1eee77249bd75d";
                        m_Enchants1["Acid Aura"] = "87b96ae9ecf792741b409e85bebe30a7";
                        m_Enchants1["Fire Aura"] = "dcf93e8d4b970ee408b410f2b01ad13f";
                        m_Enchants1["Cold Aura"] = "54d0633c038e96b4a9d5422e0801dfb5";
                    }
                    catch (Exception e)
                    {
                        Main.logger.Error(e.ToString());
                        throw;
                    }
                }
                return m_Enchants1;
            }
        }
        public static Dictionary<string, string> m_Enchants1;
        public static Dictionary<string, string> ArmourTypes
        {
            get
            {
                if (m_ArmourTypes == null)
                {
                    try
                    {
                        m_ArmourTypes = new Dictionary<string, string>();
                        m_ArmourTypes["Findus the Banded Mail"] = "1c1db1866104435ea7fc27b9c3faa97f";
                        m_ArmourTypes["Findus the Breastplate"] = "1ed08893c9204efe8a3310c8dbe74665";
                        m_ArmourTypes["Findus the Chainmail"] = "0a210038fd6d45d281f13b7fba76222f";
                        m_ArmourTypes["Findus the Chainshirt"] = "701a856bc3624eccba521da7adfec348";
                        m_ArmourTypes["Findus the Full Plate"] = "2664412ea3364d1b9d02dfd86615038e";
                        m_ArmourTypes["Findus the Half-Plate"] = "59e441aca8ba4b97bf99f59c5a337a99";
                        m_ArmourTypes["Findus the Haramaki"] = "7e511945a1774159ba86574253c823cb";
                        m_ArmourTypes["Findus the Hide Armor"] = "e10f7792e0a0401ca5142481d22046bd";
                        m_ArmourTypes["Findus the Leather Armor"] = "607a2f03425c4e468c3f7263732523c3";
                        m_ArmourTypes["Findus the Padded Armor"] = "50aec32db4d24f7c9e085dfbef4162d3";
                        m_ArmourTypes["Findus the Scalemail"] = "e7789ce40b9e4878922af7f8f7f9f534";
                        m_ArmourTypes["Findus the Studded Leather"] = "18552b85ceb6491190f21fdb086bff76";
                    }
                    catch (Exception e)
                    {
                        Main.logger.Error(e.ToString());
                        throw;
                    }
                }
                return m_ArmourTypes;
            }
        }
        public static Dictionary<string, string> m_ArmourTypes;

        public static Dictionary<string, string> ArmourTypes_description
        {
            get
            {
                if (m_ArmourTypesDescription == null)
                {
                    try
                    {
                        m_ArmourTypesDescription = new Dictionary<string, string>();
                        m_ArmourTypesDescription["1c1db1866104435ea7fc27b9c3faa97f"] = "Banded Mail";
                        m_ArmourTypesDescription["1ed08893c9204efe8a3310c8dbe74665"] = "Breastplate";
                        m_ArmourTypesDescription["0a210038fd6d45d281f13b7fba76222f"] = "Chainmail";
                        m_ArmourTypesDescription["701a856bc3624eccba521da7adfec348"] = "Chainshirt";
                        m_ArmourTypesDescription["2664412ea3364d1b9d02dfd86615038e"] = "Full Plate";
                        m_ArmourTypesDescription["59e441aca8ba4b97bf99f59c5a337a99"] = "Half-Plate";
                        m_ArmourTypesDescription["7e511945a1774159ba86574253c823cb"] = "Haramaki";
                        m_ArmourTypesDescription["e10f7792e0a0401ca5142481d22046bdr"] = "Hide Armor";
                        m_ArmourTypesDescription["607a2f03425c4e468c3f7263732523c3"] = "Leather Armor";
                        m_ArmourTypesDescription["50aec32db4d24f7c9e085dfbef4162d3"] = "Padded Armor";
                        m_ArmourTypesDescription["e7789ce40b9e4878922af7f8f7f9f534"] = "Scalemail";
                        m_ArmourTypesDescription["18552b85ceb6491190f21fdb086bff76"] = "Studded Leather";
                    }
                    catch (Exception e)
                    {
                        Main.logger.Error(e.ToString());
                        throw;
                    }
                }
                return m_ArmourTypesDescription;
            }
        }
        public static Dictionary<string, string> m_ArmourTypesDescription;

        public static Dictionary<int, string> EnhancementBoni
        {
            get
            {
                if (m_EnhancementBoni == null)
                {
                    //Change these to the armor ones
                    m_EnhancementBoni = new Dictionary<int, string>();
                    m_EnhancementBoni[1] = "d42fc23b92c640846ac137dc26e000d4";
                    m_EnhancementBoni[2] = "eb2faccc4c9487d43b3575d7e77ff3f5";
                    m_EnhancementBoni[3] = "80bb8a737579e35498177e1e3c75899b";
                    m_EnhancementBoni[4] = "783d7d496da6ac44f9511011fc5f1979";
                    m_EnhancementBoni[5] = "bdba267e951851449af552aa9f9e3992";
                    m_EnhancementBoni[6] = "0326d02d2e24d254a9ef626cc7a3850f";

                }
                return m_EnhancementBoni;
            }
        }
        public static Dictionary<int, string> m_EnhancementBoni;

        public static Dictionary<string, string> Materials
        {
            get
            {
                if (m_Materials == null)
                {
                    //Need to add guids and shit
                    m_Materials = new Dictionary<string, string>();


                }
                return m_Materials;
            }
        }
        public static Dictionary<string, string> m_Materials;
        /// <summary>
        /// Removes Brilliant energy & Heartseeker and adds enchants according to settings.
        /// </summary>
        public static void AddEnchantments(ItemEntity findus)//, BlueprintItemEnchantment enchant)
        {
            if (findus == null || findus.GetType() != typeof(ItemEntityArmor)) return;
            string enchantmentguid = EnhancementBoni[Kingmaker.Game.Instance.Player.Chapter];
            // Main.logger.Log("Adding Enchantments...");
            /*if (!FindusSettings.Instance.Enchantment1GUID.IsNullOrEmpty() && !FindusSettings.Instance.Enchantment2GUID.IsNullOrEmpty() && !FindusSettings.Instance.MaterialGuid.IsNullOrEmpty())
            {
                var enchantment = findus.GetEnchantment(ResourcesLibrary.TryGetBlueprint<BlueprintItemEnchantment>("66e9e299c9002ea4bb65b6f300e43770"));
                if (enchantment != null)
                {
                    findus.m_HasExternalEnchantments = true;
                    findus.RemoveEnchantment(enchantment);
                }
            }*/
            /*if (!FindusSettings.Instance.Enchantment1GUID.IsNullOrEmpty() && !FindusSettings.Instance.Enchantment2GUID.IsNullOrEmpty() && !FindusSettings.Instance.MaterialGuid.IsNullOrEmpty())
            {
                var enchantment = findus.GetEnchantment(ResourcesLibrary.TryGetBlueprint<BlueprintItemEnchantment>("e252b26686ab66241afdf33f2adaead6"));
                if (enchantment != null)
                {
                    findus.m_HasExternalEnchantments = true;
                    findus.RemoveEnchantment(enchantment);
                }
            }*/
            foreach (var enchantment in findus.Enchantments.ToTempList())
            {
                if ((EnchantsTier1.Values.Contains(enchantment.Blueprint.AssetGuidThreadSafe) && FindusSettings.Instance.Enchantment1GUID != enchantment.Blueprint.AssetGuidThreadSafe) || (enchantmentguid != enchantment.Blueprint.AssetGuidThreadSafe && EnhancementBoni.ContainsValue(enchantment.Blueprint.AssetGuidThreadSafe)) || (Materials.Values.Contains(enchantment.Blueprint.AssetGuidThreadSafe) && FindusSettings.Instance.MaterialGuid != enchantment.Blueprint.AssetGuidThreadSafe))
                {
                    findus.RemoveEnchantment(enchantment);
                }
            }
            if (enchantmentguid != "" && !findus.Enchantments.Any(a => a.Blueprint.AssetGuidThreadSafe == enchantmentguid))
            {
                findus.AddEnchantment(ResourcesLibrary.TryGetBlueprint<BlueprintWeaponEnchantment>(enchantmentguid), new MechanicsContext(default));
            }
            if (FindusSettings.Instance.Enchantment1GUID != null && !findus.Enchantments.Any(a => a.Blueprint.AssetGuidThreadSafe == FindusSettings.Instance.Enchantment1GUID)) findus.AddEnchantment(ResourcesLibrary.TryGetBlueprint<BlueprintItemEnchantment>(FindusSettings.Instance.Enchantment1GUID), new MechanicsContext(default));
            if (FindusSettings.Instance.MaterialGuid != null && !findus.Enchantments.Any(a => a.Blueprint.AssetGuidThreadSafe == FindusSettings.Instance.MaterialGuid)) findus.AddEnchantment(ResourcesLibrary.TryGetBlueprint<BlueprintItemEnchantment>(FindusSettings.Instance.MaterialGuid), new MechanicsContext(default));
            // Main.logger.Log("Sucessfully added enchantments.");
        }
    }
   /* [HarmonyLib.HarmonyPatch(typeof(InventorySmartItemVM), "EquipItem")]
    public static class EquipItem_Patch
    {
        public static void Postfix(InventorySmartItemVM __instance, ItemEntity item)
        {
            __instance.RefreshFinneanItems();
            FinneanEnchantmentHandler.AddEnchantments(item);
        }
    }
    [HarmonyLib.HarmonyPatch(typeof(InventorySmartItemVM), "EquipItemInSlot")]
    public static class EquipItemInSlot_Patch
    {
        public static void Postfix(InventorySmartItemVM __instance, ItemEntity item)
        {
            FinneanEnchantmentHandler.AddEnchantments(item);
        }
    }
    [HarmonyLib.HarmonyPatch(typeof(InventorySmartItemVM), "RefreshData")]
    public static class RefreshData_Patch
    {
        public static void Postfix(InventorySmartItemVM __instance)
        {
            __instance.RefreshFinneanItems();
        }
    }
    // [HarmonyLib.HarmonyPatch(typeof(InventorySmartItemVM), "SelectItem",new Type[] { typeof(int) })]
    public static class SelectItem_Patch
    {
        public static void Postfix(InventorySmartItemVM __instance)
        {
            __instance.RefreshFinneanItems();
        }
    }
    //[HarmonyLib.HarmonyPatch(typeof(ItemEntity), "ContainsEnchantmentFromBlueprint")]
    public static class ContainsEnchantmentFromBlueprint_patch
    {
        public static void Postfix(ItemEntity __instance, ref bool __result, BlueprintItemEnchantment enchantment)
        {
            try
            {
                //This is dirty but __instance seems to always be null so cant check that its finnean.
                if (enchantment.AssetGuidThreadSafe == "66e9e299c9002ea4bb65b6f300e43770")//if (__instance != null && __instance.Blueprint != null && __instance.Blueprint.NameForAcronym.Contains("Finnean"))
                {
                    __result = true;
                }
            }
            catch (Exception e)
            {
                Main.logger.Error(e.ToString());
            }
        }
        public static void Prefix(ItemEntity __instance, ref bool __result, BlueprintItemEnchantment enchantment)
        {
            try
            {
                if (__instance == null)
                {
                    Main.logger.Log("EnchantMentContainsNull");

                }
                else
                {
                    Main.logger.Log("not null " + __instance.ToString());
                }
            }
            catch (Exception e)
            {
                Main.logger.Error(e.ToString());
            }
        }
    }
   /* [HarmonyLib.HarmonyPatch(typeof(ItemEntity), "ApplyEnchantments")]
    public static class ApplyEnchantments_patch
    {
        public static bool Prefix(ItemEntity __instance, bool onInitializeOrEquip)
        {

            try
            {
                if (!__instance.Blueprint.NameForAcronym.Contains("Finnean")) return true;
                if (!__instance.InstantiateEnchantments)
                {
                    return false;
                }
                ItemEnchantmentCollection itemEnchantmentCollection = __instance.m_Enchantments = __instance.Facts.EnsureFactProcessor<ItemEnchantmentCollection>();
                List<ItemEnchantment> list = null;
                List<BlueprintItemEnchantment> list2 = null;
                if (!onInitializeOrEquip)
                {
                    foreach (ItemEnchantment itemEnchantment in itemEnchantmentCollection.RawFacts)
                    {
                        if (itemEnchantment.ParentContext == null && !__instance.Blueprint.Enchantments.HasItem(itemEnchantment.Blueprint))
                        {
                            list = (list ?? ListPool<ItemEnchantment>.Claim(5));
                            list.Add(itemEnchantment);
                            LogChannel @default = PFLog.Default;
                            ICanBeLogContext blueprint = __instance.Blueprint;
                            string format = "{0}: remove redunant enchantment '{1}' (owner: {2})";
                            object arg = itemEnchantment;
                            UnitDescriptor arg2;
                            if ((arg2 = __instance.Owner) == null)
                            {
                                ItemsCollection collection = __instance.Collection;
                                arg2 = ((collection != null) ? collection.OwnerUnit : null);
                            }
                            @default.Warning(blueprint, string.Format(format, __instance, arg, arg2), Array.Empty<object>());
                        }
                    }
                }
                foreach (BlueprintItemEnchantment blueprintItemEnchantment in __instance.Blueprint.Enchantments)
                {
                    if (blueprintItemEnchantment.AssetGuidThreadSafe != "66e9e299c9002ea4bb65b6f300e43770" && !ItemEntity.ContainsEnchantmentFromBlueprint(itemEnchantmentCollection, blueprintItemEnchantment))
                    {
                        list2 = (list2 ?? ListPool<BlueprintItemEnchantment>.Claim(5));
                        list2.Add(blueprintItemEnchantment);
                        if (!onInitializeOrEquip)
                        {
                            LogChannel default2 = PFLog.Default;
                            ICanBeLogContext blueprint2 = __instance.Blueprint;
                            string format2 = "{0}: restore missing enchantment '{1}' (owner: {2})";
                            object arg3 = blueprintItemEnchantment;
                            UnitDescriptor arg4;
                            if ((arg4 = __instance.Owner) == null)
                            {
                                ItemsCollection collection2 = __instance.Collection;
                                arg4 = ((collection2 != null) ? collection2.OwnerUnit : null);
                            }
                            default2.Warning(blueprint2, string.Format(format2, __instance, arg3, arg4), Array.Empty<object>());
                        }
                    }
                }
                if (list != null)
                {
                    foreach (ItemEnchantment fact in list)
                    {
                        itemEnchantmentCollection.RemoveFact(fact);
                    }
                }
                if (list2 != null)
                {
                    using (ContextData<ItemEntity.BuiltInEnchantmentFlag>.Request())
                    {
                        foreach (BlueprintItemEnchantment blueprint3 in list2)
                        {
                            __instance.AddEnchantmentInternal(blueprint3, null, null);
                        }
                    }
                }
                if (list != null)
                {
                    ListPool<ItemEnchantment>.Release(list);
                }
                if (list2 != null)
                {
                    ListPool<BlueprintItemEnchantment>.Release(list2);
                }
                return false;
            }
            catch (Exception e)
            {
                Main.logger.Error(e.ToString());
                return true;
            }
        }
    }*/
}
