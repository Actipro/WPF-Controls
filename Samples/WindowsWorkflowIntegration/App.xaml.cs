using System.Windows;
using ActiproSoftware.Windows.Themes;

namespace ActiproSoftware.Windows.WindowsWorkflowIntegration {

	/// <summary>
	/// Represents the application.
	/// </summary>
	public partial class App : Application {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Occurs when the application is starting up.
		/// </summary>
		/// <param name="e">A <see cref="StartupEventArgs"/> that contains data related to this event.</param>
		protected override void OnStartup(StartupEventArgs e) {
			// Configure Actipro themes for native controls and set an Office theme that is similar to Word
			ThemeManager.BeginUpdate();
			try {
				ThemeManager.AreNativeThemesEnabled = true;
				ThemeManager.CurrentTheme = ThemeNames.OfficeColorfulIndigo;
			}
			finally {
				ThemeManager.EndUpdate();
			}

			// Call the base method
			base.OnStartup(e);
		}

	}

}
