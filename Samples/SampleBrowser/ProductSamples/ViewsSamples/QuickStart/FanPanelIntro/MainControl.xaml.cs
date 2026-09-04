using ActiproSoftware.ProductSamples.ViewsSamples.QuickStart.Common;
using ActiproSoftware.Windows.Controls.Views;
using ActiproSoftware.Windows.Data;

namespace ActiproSoftware.ProductSamples.ViewsSamples.QuickStart.FanPanelIntro;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	#region Dependency Properties

	/// <summary>
	/// Defines the <see cref="BackAngleStep"/> property.
	/// </summary>
	public static readonly DependencyProperty BackAngleStepProperty
		= DependencyProperty.Register(nameof(BackAngleStep), typeof(double), typeof(MainControl), new FrameworkPropertyMetadata(defaultValue: 15.0), obj => ValidationHelper.ValidateDoubleIsBetweenInclusive(obj, -360, 360));

	/// <summary>
	/// Defines the <see cref="BackOffsetStep"/> property.
	/// </summary>
	public static readonly DependencyProperty BackOffsetStepProperty
		= DependencyProperty.Register(nameof(BackOffsetStep), typeof(Point), typeof(MainControl), new FrameworkPropertyMetadata(defaultValue: new Point()));

	/// <summary>
	/// Defines the <see cref="BackOpacityStep"/> property.
	/// </summary>
	public static readonly DependencyProperty BackOpacityStepProperty
		= DependencyProperty.Register(nameof(BackOpacityStep), typeof(double), typeof(MainControl), new FrameworkPropertyMetadata(defaultValue: 0.1), obj => ValidationHelper.ValidateDoubleIsBetweenInclusive(obj, 0, 1));

	/// <summary>
	/// Defines the <see cref="ForeAngleStep"/> property.
	/// </summary>
	public static readonly DependencyProperty ForeAngleStepProperty
		= DependencyProperty.Register(nameof(ForeAngleStep), typeof(double), typeof(MainControl), new FrameworkPropertyMetadata(defaultValue: 15.0), obj => ValidationHelper.ValidateDoubleIsBetweenInclusive(obj, -360, 360));

	/// <summary>
	/// Defines the <see cref="ForeElementLayoutPlacement"/> property.
	/// </summary>
	public static readonly DependencyProperty ForeElementLayoutPlacementProperty
		= DependencyProperty.Register(nameof(ForeElementLayoutPlacement), typeof(ElementLayoutPlacement), typeof(MainControl), new FrameworkPropertyMetadata(defaultValue: ElementLayoutPlacement.Hidden));

	/// <summary>
	/// Defines the <see cref="ForeOffsetStep"/> property.
	/// </summary>
	public static readonly DependencyProperty ForeOffsetStepProperty
		= DependencyProperty.Register(nameof(ForeOffsetStep), typeof(Point), typeof(MainControl), new FrameworkPropertyMetadata(defaultValue: new Point()));

	/// <summary>
	/// Defines the <see cref="ForeOpacityStep"/> property.
	/// </summary>
	public static readonly DependencyProperty ForeOpacityStepProperty
		= DependencyProperty.Register(nameof(ForeOpacityStep), typeof(double), typeof(MainControl), new FrameworkPropertyMetadata(defaultValue: 0.1), obj => ValidationHelper.ValidateDoubleIsBetweenInclusive(obj, 0, 1));

	#endregion

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		for (var i = 0; i < 15; i++)
			AddNewItem();
		listBox.SelectedIndex = 2;

		// Show horizontal layout
		OnLayoutHorizontalButtonClick(this, e: null);
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void AddNewItem()
		=> listBox.Items.Add(CreateItem());

	private static ProductListBoxItem CreateItem()
		=> new() { IsMovable = true };

	private void OnClearAllClick(object sender, RoutedEventArgs e)
		=> listBox.Items.Clear();

	private void OnAddItemClick(object sender, RoutedEventArgs e)
		=> AddNewItem();

	private void OnInsertItemClick(object sender, RoutedEventArgs e) {
		var index = MathHelper.Range(listBox.SelectedIndex + 1, 0, listBox.Items.Count);
		listBox.Items.Insert(index, CreateItem());
	}

	private void OnLayoutDefaultButtonClick(object sender, RoutedEventArgs e) {
		ForeAngleStep = 15.0;
		ForeElementLayoutPlacement = ElementLayoutPlacement.Hidden;
		ForeOffsetStep = new Point();
		ForeOpacityStep = 0.1;
		BackAngleStep = 15.0;
		BackOffsetStep = new Point();
		BackOpacityStep = 0.1;
	}

	private void OnLayoutHorizontalButtonClick(object sender, RoutedEventArgs? e) {
		ForeAngleStep = 10.0;
		ForeElementLayoutPlacement = ElementLayoutPlacement.Below;
		ForeOffsetStep = new Point(100, 0);
		ForeOpacityStep = 0.2;
		BackAngleStep = 10.0;
		BackOffsetStep = new Point(100, 0);
		BackOpacityStep = 0.2;
	}

	private void OnLayoutTwistButtonClick(object sender, RoutedEventArgs e) {
		ForeAngleStep = 20.0;
		ForeElementLayoutPlacement = ElementLayoutPlacement.Below;
		ForeOffsetStep = new Point(10, 10);
		ForeOpacityStep = 0.05;
		BackAngleStep = 20.0;
		BackOffsetStep = new Point(10, 10);
		BackOpacityStep = 0.05;
	}

	private void OnLayoutVerticalButtonClick(object sender, RoutedEventArgs e) {
		ForeAngleStep = 0.0;
		ForeElementLayoutPlacement = ElementLayoutPlacement.Below;
		ForeOffsetStep = new Point(0, 125);
		ForeOpacityStep = 0.4;
		BackAngleStep = 0.0;
		BackOffsetStep = new Point(0, 125);
		BackOpacityStep = 0.4;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// A value used in increment the angle of elements after the focal element based on their distance.
	/// </summary>
	/// <value>
	/// The default value is <c>15.0</c>.
	/// </value>
	public double BackAngleStep {
		get => (double)GetValue(BackAngleStepProperty);
		set => SetValue(BackAngleStepProperty, value);
	}

	/// <summary>
	/// A value used in increment the X and Y coordinate of the elements after the focal element based on their distance.
	/// </summary>
	/// <value>
	/// The default value is <c>0,0</c>.
	/// </value>
	public Point BackOffsetStep {
		get => (Point)GetValue(BackOffsetStepProperty);
		set => SetValue(BackOffsetStepProperty, value);
	}

	/// <summary>
	/// A value used in decrement the opacity of the elements after the focal element based on their distance.
	/// </summary>
	/// <value>
	/// The default value is <c>0.1</c>.
	/// </value>
	public double BackOpacityStep {
		get => (double)GetValue(BackOpacityStepProperty);
		set => SetValue(BackOpacityStepProperty, value);
	}

	/// <summary>
	/// A value used in increment the angle of elements before the focal element based on their distance.
	/// </summary>
	/// <value>
	/// The default value is <c>15.0</c>.
	/// </value>
	public double ForeAngleStep {
		get => (double)GetValue(ForeAngleStepProperty);
		set => SetValue(ForeAngleStepProperty, value);
	}

	/// <summary>
	/// The placement of any elements after the focal element in the panel.
	/// </summary>
	/// <value>
	/// The default value is <see cref="ElementLayoutPlacement.Hidden"/>.
	/// </value>
	public ElementLayoutPlacement ForeElementLayoutPlacement {
		get => (ElementLayoutPlacement)GetValue(ForeElementLayoutPlacementProperty);
		set => SetValue(ForeElementLayoutPlacementProperty, value);
	}

	/// <summary>
	/// A value used in increment the X and Y coordinate of the elements before the focal element based on their distance.
	/// </summary>
	/// <value>
	/// The default value is <c>0,0</c>.
	/// </value>
	public Point ForeOffsetStep {
		get => (Point)GetValue(ForeOffsetStepProperty);
		set => SetValue(ForeOffsetStepProperty, value);
	}

	/// <summary>
	/// A value used in decrement the opacity of the elements before the focal element based on their distance.
	/// </summary>
	/// <value>
	/// The default value is <c>0.1</c>.
	/// </value>
	public double ForeOpacityStep {
		get => (double)GetValue(ForeOpacityStepProperty);
		set => SetValue(ForeOpacityStepProperty, value);
	}

}
