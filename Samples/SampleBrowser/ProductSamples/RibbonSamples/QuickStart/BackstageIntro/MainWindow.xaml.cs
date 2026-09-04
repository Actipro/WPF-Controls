using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Controls.Ribbon;
using ActiproSoftware.Windows.DocumentManagement;
using ActiproSoftware.ProductSamples.RibbonSamples.Common;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.RibbonSamples.QuickStart.BackstageIntro;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainWindow : RibbonWindow {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainWindow() {
		InitializeComponent();

		// Populate some sample recent documents
		DocumentReferenceGenerator.BindRecentDocumentManager(recentDocManager);


		// Add command bindings
		CommandBindings.Add(new CommandBinding(ApplicationCommands.Open, OnOpenExecute));
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Occurs when the application menu opens or closes.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnIsApplicationMenuOpenChanged(object sender, BooleanPropertyChangedRoutedEventArgs e) {
		// If opening, ensure the that the New is always selected
		if (ribbon.IsApplicationMenuOpen)
			appMenu.SelectedItem = newBackstageTab;
	}

	private void OnOpenExecute(object sender, ExecutedRoutedEventArgs e) {
		if (e.Parameter is IDocumentReference documentReference) {
			// Process recent document clicks
			MessageBox.Show($"Open document '{documentReference.Name}' here.", "Open Recent Document", MessageBoxButton.OK, MessageBoxImage.Information);
		}
	}

}
