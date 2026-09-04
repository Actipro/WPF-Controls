using System.Windows.Media.Animation;

namespace ActiproSoftware.ProductSamples.ViewsSamples.QuickStart.InertiaScrollViewerIntro;

/// <summary>
/// The view model for InertiaScrollViewer QuickStart.
/// </summary>
public class InertiaScrollViewerViewModel : ObservableObjectBase {

	private IEasingFunction? _selectedEasingFunction;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public InertiaScrollViewerViewModel() {
		InitializeEasingFunctions();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes the easing functions.
	/// </summary>
	private void InitializeEasingFunctions() {
		var quarticEase = new QuarticEase();

		EasingFunctions.Add(new BackEase());
		EasingFunctions.Add(new BounceEase());
		EasingFunctions.Add(new CircleEase());
		EasingFunctions.Add(new CubicEase());
		EasingFunctions.Add(new ElasticEase());
		EasingFunctions.Add(new ExponentialEase());
		EasingFunctions.Add(new PowerEase());
		EasingFunctions.Add(new QuadraticEase());
		EasingFunctions.Add(quarticEase);
		EasingFunctions.Add(new QuinticEase());
		EasingFunctions.Add(new SineEase());

		foreach (var easingFunction in EasingFunctions.OfType<EasingFunctionBase>())
			easingFunction.EasingMode = EasingMode.EaseOut;

		SelectedEasingFunction = quarticEase;
	}


	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The easing functions.
	/// </summary>
	public ObservableCollection<IEasingFunction> EasingFunctions { get; } = [];

	/// <summary>
	/// The selected easing function.
	/// </summary>
	public IEasingFunction? SelectedEasingFunction {
		get => _selectedEasingFunction;
		set => SetProperty(ref _selectedEasingFunction, value);
	}

}
