using ActiproSoftware.Windows.Controls.Bars;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.Footer;

/// <summary>
/// Defines configurable options for this sample.
/// </summary>
public class OptionsViewModel : ObservableObjectBase {

	private RibbonFooterKind _footerKind = RibbonFooterKind.Warning;
	private bool _isFooterVisible = true;
	private Thickness _padding = new(10, 5, 10, 5);
	private RibbonQuickAccessToolBarLocation _qatLocation = RibbonQuickAccessToolBarLocation.Below;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The footer kind.
	/// </summary>
	[DisplayName("Kind")]
	public RibbonFooterKind FooterKind {
		get => _footerKind;
		set => SetProperty(ref _footerKind, value);
	}

	/// <summary>
	/// Indicates if the footer is visible.
	/// </summary>
	public bool IsFooterVisible {
		get => _isFooterVisible;
		set => SetProperty(ref _isFooterVisible, value);
	}

	/// <summary>
	/// The padding for the footer content.
	/// </summary>
	[DisplayName("Padding")]
	public Thickness Padding {
		get => _padding;
		set => SetProperty(ref _padding, value);
	}

	/// <summary>
	/// The location of the Quick Access Toolbar.
	/// </summary>
	[DisplayName("QAT location")]
	public RibbonQuickAccessToolBarLocation QuickAccessToolBarLocation {
		get => _qatLocation;
		set => SetProperty(ref _qatLocation, value);
	}

}
