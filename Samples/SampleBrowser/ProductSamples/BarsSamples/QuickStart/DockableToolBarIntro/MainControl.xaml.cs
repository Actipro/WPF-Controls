using ActiproSoftware.ProductSamples.BarsSamples.Common;
using ActiproSoftware.Text.Implementation;
using ActiproSoftware.Text.Languages.Xml.Implementation;
using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Controls.Bars;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Extensions;
using ActiproSoftware.Windows.Input;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;
using UIDensity = ActiproSoftware.Windows.Themes.UserInterfaceDensity;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.DockableToolBarIntro;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	private string? _originalLayout;
	private ICommand? _restoreLayoutCommand;
	private ICommand? _restoreOriginalCommand;
	private ICommand? _saveLayoutCommand;
	private UIDensity _spacingDensity = UIDensity.Normal;
	private ICommand? _toggleCanToolBarsFloatCommand;
	private ICommand? _toggleToolBarsHaveGrippersCommand;
	private ICommand? _toggleToolBarsHaveOptionsButtonsCommand;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		// Bind the view to itself since we do not define an explicit view model
		DataContext = this;

		//
		// Configure the dockable toolbar host
		//

		var barManager = new BarManager();

		// Reset any collapse behaviors intended for a Simplified Ribbon
		foreach (var controlViewModel in barManager.ControlViewModels) {
			switch (controlViewModel) {
				case BarButtonViewModel buttonViewModel:
					buttonViewModel.ToolBarItemCollapseBehavior = ItemCollapseBehavior.Default;
					break;
				case BarPopupButtonViewModel popupButtonViewModel:
					popupButtonViewModel.ToolBarItemCollapseBehavior = ItemCollapseBehavior.Default;
					break;
			}
		}

		var itemContainerTemplateSelector = new BarControlTemplateSelector();

		DockableToolBarHostViewModel = CreateDockableToolBarHostViewModel(barManager);
		DockableToolBarHostViewModel.ItemContainerTemplateSelector = itemContainerTemplateSelector;

		MainMenuViewModel = CreateBarMainMenuViewModel(barManager);
		MainMenuViewModel.ItemContainerTemplateSelector = itemContainerTemplateSelector;

		var document = SampleViewModelFactory.CreateFlowDocument(
			barManager,
			"This is an editor application sample that demonstrates multiple dockable toolbars."
		);
		SelectedDocument = new RichTextEditorDocumentViewModel(barManager, document) {
			ItemContainerTemplateSelector = itemContainerTemplateSelector,
		};
		SelectedDocument.RegisterCommands();

		// Try to cache the original layout so it can be restored
		Loaded += (s, e) => {
			if ((_originalLayout is null) && TrySaveLayout(out var layout)) {
				_originalLayout = layout;
				CurrentLayout = layout;
			}
		};
	}

	// --------------------------------------------------------------------------------------------------
	// PRIVATE PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Creates a <see cref="BarMainMenuViewModel"/>.
	/// </summary>
	/// <param name="barManager">The associated <see cref="BarManager"/>.</param>
	private BarMainMenuViewModel CreateBarMainMenuViewModel(BarManager barManager) {
		return new BarMainMenuViewModel() {
			Items = {
				new BarPopupButtonViewModel("File") {
					MenuItems = {
						barManager.ControlViewModels[BarControlKeys.New],
						barManager.ControlViewModels[BarControlKeys.Open],
						barManager.ControlViewModels[BarControlKeys.Save],
						new BarSeparatorViewModel(),
						new BarButtonViewModel("Exit", barManager.NotImplementedCommand),
					}
				},
				new BarPopupButtonViewModel("Edit") {
					MenuItems = {
						barManager.ControlViewModels[BarControlKeys.Cut],
						barManager.ControlViewModels[BarControlKeys.Copy],
						barManager.ControlViewModels[BarControlKeys.Paste],
						new BarSeparatorViewModel(),
						barManager.ControlViewModels[BarControlKeys.SelectAll],
					}
				},
				new BarPopupButtonViewModel("View") {
					MenuItems = {
						new BarToggleButtonViewModel("AllowFloatingToolBars", "Can Toolbars Float", "F", ToggleCanToolBarsFloatCommand) { IsChecked = true },
						new BarSeparatorViewModel(),
						new BarToggleButtonViewModel("ToolBarsHaveGrippers", "Toolbars Have Grippers", "G", ToggleToolBarsHaveGrippersCommand) { IsChecked = true },
						new BarToggleButtonViewModel("ToolBarsHaveOptionsButtons", "Toolbars Have Options Buttons", "B", ToggleToolBarsHaveOptionsButtonsCommand) { IsChecked = true },
						new BarSeparatorViewModel(),
						new BarPopupButtonViewModel("Spacing") {
							MenuItems = {
								new BarToggleButtonViewModel(UIDensity.Compact.ToString(), "None",
									new DelegateCommand<int>(number => { SpacingDensity = UIDensity.Compact; })) {
								},
								new BarToggleButtonViewModel(UIDensity.Normal.ToString(), "Default",
									new DelegateCommand<int>(number => { SpacingDensity = UIDensity.Normal; })) {
									IsChecked = true
								},
								new BarToggleButtonViewModel(UIDensity.Spacious.ToString(), "Extra",
									new DelegateCommand<int>(number => { SpacingDensity = UIDensity.Spacious; })) {
								},
							}
						},
						new BarSeparatorViewModel(),
						new BarPopupButtonViewModel("Serialization Data Options") {
							MenuItems = {
								new BarToggleButtonViewModel("SerializationOptionPlacement", "Placement",
									new DelegateCommand<object>(_ => { OptionsViewModel.Placement = !OptionsViewModel.Placement; })) {
									IsChecked = true,
									StaysOpenOnClick = true
								},
								new BarToggleButtonViewModel("SerializationOptionLineIndex", "LineIndex",
									new DelegateCommand<object>(_ => { OptionsViewModel.LineIndex = !OptionsViewModel.LineIndex; })) {
									IsChecked = true,
									StaysOpenOnClick = true
								},
								new BarToggleButtonViewModel("SerializationOptionSortOrder", "SortOrder",
									new DelegateCommand<object>(_ => { OptionsViewModel.SortOrder = !OptionsViewModel.SortOrder; })) {
									IsChecked = true,
									StaysOpenOnClick = true
								},
								new BarToggleButtonViewModel("SerializationOptionOffset", "Offset",
									new DelegateCommand<object>(_ => { OptionsViewModel.Offset = !OptionsViewModel.Offset; })) {
									IsChecked = true,
									StaysOpenOnClick = true
								},
								new BarToggleButtonViewModel("SerializationOptionIsFloating", "IsFloating",
									new DelegateCommand<object>(_ => { OptionsViewModel.IsFloating = !OptionsViewModel.IsFloating; })) {
									IsChecked = true,
									StaysOpenOnClick = true
								},
								new BarToggleButtonViewModel("SerializationOptionFloatingLocation", "FloatingLocation",
									new DelegateCommand<object>(_ => { OptionsViewModel.FloatingLocation = !OptionsViewModel.FloatingLocation; })) {
									IsChecked = true,
									StaysOpenOnClick = true
								},
								new BarToggleButtonViewModel("SerializationOptionIsVisible", "IsVisible",
									new DelegateCommand<object>(_ => { OptionsViewModel.IsVisible = !OptionsViewModel.IsVisible; })) {
									IsChecked = true,
									StaysOpenOnClick = true
								},
							}
						},
						new BarButtonViewModel("SaveLayout", "Save Layout", SaveLayoutCommand),
						new BarButtonViewModel("LoadLayout", "Load Layout", RestoreLayoutCommand),
						new BarButtonViewModel("LoadOriginalLayout", "Load Original Layout", "O", RestoreOriginalCommand),
					}
				},
				new BarPopupButtonViewModel("Help") {
					MenuItems = {
						new BarButtonViewModel("About", "About Actipro Bars...", barManager.NotImplementedCommand)
					}
				},
			}
		};
	}

	/// <summary>
	/// Creates a <see cref="DockableToolBarHostViewModel"/>.
	/// </summary>
	/// <param name="barManager">The associated <see cref="BarManager"/>.</param>
	private static DockableToolBarHostViewModel CreateDockableToolBarHostViewModel(BarManager barManager) {
		return new DockableToolBarHostViewModel() {
			// Designate the least-used controls to collapse in priority order across all toolbars
			ControlVariants = [
				new VariantSet() {
					SizeVariants = {
						new SizeVariant(BarControlKeys.Replace, VariantSize.Small),
						new SizeVariant(BarControlKeys.Find, VariantSize.Small),
					}
				},
				new SizeVariant(BarControlKeys.FormatPainter, VariantSize.Collapsed),
				new VariantSet() {
					SizeVariants = {
						new SizeVariant(BarControlKeys.Sort, VariantSize.Collapsed),
						new SizeVariant(BarControlKeys.ShowSymbols, VariantSize.Collapsed)
					}
				},
				new VariantSet() {
					SizeVariants = {
						new SizeVariant(BarControlKeys.IncreaseFontSize, VariantSize.Collapsed),
						new SizeVariant(BarControlKeys.DecreaseFontSize, VariantSize.Collapsed)
					}
				},
				new VariantSet() {
					SizeVariants = {
						new SizeVariant(BarControlKeys.Shading, VariantSize.Collapsed),
						new SizeVariant(BarControlKeys.Borders, VariantSize.Collapsed)
					}
				},
				new VariantSet() {
					SizeVariants = {
						new SizeVariant(BarControlKeys.Strikethrough, VariantSize.Collapsed),
						new SizeVariant(BarControlKeys.Subscript, VariantSize.Collapsed),
						new SizeVariant(BarControlKeys.Superscript, VariantSize.Collapsed)
					}
				},
				new VariantSet() {
					SizeVariants = {
						new SizeVariant(BarControlKeys.Font, VariantSize.Collapsed),
						new SizeVariant(BarControlKeys.FontSize, VariantSize.Collapsed)
					}
				},
				new VariantSet() {
					SizeVariants = {
						new SizeVariant(BarControlKeys.AlignLeft, VariantSize.Collapsed),
						new SizeVariant(BarControlKeys.AlignCenter, VariantSize.Collapsed),
						new SizeVariant(BarControlKeys.AlignRight, VariantSize.Collapsed),
						new SizeVariant(BarControlKeys.AlignJustify, VariantSize.Collapsed)
					}
				},
			],

			ToolBars = {
				new DockableToolBarViewModel("Standard") {
					Items = {
						barManager.ControlViewModels[BarControlKeys.New],
						barManager.ControlViewModels[BarControlKeys.Open],
						barManager.ControlViewModels[BarControlKeys.Save],
						new BarSeparatorViewModel(),
						barManager.ControlViewModels[BarControlKeys.Undo],
						barManager.ControlViewModels[BarControlKeys.Redo],
						new BarSeparatorViewModel(),
						barManager.ControlViewModels[BarControlKeys.Cut],
						barManager.ControlViewModels[BarControlKeys.Copy],
						barManager.ControlViewModels[BarControlKeys.PasteMenu],
						new BarSeparatorViewModel(),
						barManager.ControlViewModels[BarControlKeys.FormatPainter],
					}
				},
				new DockableToolBarViewModel("Paragraph") {
					Items = {
						barManager.ControlViewModels[BarControlKeys.Bullets],
						barManager.ControlViewModels[BarControlKeys.Numbering],
						new BarSeparatorViewModel(),
						barManager.ControlViewModels[BarControlKeys.DecreaseIndent],
						barManager.ControlViewModels[BarControlKeys.IncreaseIndent],
						new BarSeparatorViewModel(),
						barManager.ControlViewModels[BarControlKeys.Sort],
						barManager.ControlViewModels[BarControlKeys.ShowSymbols],
						new BarSeparatorViewModel(),
						barManager.ControlViewModels[BarControlKeys.AlignLeft],
						barManager.ControlViewModels[BarControlKeys.AlignCenter],
						barManager.ControlViewModels[BarControlKeys.AlignRight],
						barManager.ControlViewModels[BarControlKeys.AlignJustify],
						new BarSeparatorViewModel(),
						barManager.ControlViewModels[BarControlKeys.Shading],
						barManager.ControlViewModels[BarControlKeys.Borders],
					}
				},
				new DockableToolBarViewModel("Font") {
					LineIndex = 1,
					Items = {
						barManager.ControlViewModels[BarControlKeys.Font],
						barManager.ControlViewModels[BarControlKeys.FontSize],
						barManager.ControlViewModels[BarControlKeys.Bold],
						barManager.ControlViewModels[BarControlKeys.Italic],
						barManager.ControlViewModels[BarControlKeys.Underline],
						new BarSeparatorViewModel(),
						barManager.ControlViewModels[BarControlKeys.Strikethrough],
						barManager.ControlViewModels[BarControlKeys.Subscript],
						barManager.ControlViewModels[BarControlKeys.Superscript],
						new BarSeparatorViewModel(),
						barManager.ControlViewModels[BarControlKeys.IncreaseFontSize],
						barManager.ControlViewModels[BarControlKeys.DecreaseFontSize],
						new BarSeparatorViewModel(),
						barManager.ControlViewModels[BarControlKeys.TextHighlightColor],
						barManager.ControlViewModels[BarControlKeys.FontColor],
					}
				},
				new DockableToolBarViewModel("Search") {
					LineIndex = 1,
					Items = {
						barManager.ControlViewModels[BarControlKeys.Find],
						barManager.ControlViewModels[BarControlKeys.Replace],
					}
				}
			}
		};
	}

	/// <summary>
	/// Tries to restore the specified layout data to the dockable toolbars.
	/// </summary>
	/// <param name="xmlLayout">The XML layout data.</param>
	/// <returns><c>true</c> if the layout was successfully restored; otherwise <c>false</c>.</returns>
	private bool TryRestoreLayout(string xmlLayout) {
		try {
			// Initialize the options that will be supported during restore based on current settings
			var options = OptionsViewModel.CreateOptions();

			// Deserialize the layout to the host
			new DockableToolBarSerializer().Deserialize(toolBarHost, xmlLayout, options);

			// Indicate success
			return true;
		}
		catch (Exception ex) {
			// Exception during the deserialization process
			Debug.WriteLine(ex);
			UserPromptBuilder.Configure().ForException(ex, "Error restoring layout.").Show();

			// Indicate failure to restore
			return false;
		}
	}

	/// <summary>
	/// Tries to save the current dockable toolbar layout.
	/// </summary>
	/// <param name="layout">When successful, outputs the layout data.</param>
	/// <returns><c>true</c> if the layout was successfully saved; otherwise <c>false</c>.</returns>
	private bool TrySaveLayout(out string? layout) {
		try {
			// Initialize the options that will be supported during save based on current settings
			var options = OptionsViewModel.CreateOptions();

			// Serialize the layout from the host
			layout = new DockableToolBarSerializer().Serialize(toolBarHost, options);

			// Indicate success
			return true;
		}
		catch (Exception ex) {
			// Exception during the serialization process
			Debug.WriteLine(ex);
			UserPromptBuilder.Configure().ForException(ex, "Error saving layout.").Show();

			// Indicate failure
			layout = null;
			return false;
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The current layout data.
	/// </summary>
	/// <value>An XML-formatted string.</value>
	public string? CurrentLayout { get; set; }

	/// <summary>
	/// The view model for the dockable toolbar host.
	/// </summary>
	public DockableToolBarHostViewModel DockableToolBarHostViewModel { get; }

	/// <summary>
	/// The view model for the main menu.
	/// </summary>
	public BarMainMenuViewModel MainMenuViewModel { get; }

	/// <summary>
	/// The view model for controlling which options are included when serializing and deserialization.
	/// </summary>
	/// <value>A <see cref="SerializerOptionsViewModel"/>.</value>
	public SerializerOptionsViewModel OptionsViewModel { get; } = new SerializerOptionsViewModel();

	/// <summary>
	/// The command to deserialize the layout.
	/// </summary>
	public ICommand RestoreLayoutCommand {
		get => _restoreLayoutCommand ??= new DelegateCommand<object>(_ => {
			var currentLayout = CurrentLayout;
			if (string.IsNullOrEmpty(currentLayout)) {
				MessageBox.Show("The current layout is undefined and cannot be restored.  Please save the current layout first.", "Restore Layout", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			// Attempt to restore the current layout
			TryRestoreLayout(currentLayout!);
		});
	}

	/// <summary>
	/// The command to deserialize the original layout.
	/// </summary>
	public ICommand RestoreOriginalCommand {
		get => _restoreOriginalCommand ??= new DelegateCommand<object>(_ => {
			if (string.IsNullOrEmpty(_originalLayout)) {
				MessageBox.Show("The original layout is undefined and cannot be restored.", "Restore", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			// Attempt to restore the original layout
			TryRestoreLayout(_originalLayout!);
		});
	}

	/// <summary>
	/// The view model for the selected document.
	/// </summary>
	public RichTextEditorDocumentViewModel SelectedDocument { get; private set; }

	/// <summary>
	/// A predefined spacing configuration.
	/// </summary>
	public UIDensity SpacingDensity {
		get => _spacingDensity;
		set {
			if (_spacingDensity != value) {
				_spacingDensity = value;

				switch (_spacingDensity) {
					case UIDensity.Compact:
						toolBarHost.OuterPadding = new Thickness(0);
						toolBarHost.LineSpacing = 0;
						toolBarHost.ToolBarSpacing = 0;
						toolBarHost.ToolBarItemSpacing = 0;
						toolBarHost.Padding = new Thickness(0);
						break;
					case UIDensity.Spacious:
						toolBarHost.OuterPadding = new Thickness(2);
						toolBarHost.LineSpacing = 2;
						toolBarHost.ToolBarSpacing = 2;
						toolBarHost.ToolBarItemSpacing = 2;
						toolBarHost.Padding = new Thickness(2);
						break;
					case UIDensity.Normal:
					default:
						toolBarHost.OuterPadding = new Thickness(1);
						toolBarHost.LineSpacing = 1;
						toolBarHost.ToolBarSpacing = 1;
						toolBarHost.ToolBarItemSpacing = 1;
						toolBarHost.Padding = new Thickness(1);
						break;
				}

				var viewViewModel = MainMenuViewModel.Items.OfType<BarPopupButtonViewModel>().FirstOrDefault(vm => vm.Key == "View");
				var spacingViewModel = viewViewModel?.MenuItems.OfType<BarPopupButtonViewModel>().FirstOrDefault(vm => vm.Key == "Spacing");

				// Update checked states
				if (spacingViewModel is not null) {
					foreach (var menuItem in spacingViewModel.MenuItems) {
						if (menuItem is BarToggleButtonViewModel toggleViewModel)
							toggleViewModel.IsChecked = (toggleViewModel.Key == _spacingDensity.ToString());
					}
				}
			}
		}
	}

	/// <summary>
	/// The command to serialize the layout.
	/// </summary>
	public ICommand SaveLayoutCommand {
		get => _saveLayoutCommand ??= new DelegateCommand<object>(_ => {
			// Attempt to save the current layout
			if (TrySaveLayout(out var layout)) {
				CurrentLayout = layout;

				UserPromptBuilder.Configure()
					.WithTitle("Layout Saved")
					.WithContent(_ => {
						return new SyntaxEditor() {
							Width = 600,
							Height = 400,
							AreLineModificationMarksVisible = false,
							CanScrollPastDocumentEnd = false,
							CanSplitHorizontally = false,
							IsDocumentReadOnly = true,
							IsLineNumberMarginVisible = false,
							IsOutliningMarginVisible = false,
							Document = new EditorDocument() {
								TabSize = 2,
								Language = new XmlSyntaxLanguage(),
							},
							Text = layout ?? string.Empty
						};
					})
					.WithStandardButtons(UserPromptStandardButtons.OK)
					.Show();
			}
		});
	}

	/// <summary>
	/// The command to toggle whether toolbars can float.
	/// </summary>
	public ICommand ToggleCanToolBarsFloatCommand {
		get => _toggleCanToolBarsFloatCommand ??= new DelegateCommand<object>(_ => {
			DockableToolBarHostViewModel.CanToolBarsFloat = !DockableToolBarHostViewModel.CanToolBarsFloat;
		});
	}

	/// <summary>
	/// The command to toggle whether toolbars have grippers.
	/// </summary>
	public ICommand ToggleToolBarsHaveGrippersCommand {
		get => _toggleToolBarsHaveGrippersCommand ??= new DelegateCommand<object>(_ => {
			DockableToolBarHostViewModel.ToolBarsHaveGrippers = !DockableToolBarHostViewModel.ToolBarsHaveGrippers;
		});
	}

	/// <summary>
	/// The command to toggle whether toolbars have options buttons.
	/// </summary>
	public ICommand ToggleToolBarsHaveOptionsButtonsCommand {
		get => _toggleToolBarsHaveOptionsButtonsCommand ??= new DelegateCommand<object>(_ => {
			DockableToolBarHostViewModel.ToolBarsHaveOptionsButtons = !DockableToolBarHostViewModel.ToolBarsHaveOptionsButtons;
		});
	}

}
