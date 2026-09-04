/*

RIBBON GETTING STARTED SERIES - STEP 8

STEP SUMMARY:

	The step configures the Backstage to be displayed when the Application Button is pressed.

CHANGES SINCE LAST STEP:

	Added the RibbonBackstageViewModel configuration to RibbonViewModel.

	Prior sample comments were removed/condensed.

*/

using ActiproSoftware.Windows.Controls.Bars;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using ActiproSoftware.Windows.Input;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.GettingStarted.Step08;

/// <summary>
/// Defines the view model for this sample.
/// </summary>
public class SampleWindowViewModel : ObservableObjectBase {

	private ICommand? _helpCommand;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public SampleWindowViewModel() {

		var barManager = BarManager;

		// Initialize the view model for the Ribbon
		Ribbon = new RibbonViewModel() {
			QuickAccessToolBarLocation = RibbonQuickAccessToolBarLocation.Below,

			Backstage = new RibbonBackstageViewModel() {

				// SAMPLE NOTE 8.1:
				//   The Backstage displays "full screen" over the entire RibbonContainerPanel, defines a "Back" button,
				//   and a column of buttons or tabs that can be invoked or selected to display additional content.
				//
				//   The RibbonBackstageViewModel.Items define the menu of options shown on the left edge
				//   of the Backstage. Buttons can be displayed that will perform an action and close the
				//   Backstage. Tabs can also be configured that, when selected, will display additional
				//   content next to the column of buttons.
				//
				//   This step adds a few buttons to the Backstage that are commonly used by applications. Most
				//   of these buttons will be associated with the "NotImplementedCommand" since they are for
				//   demonstration purposes only and the equivalent routed commands are not handled by the
				//   sample application.
				//
				//   Buttons do support images, but images for these buttons are not commonly used in
				//   modern application.
				//
				//   Tabs will be configured in a future step.
				Items = {

					// SAMPLE NOTE 8.2:
					//   These initial items will be aligned at the top of the Backstage column (default alignment)
					new RibbonBackstageHeaderButtonViewModel(SampleBarControlKeys.BackstageClose, "Close", SampleBarManager.NotImplementedCommand),
					new RibbonBackstageHeaderButtonViewModel(SampleBarControlKeys.BackstagePrint, "Print", SampleBarManager.NotImplementedCommand),

					// SAMPLE NOTE 8.3:
					//   The following items will be aligned at the bottom of the Backstage column. A separator is also added to
					//   render a line before the bottom buttons.
					new RibbonBackstageHeaderSeparatorViewModel(RibbonBackstageHeaderAlignment.Bottom),
					new RibbonBackstageHeaderButtonViewModel(SampleBarControlKeys.BackstageHelp, "Help", "E", ApplicationCommands.Help) {
						HeaderAlignment = RibbonBackstageHeaderAlignment.Bottom
					},

				}
			},

			QuickAccessToolBar = new RibbonQuickAccessToolBarViewModel() {
				CommonItems = {
					barManager.ControlViewModels[SampleBarControlKeys.Cut],
					barManager.ControlViewModels[SampleBarControlKeys.Copy],
					barManager.ControlViewModels[SampleBarControlKeys.PasteMenu],
				},
				Items = {
					barManager.ControlViewModels[SampleBarControlKeys.Undo],
					barManager.ControlViewModels[SampleBarControlKeys.Redo],
					barManager.ControlViewModels[SampleBarControlKeys.Cut],
					barManager.ControlViewModels[SampleBarControlKeys.Copy],
					barManager.ControlViewModels[SampleBarControlKeys.PasteMenu],
				},
			},

			Tabs = {
				new RibbonTabViewModel("Home") {
					Groups = {
						new RibbonGroupViewModel(SampleBarControlKeys.GroupUndo, "Undo") {
							SmallImageSource = barManager.ImageProvider.GetImageSource(SampleBarControlKeys.GroupUndo, BarImageSize.Small),
							Items = {
								new RibbonControlGroupViewModel() {
									ItemVariantBehavior = ItemVariantBehavior.AlwaysSmall,
									Items = {
										barManager.ControlViewModels[SampleBarControlKeys.Undo],
										barManager.ControlViewModels[SampleBarControlKeys.Redo],
									}
								}
							}
						},
						new RibbonGroupViewModel(SampleBarControlKeys.GroupClipboard, "Clipboard") {
							SmallImageSource = barManager.ImageProvider.GetImageSource(SampleBarControlKeys.GroupClipboard, BarImageSize.Small),
							Items = {
								barManager.ControlViewModels[SampleBarControlKeys.PasteMenu],
								new RibbonControlGroupViewModel() {
									ItemVariantBehavior = ItemVariantBehavior.NeverLarge,
									Items = {
										barManager.ControlViewModels[SampleBarControlKeys.Cut],
										barManager.ControlViewModels[SampleBarControlKeys.Copy],
									}
								}
							}
						},
					}
				},
				new RibbonTabViewModel("Help") {
					KeyTipText = "E",
					Groups = {
						new RibbonGroupViewModel(SampleBarControlKeys.GroupHelpResources, "Resources") {
							Items = {
								barManager.ControlViewModels[SampleBarControlKeys.Help],
							}
						}
					}
				}
			}

		};

	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The manager for working with the objects used by Ribbon and related menus.
	/// </summary>
	private SampleBarManager BarManager { get; } = new SampleBarManager();

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The view models of the menu controls to be displayed in the editor's context menu.
	/// </summary>
	/// <value>An <see cref="IEnumerable{T}"/> of type <see cref="object"/> for each view model.</value>
	public IEnumerable<object> EditorContextMenuItems {
		get {
			yield return BarManager.ControlViewModels[SampleBarControlKeys.Cut];
			yield return BarManager.ControlViewModels[SampleBarControlKeys.Copy];
			yield return BarManager.ControlViewModels[SampleBarControlKeys.Paste];
			yield return new BarSeparatorViewModel();
			yield return BarManager.ControlViewModels[SampleBarControlKeys.SelectAll];
		}
	}

	/// <summary>
	/// The command for displaying Help.
	/// </summary>
	public ICommand HelpCommand {
		get => _helpCommand ??= new DelegateCommand<object>(
			executeAction: _ => {
				MessageBox.Show("This is where contextual Help would be displayed.", "Help", MessageBoxButton.OK, MessageBoxImage.Information);
			}
		);
	}

	/// <summary>
	/// The view model for the ribbon control.
	/// </summary>
	public RibbonViewModel Ribbon { get; }

}
