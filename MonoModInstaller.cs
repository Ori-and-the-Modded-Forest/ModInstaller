using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mod_Installer {
	public static class Installer {

		public static void InstallAt(string path) {

			if (!Directory.Exists(path) || !File.Exists(Path.Combine(path, "oriDE_Data", "Managed", "Assembly-CSharp.dll")))
				return;

			Directory.CreateDirectory(Path.Combine(path, "Mods"));

			string dllPath = Path.Combine(path, "oriDE_Data", "Managed");

			if (!Directory.Exists(Path.Combine(dllPath, "orig")) || !File.Exists(Path.Combine(dllPath, "orig", "Assembly-CSharp.dll"))) {

				Directory.CreateDirectory(Path.Combine(dllPath, "orig"));
				File.Copy(Path.Combine(dllPath, "Assembly-CSharp.dll"), Path.Combine(dllPath, "orig", "Assembly-CSharp.dll"), true);
			}

			// Copy vanilla code
			File.Copy(Path.Combine(dllPath, "orig", "Assembly-CSharp.dll"), Path.Combine(dllPath, "Assembly-CSharp.dll"), true);

			//Directory.CreateDirectory(Path.Combine(dllPath, "MonoMod"));

			//File.Copy($"MonoMod/Assembly-CSharp.ModBase.mm.dll", Path.Combine(dllPath, "Assembly-CSharp.ModBase.mm.dll"), true);
			//File.Copy($"MonoMod/MonoMod.exe", Path.Combine(dllPath, "MonoMod.exe"), true);

			foreach (var file in Directory.EnumerateFiles("MonoMod")) {
				File.Copy(file, Path.Combine(dllPath, Path.GetFileName(file)), true);
			}


			ProcessStartInfo info = new ProcessStartInfo("cmd");
			//info.RedirectStandardOutput = true;
			info.RedirectStandardInput = true;
			info.Domain = dllPath + "\\";// Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\";

			using (Process process = new Process()) {

				process.StartInfo = info;
				process.Start();

				using (StreamWriter sw = process.StandardInput) {
					sw.WriteLine($"\"{dllPath}/MonoMod.exe\" \"{dllPath}/Assembly-CSharp.dll\"");
				}

				process.WaitForExit();
			}

			if (File.Exists($"{dllPath}/MONOMODDED_Assembly-CSharp.dll")) {
				File.Copy($"{dllPath}/MONOMODDED_Assembly-CSharp.dll", $"{dllPath}/Assembly-CSharp.dll", true);
				File.Copy($"MonoMod/YamlDotNet.dll", $"{dllPath}/YamlDotNet.dll", true);
				File.Delete($"{dllPath}/MONOMODDED_Assembly-CSharp.dll");
				File.Delete($"{dllPath}/MONOMODDED_Assembly-CSharp.pdb");
				File.Delete($"{dllPath}/Assembly-CSharp.mm.dll");
				File.Delete($"{dllPath}/Assembly-CSharp.mm.pdb");
			}

			using (Process process = new Process()) {

				process.StartInfo = info;
				process.Start();

				using (StreamWriter sw = process.StandardInput) {
					sw.WriteLine($"\"{dllPath}/MonoMod.RuntimeDetour.HookGen.exe\" \"{dllPath}/Assembly-CSharp.dll\"");
				}
				process.WaitForExit();

			}

		}
	}
}
