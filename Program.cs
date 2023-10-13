	using System.Diagnostics;

	namespace Mod_Installer {
	internal static class Program {
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args) {

//#if DEBUG
//			var pr = Process.GetProcessesByName("oriDE");

//			if (pr.Length > 0) {
//				pr[0].Kill();
//				pr[0].WaitForExit();
//			}


//			Installer.InstallAt(args[0]);

//			Process.Start(Path.Combine(args[0], "oriDE.exe"));


//			return;
//#endif

			// To customize application configuration such as set high DPI settings or default font,
			// see https://aka.ms/applicationconfiguration.
			ApplicationConfiguration.Initialize();
			Application.Run(new Form1());
		}
	}

}