using Harmony;
using System.Reflection;
using BattleTech;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MeleeMover
{
    public class MeleeMover
    {
        internal static string ModDirectory;

        // BEN: Debug (0: nothing, 1: errors, 2:all)
        internal static int DebugLevel = 2;

        public static void Init(string directory, string settingsJSON) {
            ModDirectory = directory;

            var harmony = HarmonyInstance.Create("de.mad.MeleeMover");
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
