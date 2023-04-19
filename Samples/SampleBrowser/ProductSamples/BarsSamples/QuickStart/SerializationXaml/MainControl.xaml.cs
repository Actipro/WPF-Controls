using ActiproSoftware.ProductSamples.SharedSamples.Common;
using ActiproSoftware.Windows.Controls.Bars;
using ActiproSoftware.Windows.Input;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.SerializationXaml {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		private readonly string originalLayout;

		private ICommand restoreLayoutCommand;
		private ICommand restoreOriginalCommand;
		private ICommand saveLayoutCommand;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			// Bind the view to itself since we do not define an explicit view model
			this.DataContext = this;

			// Try to cache the original layout so it can be restored
			if (TrySaveLayout(out var layout)) {
				originalLayout = layout;
				this.CurrentLayout = layout;
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Occurs when an item is being added to the Quick Access Toolbar.
		/// </summary>
		/// <param name="sender">A reference to the <see cref="Ribbon"/>.</param>
		/// <param name="e">The event arguments.</param>
		private void OnQuickAccessToolBarItemAdding(object sender, RibbonQuickAccessToolBarItemAddingEventArgs e) {
			// This event is raised when an item is being added to the Quick Access Toolbar.
			// The event data defines the Key of the item being added and, if found, the Item
			// that will be added. If the Ribbon is unable to automatically locate an item with
			// the desired key, the Item property will be NULL. When this happens, you can manually
			// assign an Item that corresponds to the given Key. If Item is NULL or Cancel is set
			// to TRUE then nothing will be added.
			//
			// This event can also be used to notify a user if an attempt was made to add an item
			// to the Quick Access Toolbar that might no longer be available.
			if (!e.Cancel) {
				Debug.WriteLine($"Adding QAT Item... Key={e.Key}; Item={e.Item?.ToString() ?? "NULL"}");

				if (e.Item is null) {
					MessageBox.Show($"Unable to restore the Quick Access Toolbar item '{e.Key}' because the corresponding command could not be found.", "Command Not Found", MessageBoxButton.OK, MessageBoxImage.Warning);
				}
			}
		}

		/// <summary>
		/// Tries to restore the specified layout data to the Ribbon.
		/// </summary>
		/// <param name="xmlLayout">The XML layout data.</param>
		/// <returns><c>true</c> if the layout was successfully restored; otherwise <c>false</c>.</returns>
		private bool TryRestoreLayout(string xmlLayout) {
			try {
				// Initialize the options that will be supported during restore based on current settings
				var options = this.OptionsModel.CreateOptions();

				// Deserialize the layout to the Ribbon
				var serializer = new RibbonSerializer();
				serializer.Deserialize(ribbon, xmlLayout, options);

				// Indicate success
				return true;
			}
			catch (Exception ex) {
				// Exception during the deserialization process
				Debug.WriteLine(ex);
				UserPromptHelper.ShowException(ex, "Error restoring layout.");

				// Indicate failure to restore
				return false;
			}
		}

		/// <summary>
		/// Tries to save the current Ribbon layout.
		/// </summary>
		/// <param name="layout">When successful, outputs the layout data.</param>
		/// <returns><c>true</c> if the layout was successfully saved; otherwise <c>false</c>.</returns>
		private bool TrySaveLayout(out string layout) {
			try {
				// Initialize the options that will be supported during restore based on current settings
				var options = this.OptionsModel.CreateOptions();

				// Serialize the layout from the Ribbon
				var serializer = new RibbonSerializer();
				layout = serializer.Serialize(ribbon, options);

				// Indicate success
				return true;
			}
			catch (Exception ex) {
				// Exception during the serialization process
				Debug.WriteLine(ex);
				UserPromptHelper.ShowException(ex, "Error saving layout.");

				// Indicate failure
				layout = null;
				return false;
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the current layout data.
		/// </summary>
		/// <value>An XML-formatted string.</value>
		public string CurrentLayout {
			get => xmlDataEditor.Text;
			set => xmlDataEditor.Text = value;
		}

		/// <summary>
		/// Gets the view model for controlling which options are included when serializing and deserialization.
		/// </summary>
		/// <value>A <see cref="SerializerOptionsViewModel"/>.</value>
		public SerializerOptionsViewModel OptionsModel { get; } = new SerializerOptionsViewModel();

		/// <summary>
		/// Gets the command to restore the configured layout.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand RestoreLayoutCommand {
			get {
				if (restoreLayoutCommand is null) {
					restoreLayoutCommand = new DelegateCommand<object>(param => {
						var currentLayout = this.CurrentLayout;
						if (string.IsNullOrEmpty(currentLayout)) {
							MessageBox.Show("The current layout is undefined and cannot be restored.", "Restore", MessageBoxButton.OK, MessageBoxImage.Error);
							return;
						}

						// Attempt to restore the current layout
						TryRestoreLayout(currentLayout);
					});
				}
				return restoreLayoutCommand;
			}
		}

		/// <summary>
		/// Gets the command to restore the original layout.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand RestoreOriginalCommand {
			get {
				if (restoreOriginalCommand is null) {
					restoreOriginalCommand = new DelegateCommand<object>(param => {
						if (originalLayout is null) {
							MessageBox.Show("The original layout is undefined and cannot be restored.", "Restore", MessageBoxButton.OK, MessageBoxImage.Error);
							return;
						}

						// Attempt to restore the original layout
						if (TryRestoreLayout(originalLayout))
							CurrentLayout = originalLayout;
					});
				}
				return restoreOriginalCommand;
			}
		}

		/// <summary>
		/// Gets the command to save the current layout.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand SaveLayoutCommand {
			get {
				if (saveLayoutCommand is null) {
					saveLayoutCommand = new DelegateCommand<object>(param => {
						// Attempt to save the current layout
						if (TrySaveLayout(out var layout))
							this.CurrentLayout = layout;
					});
				}
				return saveLayoutCommand;
			}
		}

	}
}