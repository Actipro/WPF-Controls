using ActiproSoftware.Windows.Input;
using Microsoft.Win32;
using System.Runtime.CompilerServices;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.ApplicationMenu;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl : INotifyPropertyChanged {

	private ICommand? _notImplementedCommand;
	private bool _useLargeSize = true;

	// --------------------------------------------------------------------------------------------------
	// EVENTS
	// --------------------------------------------------------------------------------------------------

	public event PropertyChangedEventHandler? PropertyChanged;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		DataContext = this;

		// Configure command bindings
		CommandBindings.Add(new CommandBinding(ApplicationCommands.Open, OnOpenExecute));
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Occurs when the <see cref="ApplicationCommands.Open"/> RoutedCommand is executed.
	/// </summary>
	private void OnOpenExecute(object sender, ExecutedRoutedEventArgs e) {
		e.Handled = true;

		if (e.Parameter is string recentFileName) {
			// Open recent file
			MessageBox.Show($"Here is where you would open the file '{recentFileName}'.", "Open File", MessageBoxButton.OK, MessageBoxImage.Information);
			return;
		}

		// Show the open file dialog
		var dialog = new OpenFileDialog() {
			CheckFileExists = true,
			Filter = "All Files (*.*)|*.*"
		};
		if (dialog.ShowDialog() == true) {
			var fileInfo = new FileInfo(dialog.FileName);
			MessageBox.Show($"Here is where you would open the file '{fileInfo.Name}'.", "Open File", MessageBoxButton.OK, MessageBoxImage.Information);
		}

	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The command to be invoked for buttons that do not have an implementation.
	/// </summary>
	public ICommand NotImplementedCommand {
		get => _notImplementedCommand ??= new DelegateCommand<object>(_ => {
			MessageBox.Show(
				"This control is for user interface demonstration purposes only and no application functionality has been implemented for it.", "Not Implemented",
				MessageBoxButton.OK, MessageBoxImage.Information);
		});
	}

	/// <summary>
	/// Raises the <see cref="PropertyChanged"/> event.
	/// </summary>
	/// <param name="propertyName">(Optional) The name of the property that changed.</param>
	protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
		=> OnPropertyChanged(new PropertyChangedEventArgs(propertyName));

	/// <summary>
	/// Raises the <see cref="PropertyChanged"/> event.
	/// </summary>
	/// <param name="e">The event data.</param>
	protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		=> PropertyChanged?.Invoke(this, e);

	/// <summary>
	/// Called from a property setter to change the backing field's value and raise
	/// <see cref="PropertyChanged"/> notification events if the new value is not equal to the current value.
	/// </summary>
	/// <typeparam name="T">The type of the property that changed.</typeparam>
	/// <param name="field">The backing field that holds the property's value, which may be updated.</param>
	/// <param name="newValue">The new property value.</param>
	/// <param name="propertyName">(Optional) The name of the property that changed.</param>
	/// <returns>
	/// <c>true</c> if the property was changed; otherwise, <c>false</c>.
	/// </returns>
	protected bool SetProperty<T>(
		#if NET
		[NotNullIfNotNull(nameof(newValue))]
		#endif
		ref T field, T newValue, [CallerMemberName] string? propertyName = null) {

		if (!EqualityComparer<T>.Default.Equals(field, newValue)) {
			field = newValue;
			OnPropertyChanged(propertyName);
			return true;
		}

		return false;
	}

	/// <summary>
	/// Indicates if most menu items will use a large size.
	/// </summary>
	public bool UseLargeSize {
		get => _useLargeSize;
		set => SetProperty(ref _useLargeSize, value);
	}

}
