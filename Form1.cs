namespace Mod_Installer {
	public partial class Form1 : Form {
		public Form1() {
			InitializeComponent();
		}

		private void installButton_Click(object sender, EventArgs e) {
			var d = new OpenFileDialog();

			if (d.ShowDialog() == DialogResult.OK) {

				var file = d.FileName;
				if (Path.GetFileName(file) != "oriDE.exe") {
					file = Path.Combine(Path.GetDirectoryName(file)!, "oriDE.exe");
				}

				if (File.Exists(file)) {
					foreach (Control control in Controls) {
						control.Enabled = false;
					}
					try {

						Installer.InstallAt(Path.GetDirectoryName(file)!);

						MessageBox.Show("Congrats, the mod loader was successfully installed!", "Ori Mod Loader", MessageBoxButtons.OK, MessageBoxIcon.None);
					}
					finally {
						foreach (Control control in Controls) {
							control.Enabled = true;
						}
					}

				}
				else {
					MessageBox.Show("Please select the .exe file for Ori 1.", "Ori Mod Loader", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}

			d.Dispose();

		}
	}
}