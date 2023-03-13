using FrooxEngine;
using HarmonyLib;
using NeosModLoader;

namespace ScalableWorldOrbs {
	public class ScalableWorldOrbs : NeosMod {
		public override string Name => "ScalableWorldOrbs";
		public override string Author => "Delta";
		public override string Version => "1.0.0";
		public override string Link => "https://github.com/XDelta/ScalableWorldOrbs";

		[AutoRegisterConfigKey]
		private static readonly ModConfigurationKey<bool> modEnabled = new ModConfigurationKey<bool>("modEnabled", "Mod Enabled", () => true);

		private static ModConfiguration Config;
		public override void OnEngineInit() {
			Config = GetConfiguration();
			Config.Save(true);
			Harmony harmony = new Harmony("net.deltawolf.ScalableWorldOrbs");
			harmony.PatchAll();
		}

		[HarmonyPatch(typeof(WorldOrb), "SetupOrb")]
		class WorldOrb_SetupOrb_Patch {
			public static void Postfix(WorldOrb __instance) {
				if (!Config.GetValue(modEnabled)) { return; }
				__instance.Slot.GetComponent<Grabbable>().Scalable.Value = true;
			}
		}
	}
}