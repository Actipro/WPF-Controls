using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Input;
using Microsoft.Win32;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.ApplicationMenu {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl : INotifyPropertyChanged {

		private ICommand	notImplementedCommand;
		private bool		useLargeSize = true;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// EVENTS
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public event PropertyChangedEventHandler PropertyChanged;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			this.DataContext = this;

			// Configure command bindings
			this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Open, OnOpenExecute));
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Raises the <see cref="PropertyChanged"/> event.
		/// </summary>
		/// <param name="propertyName">The name of the changed property.</param>
		private void NotifyPropertyChanged(string propertyName) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		/// <summary>
		/// Occurs when the <see cref="ApplicationCommands.Open"/> RoutedCommand is executed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="ExecutedRoutedEventArgs"/> that contains the event data.</param>
		private void OnOpenExecute(object sender, ExecutedRoutedEventArgs e) {
			e.Handled = true;

			if (e.Parameter is string recentFileName) {
				// Open recent file
				ThemedMessageBox.Show($"Here is where you would open the file '{recentFileName}'.", "Open File", MessageBoxButton.OK, MessageBoxImage.Information);
				return;
			}

			// Show the open file dialog
			var dialog = new OpenFileDialog() {
				CheckFileExists = true,
				Filter = "All Files (*.*)|*.*"
			};
			if (dialog.ShowDialog() == true) {
				var fileInfo = new FileInfo(dialog.FileName);
				ThemedMessageBox.Show($"Here is where you would open the file '{fileInfo.Name}'.", "Open File", MessageBoxButton.OK, MessageBoxImage.Information);
			}

		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the command to be invoked for buttons that do not have an implementation.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand NotImplementedCommand {
			get {
				if (notImplementedCommand is null) {
					notImplementedCommand = new DelegateCommand<object>(param => {
						ThemedMessageBox.Show(
							"This control is for user interface demonstration purposes only and no application functionality has been implemented for it.", "Not Implemented",
							MessageBoxButton.OK, MessageBoxImage.Information);
					});
				}
				return notImplementedCommand;
			}
		}

		/// <summary>
		/// Gets or sets if most menu items will use a large size.
		/// </summary>
		/// <value><c>true</c> to use a large size; otherwise <c>false</c>.</value>
		public bool UseLargeSize {
			get => useLargeSize;
			set {
				if (useLargeSize != value) {
					useLargeSize = value;
					NotifyPropertyChanged(nameof(UseLargeSize));
				}
			}
		}

	}
}