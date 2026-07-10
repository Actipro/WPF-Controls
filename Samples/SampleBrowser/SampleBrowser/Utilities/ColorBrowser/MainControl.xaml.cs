using System.Windows.Interop;

namespace ActiproSoftware.SampleBrowser.Utilities.ColorBrowser;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	public static readonly RoutedCommand CopyName = new(nameof(CopyName), typeof(MainControl));
	public static readonly RoutedCommand CopyStaticResourceBrush = new(nameof(CopyStaticResourceBrush), typeof(MainControl));
	public static readonly RoutedCommand CopyStaticResourceColor = new(nameof(CopyStaticResourceColor), typeof(MainControl));

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		// Register class command bindings
		CommandManager.RegisterClassCommandBinding(typeof(ListBox), new CommandBinding(ApplicationCommands.Copy, OnCopyExecuted, OnCopyCanExecute));
		CommandManager.RegisterClassCommandBinding(typeof(ListBox), new CommandBinding(CopyName, OnCopyNameExecuted, OnCopyCanExecute));
		CommandManager.RegisterClassCommandBinding(typeof(ListBox), new CommandBinding(CopyStaticResourceBrush, OnCopyStaticResourceBrushExecuted, OnCopyCanExecute));
		CommandManager.RegisterClassCommandBinding(typeof(ListBox), new CommandBinding(CopyStaticResourceColor, OnCopyStaticResourceColorExecuted, OnCopyCanExecute));
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private static void OnCopyCanExecute(object sender, CanExecuteRoutedEventArgs e) {
		if ((sender is ListBox listBox) && (listBox.SelectedIndex != -1)) {
			e.CanExecute = true;
			e.Handled = true;
		}
	}

	private static void OnCopyExecuted(object sender, ExecutedRoutedEventArgs e) {
		if ((sender is ListBox listBox) && (listBox.SelectedIndex != -1)) {
			if (listBox.SelectedItem is NamedColor namedColor) {
				Clipboard.SetText(namedColor.Color.ToString());
				e.Handled = true;
			}
		}
	}

	private static void OnCopyNameExecuted(object sender, ExecutedRoutedEventArgs e) {
		if ((sender is ListBox listBox) && (listBox.SelectedIndex != -1)) {
			if (listBox.SelectedItem is NamedColor namedColor) {
				Clipboard.SetText(namedColor.Name);
				e.Handled = true;
			}
		}
	}

	private static void OnCopyStaticResourceBrushExecuted(object sender, ExecutedRoutedEventArgs e) {
		if ((sender is ListBox listBox) && (listBox.SelectedIndex != -1)) {
			if (listBox.SelectedItem is NamedColor { IsSystemColor: true } namedColor) {
				Clipboard.SetText(string.Format("{{StaticResource {{x:Static SystemColors.{0}BrushKey}}}}", namedColor.Name));
				e.Handled = true;
			}
		}
	}

	/// <summary>
	/// Occurs when the <see cref="RoutedCommand"/> is executed.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private static void OnCopyStaticResourceColorExecuted(object sender, ExecutedRoutedEventArgs e) {
		if ((sender is ListBox listBox) && (listBox.SelectedIndex != -1)) {
			if (listBox.SelectedItem is NamedColor { IsSystemColor: true } namedColor) {
				Clipboard.SetText(string.Format("{{StaticResource {{x:Static SystemColors.{0}ColorKey}}}}", namedColor.Name));
				e.Handled = true;
			}
		}
	}
}
