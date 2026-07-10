using ActiproSoftware.Windows.Controls.MicroCharts.Palettes;

namespace ActiproSoftware.ProductSamples.MicroChartsSamples.QuickStart.Palettes;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	private IList<MicroSeriesPaletteStyleSelector>? _styleSelectors;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The collection of style selectors that shows off the available palettes.
	/// </summary>
	public IEnumerable<MicroSeriesPaletteStyleSelector> StyleSelectors {
		get {
			if (_styleSelectors is null) {
				_styleSelectors = [];
				#if NET
				foreach (var value in Enum.GetValues<MicroPaletteKind>()) {
				#else
				foreach (MicroPaletteKind value in Enum.GetValues(typeof(MicroPaletteKind))) {
				#endif
					var palette = new MicroPalette(value);
					var styleSelector = new MicroSeriesPaletteStyleSelector(palette);
					_styleSelectors.Add(styleSelector);
				}
			}
			return _styleSelectors;
		}
	}

}
