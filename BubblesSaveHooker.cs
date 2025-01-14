﻿//using FinneanTweaks;
using HarmonyLib;
using Kingmaker;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.EntitySystem.Persistence;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Class.LevelUp;
using Kingmaker.UnitLogic.Class.LevelUp.Actions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Findus
{
    [HarmonyPatch]
    static class SaveHooker
    {

        [HarmonyPatch(typeof(ZipSaver))]
        [HarmonyPatch("SaveJson"), HarmonyPostfix]
        static void Zip_Saver(string name, ZipSaver __instance)
        {
            DoSave(name, __instance);
        }

        [HarmonyPatch(typeof(FolderSaver))]
        [HarmonyPatch("SaveJson"), HarmonyPostfix]
        static void Folder_Saver(string name, FolderSaver __instance)
        {
            DoSave(name, __instance);
        }

        static void DoSave(string name, ISaver saver)
        {
            if (name != "header")
                return;

            try
            {
                var serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                var writer = new StringWriter();
                serializer.Serialize(writer, FindusSettings.Instance);
                writer.Flush();
                saver.SaveJson(LoadHooker.FileName, writer.ToString());
            }
            catch (Exception e)
            {
                Main.logger.Log(e.ToString());
            }
        }
    }

    [HarmonyPatch(typeof(Game))]
    static class LoadHooker
    {
        public const string FileName = "header.json.barley_findussettings";

        [HarmonyPatch("LoadGame"), HarmonyPostfix]
        static void LoadGame(SaveInfo saveInfo)
        {
            try
            {
                using (saveInfo)
                {
                    using (saveInfo.GetReadScope())
                    {
                        ThreadedGameLoader.RunSafelyInEditor((Action)(() =>
                        {
                            string raw;
                            using (ISaver saver = saveInfo.Saver.Clone())
                            {
                                raw = saver.ReadJson(FileName);
                            }
                            if (raw != null)
                            {
                                var serializer = new JsonSerializer();
                                var rawReader = new StringReader(raw);
                                var jsonReader = new JsonTextReader(rawReader);
                                FindusSettings.Instance = serializer.Deserialize<FindusSettings>(jsonReader);
                            }
                            else
                            {
                                FindusSettings.Instance = new FindusSettings();
                            }
                        })).Wait();
                    }
                }
            }
            catch(Exception e)
            {
                Main.logger.Error(e.ToString());
            }
        }
    }
    public class FindusSettings
    {
        public static FindusSettings Instance = new FindusSettings();
        public string MaterialGuid;
        public string Enchantment1GUID;
    }
}
