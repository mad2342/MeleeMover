using Harmony;
using System.Reflection;
using BattleTech;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;

namespace MeleeMover
{
    public class MeleeMover
    {
        internal static string LogPath;
        internal static string ModDirectory;

        // BEN: DebugLevel (0: nothing, 1: error, 2: debug, 3: info)
        internal static int DebugLevel = 2;

        public static void Init(string directory, string settings)
        {
            ModDirectory = directory;
            LogPath = Path.Combine(ModDirectory, "MeleeMover.log");

            Logger.Initialize(LogPath, DebugLevel, ModDirectory, nameof(MeleeMover));

            // Harmony calls need to go last here because their Prepare() methods directly check Settings...
            HarmonyInstance harmony = HarmonyInstance.Create("de.mad.MeleeMover");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

    [HarmonyPatch(typeof(Pathing))]
    [HarmonyPatch("GetMeleeDestsForTarget")]
    public static class Pathing_GetMeleeDestsForTarget_Patch
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> instructionList = instructions.ToList();
            MethodInfo mi = AccessTools.Property(typeof(Vector3), nameof(Vector3.magnitude)).GetGetMethod();
            int index = instructionList.FindIndex(instruction => instruction.operand == mi) - 1;
            instructionList.RemoveRange(index, 6);
            instructionList[index].labels.Clear();

            return instructionList;
        }
    }
}
