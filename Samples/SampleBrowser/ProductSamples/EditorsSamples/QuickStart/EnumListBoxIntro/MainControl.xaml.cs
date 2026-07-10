using ActiproSoftware.ProductSamples.EditorsSamples.Common;
using ActiproSoftware.SampleBrowser;
using ActiproSoftware.Windows.Controls.Editors.Primitives;

namespace ActiproSoftware.ProductSamples.EditorsSamples.QuickStart.EnumListBoxIntro;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl : ProductItemControl {

	#region Dependency Properties

	/// <summary>
	/// Defines the <see cref="EnumWithFlags"/> property.
	/// </summary>
	public static readonly DependencyProperty EnumWithFlagsProperty
		= DependencyProperty.Register(nameof(EnumWithFlags), typeof(EnumWithFlags?), typeof(MainControl), new PropertyMetadata(defaultValue: Common.EnumWithFlags.None));

	/// <summary>
	/// Defines the <see cref="EnumWithoutFlags"/> property.
	/// </summary>
	public static readonly DependencyProperty EnumWithoutFlagsProperty
		= DependencyProperty.Register(nameof(EnumWithoutFlags), typeof(EnumWithoutFlags?), typeof(MainControl), new PropertyMetadata(defaultValue: Common.EnumWithoutFlags.None));

	#endregion

	// --------------------------------------------------------------------------------------------------
	// NESTED TYPES
	// --------------------------------------------------------------------------------------------------

	#region RandomEnumSortComparer

	/// <summary>
	/// Represents a random sort comparer for enumeration values.
	/// </summary>
	private class RandomEnumSortComparer : IComparer<Enum> {

		private readonly Random _random = new(Environment.TickCount);

		// --------------------------------------------------------------------------------------------------
		// PUBLIC PROCEDURES
		// --------------------------------------------------------------------------------------------------

		/// <inheritdoc cref="IComparer{T}.Compare(T?, T?)"/>
		public int Compare(Enum? x, Enum? y) {
			// If equal, then 0 must be returned
			if (x == y)
				return 0;

			double r = _random.NextDouble();
			if (r < 0.33)
				return -1;
			else if (r > 0.66)
				return 1;
			return 0;
		}

	}

	#endregion

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		DataContext = this;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Occurs when the selection is changed.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnSortComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e) {
		var sortComboBox = (ComboBox)sender;
		listBox1.EnumSortComparer = sortComboBox.SelectedIndex switch {
			1 => EnumValueNameSortComparer.Instance,
			2 => new RandomEnumSortComparer(),
			_ => null
		};
		listBox2.EnumSortComparer = listBox1.EnumSortComparer;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// An enumeration value that has the flags attribute.
	/// </summary>
	/// <value>
	/// The default value is <see cref="EnumWithFlags.None"/>.
	/// </value>
	public EnumWithFlags? EnumWithFlags {
		get => (EnumWithFlags?)GetValue(EnumWithFlagsProperty);
		set => SetValue(EnumWithFlagsProperty, value);
	}

	/// <summary>
	/// An enumeration value that does not have the flags attribute.
	/// </summary>
	/// <value>
	/// The default value is <see cref="EnumWithoutFlags.None"/>.
	/// </value>
	public EnumWithoutFlags? EnumWithoutFlags {
		get => (EnumWithoutFlags?)GetValue(EnumWithoutFlagsProperty);
		set => SetValue(EnumWithoutFlagsProperty, value);
	}

}
