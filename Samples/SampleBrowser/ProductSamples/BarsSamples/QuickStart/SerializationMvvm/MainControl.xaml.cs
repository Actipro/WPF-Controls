using ActiproSoftware.ProductSamples.SharedSamples.Common;
using ActiproSoftware.Windows.Controls.Bars;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using ActiproSoftware.Windows.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.SerializationMvvm {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl : ISampleCommands {

		private string originalLayout;

		private readonly SampleBarManager barManager;

		private ICommand restoreLayoutCommand;
		private ICommand restoreOriginalCommand;
		private ICommand saveLayoutCommand;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// INTERFACE IMPLEMENTATION
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc/>
		ICommand ISampleCommands.RestoreLayout {
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

		/// <inheritdoc/>
		ICommand ISampleCommands.RestoreOriginalLayout {
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

		/// <inheritdoc/>
		ICommand ISampleCommands.SaveLayout {
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

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			// Initialize the bar manager for working with Ribbon view models
			this.barManager = new SampleBarManager(this);

			// Initialize the view models for the Ribbon
			InitializeRibbonViewModels();

			// Bind the view to itself since we do not define an explicit view model
			this.DataContext = this;

			// Try to cache the original layout so it can be restored
			this.Loaded += (s, e) => {
				if ((originalLayout is null) && TrySaveLayout(out var layout)) {
					originalLayout = layout;
					this.CurrentLayout = layout;
				}
			};
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes the view models used to define the Ribbon interface.
		/// </summary>
		private void InitializeRibbonViewModels() {
			// Configure Tabs
			RibbonTabs = new ObservableCollection<RibbonTabViewModel>() {
				new RibbonTabViewModel("Home") {
					Groups = {
						new RibbonGroupViewModel("Serialization") {
							Items = {
								new RibbonControlGroupViewModel() {
									ItemVariantBehavior = ItemVariantBehavior.AlwaysLarge,
									SeparatorMode = RibbonControlGroupSeparatorMode.Always,
									Items = {
										barManager.ControlViewModels[SampleBarControlKeys.SaveLayout],
										barManager.ControlViewModels[SampleBarControlKeys.RestoreLayout]
									}
								},
								barManager.ControlViewModels[SampleBarControlKeys.RestoreOriginalLayout],
							}
						}
					}
				},
			};

			// Configure Quick Access Toolbar
			var cutButtonViewModel = barManager.ControlViewModels[SampleBarControlKeys.Cut];
			var copyButtonViewModel = barManager.ControlViewModels[SampleBarControlKeys.Copy];
			var pasteButtonViewModel = barManager.ControlViewModels[SampleBarControlKeys.Paste];
			QuickAccessToolBarActiveItems = new ObservableCollection<object>() { cutButtonViewModel, copyButtonViewModel, pasteButtonViewModel };
			QuickAccessToolBarCommonItems = new object[] { cutButtonViewModel, copyButtonViewModel, pasteButtonViewModel };

		}

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

				// Attempt to resolve any unspecified items by looking up the key in the bar manager
				if (e.Item is null)
					e.Item = barManager?.ControlViewModels[e.Key];

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
		/// Gets or sets the items which are actively displayed in the Quick Access Toolbar.
		/// </summary>
		/// <value>An <see cref="ObservableCollection{T}"/> of objects.</value>
		public ObservableCollection<object> QuickAccessToolBarActiveItems { get; private set; }

		/// <summary>
		/// Gets or sets the items which are commonly displayed in the Quick Access Toolbar.
		/// </summary>
		/// <value>An <see cref="IEnumerable{T}"/> of objects.</value>
		public IEnumerable<object> QuickAccessToolBarCommonItems { get; private set; }

		/// <summary>
		/// Gets or sets the view models for the tabs displayed in the ribbon.
		/// </summary>
		/// <value>An <see cref="ObservableCollection{T}"/> of type <see cref="RibbonTabViewModel"/>.</value>
		public ObservableCollection<RibbonTabViewModel> RibbonTabs { get; private set; }

	}
}