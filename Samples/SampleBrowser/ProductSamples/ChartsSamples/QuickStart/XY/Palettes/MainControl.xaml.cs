using ActiproSoftware.ProductSamples.Charts.Common;
using ActiproSoftware.Windows.Controls.Charts.Palettes;

namespace ActiproSoftware.ProductSamples.ChartsSamples.QuickStart.XY.Palettes;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	private IList<SeriesPaletteStyleSelector>? _styleSelectors;

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
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Builds the style selectors.
	/// </summary>
	private static List<SeriesPaletteStyleSelector> BuildStyleSelectors() {
		var enumValues = new EnumValueProvider(typeof(PaletteKind)).EnumValues;

		var selectors = new List<SeriesPaletteStyleSelector>();
		foreach (Enum value in enumValues) {
			var palette = new Palette((PaletteKind)value);
			var styleSelector = new SeriesPaletteStyleSelector(palette);
			selectors.Add(styleSelector);
		}

		return selectors;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The collection of style selectors that shows off the available palettes.
	/// </summary>
	public IEnumerable<SeriesPaletteStyleSelector> StyleSelectors
		=> _styleSelectors ??= BuildStyleSelectors();

}
