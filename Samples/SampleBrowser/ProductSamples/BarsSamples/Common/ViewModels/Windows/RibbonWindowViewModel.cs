using ActiproSoftware.SampleBrowser;
using ActiproSoftware.Windows.Controls.Bars;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using ActiproSoftware.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common;

public class RibbonWindowViewModel : WindowViewModel {

	private ICommand? _toggleApplicationButtonCommand;
	private ICommand? _toggleFooterCommand;
	private ICommand? _toggleQuickAccessToolBarCommand;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc cref="WindowViewModel(BarManager)"/>
	/// <param name="ribbonViewModel">The view model of the ribbon associated with the window.</param>
	public RibbonWindowViewModel(BarManager barManager, RibbonViewModel ribbonViewModel)
		: base(barManager) {

		Ribbon = ribbonViewModel ?? throw new ArgumentNullException(nameof(ribbonViewModel));

		barManager.UpdateControlViewModelCheckedState(BarControlKeys.ShowApplicationButton, () => Ribbon?.IsApplicationButtonVisible == true);
		barManager.UpdateControlViewModelCheckedState(BarControlKeys.ShowFooter, () => Ribbon?.Footer is not null);
		barManager.UpdateControlViewModelCheckedState(BarControlKeys.ShowQuickAccessToolBar, () => Ribbon?.QuickAccessToolBarMode == RibbonQuickAccessToolBarMode.Visible);

	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override IEnumerable<KeyValuePair<CompositeCommand, ICommand>> GetCommandMappings(BarManager barManager) {
		return base.GetCommandMappings(barManager)
			.Concat(new Dictionary<CompositeCommand, ICommand>() {
				{ barManager.ToggleApplicationButtonCommand, ToggleApplicationButtonCommand },
				{ barManager.ToggleFooterCommand, ToggleFooterCommand },
				{ barManager.ToggleQuickAccessToolBarCommand, ToggleQuickAccessToolBarCommand },
			});
	}

	/// <summary>
	/// The view model of the ribbon associated with the window.
	/// </summary>
	public RibbonViewModel Ribbon { get; }

	/// <summary>
	/// The command which toggles the visibility of the ribbon application button.
	/// </summary>
	/// <value>An <see cref="ICommand"/>.</value>
	public ICommand ToggleApplicationButtonCommand {
		get => _toggleApplicationButtonCommand ??= new DelegateCommand<object>(
			executeAction: _ => {
				BarManager.SetValueFromControlViewModelCheckedState(
					BarControlKeys.ShowApplicationButton,
					isChecked => Ribbon.IsApplicationButtonVisible = isChecked
				);
			}
		);
	}

	/// <summary>
	/// The command which toggles the visibility of the ribbon footer.
	/// </summary>
	public ICommand ToggleFooterCommand {
		get => _toggleFooterCommand ??= new DelegateCommand<object>(
			executeAction: _ => {
				BarManager.SetValueFromControlViewModelCheckedState(
					BarControlKeys.ShowFooter,
					isChecked => {
						Ribbon.Footer = isChecked
							? new RibbonFooterViewModel() {
								Kind = RibbonFooterKind.Warning,
								Content = new RibbonFooterSimpleContentViewModel() {
									ImageSource = ImageLoader.GetIcon("InformationClear16.png"),
									Text = "Actipro Bars contains everything you need to implement modern ribbon, toolbar, and menu interfaces in your apps.",
								}
							}
							: null;
					}
				);
			}
		);
	}

	/// <summary>
	/// The command which toggles the visibility of the ribbon quick access toolbar.
	/// </summary>
	public ICommand ToggleQuickAccessToolBarCommand {
		get => _toggleQuickAccessToolBarCommand ??= new DelegateCommand<object>(
			executeAction: _ => {
				BarManager.SetValueFromControlViewModelCheckedState(
					BarControlKeys.ShowQuickAccessToolBar,
					isChecked => Ribbon.QuickAccessToolBarMode = (isChecked ? RibbonQuickAccessToolBarMode.Visible : RibbonQuickAccessToolBarMode.Hidden)
				);
			},
			canExecuteFunc: _ => (Ribbon?.QuickAccessToolBarMode ?? RibbonQuickAccessToolBarMode.None) != RibbonQuickAccessToolBarMode.None
		);
	}

}
