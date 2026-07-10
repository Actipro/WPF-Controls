using System.Windows.Media.Animation;

namespace ActiproSoftware.ProductSamples.ChartsSamples.Demo.Financial;

/// <summary>
/// Financial demo that demonstrates charts used with updating stock data.
/// </summary>
public partial class MainControl {

	#region Dependency Properties

	/// <summary>
	/// Defines the <see cref="SelectedStock"/> property.
	/// </summary>
	public static readonly DependencyProperty SelectedStockProperty =
		DependencyProperty.Register(nameof(SelectedStock), typeof(Stock), typeof(MainControl), new PropertyMetadata(defaultValue: null));

	#endregion

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		if (DesignerProperties.GetIsInDesignMode(this))
			return;

		var stockBinding = new Binding { Path = new PropertyPath(nameof(SelectedStock)) };
		SetBinding(SelectedStockProperty, stockBinding);

		Loaded += OnLoaded;
	}


	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnLoaded(object sender, RoutedEventArgs e) {
		if (SelectedStock is not null)
			((Storyboard)Resources["ShowChart"]).Begin();
	}


	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override void NotifyUnloaded() {
		base.NotifyUnloaded();

		(DataContext as FinancialViewModel)?.Teardown();
	}

	/// <summary>
	/// The selected stock.
	/// </summary>
	public Stock? SelectedStock {
		get => (Stock)GetValue(SelectedStockProperty);
		set => SetValue(SelectedStockProperty, value);
	}

}
