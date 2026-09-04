using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using ActiproSoftware.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.FooterInfoBar;

/// <summary>
/// Provides the user control for this sample that uses an MVVM-based ribbon configuration.
/// </summary>
public partial class SampleMvvmControl : SampleControlBase {

	private RibbonFooterViewModel? _footerViewModel;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public SampleMvvmControl() {
		InitializeComponent();

		// Configure this code-behind to be the view model for this sample
		DataContext = this;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The view model for the footer.
	/// </summary>
	private RibbonFooterViewModel FooterViewModel {
		get => _footerViewModel ??= new RibbonFooterViewModel() {
			// The RibbonFooterInfoBarContentViewModel can be used to define a footer
			//   with features supported by the InfoBar control
			Content = new RibbonFooterInfoBarContentViewModel() {
				CanClose = Options?.CanClose ?? true,
				IsIconVisible = Options?.CanClose ?? true,
				Message = "Use an info bar for essential app messages",
				Padding = Options?.Padding ?? new Thickness(),
				Title = "InfoBar",
				Severity = Options?.Severity ?? Windows.Controls.InfoBarSeverity.Information,
			},

			// Footer must not have padding so InfoBar can display edge-to-edge
			Padding = new Thickness(),
		};
	}

	/// <summary>
	/// Shows the footer.
	/// </summary>
	private void ShowFooter() {
		// When the footer is closed the view model is cleared.
		// Show the footer again by re-assigning the view model that defines the footer.
		Ribbon.Footer = FooterViewModel;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override RibbonViewModel InitializeRibbonViewModels() {
		var ribbonViewModel = base.InitializeRibbonViewModels();

		// Initialize the ribbon with a footer already displayed
		ribbonViewModel.Footer = FooterViewModel;

		return ribbonViewModel;
	}

	/// <inheritdoc/>
	protected override void OnOptionsPropertyValueChanged(OptionsViewModel? oldValue, OptionsViewModel? newValue) {
		// Configure the command to show the MVVM-based footer
		if (newValue is not null)
			newValue.ShowFooterMvvmCommand = new DelegateCommand<object>(_ => ShowFooter());

		base.OnOptionsPropertyValueChanged(oldValue, newValue);
	}

	/// <inheritdoc/>
	protected override void UpdateFooter() {
		if (Options is null)
			return;

		// Update properties to match the sample options
		if (FooterViewModel?.Content is RibbonFooterInfoBarContentViewModel infoBarViewModel) {
			infoBarViewModel.CanClose = Options.CanClose;
			infoBarViewModel.IsIconVisible = Options.IsIconVisible;
			infoBarViewModel.Padding = Options.Padding;
			infoBarViewModel.Severity = Options.Severity;
		}
	}

}
