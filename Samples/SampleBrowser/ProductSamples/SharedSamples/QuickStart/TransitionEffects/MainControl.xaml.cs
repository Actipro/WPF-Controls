using ActiproSoftware.Windows.Media.Animation;

namespace ActiproSoftware.ProductSamples.SharedSamples.QuickStart.TransitionEffects;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	private Transition? _transition;
	private bool _useRandomTransition;

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
	/// The <see cref="Transition"/> to use.
	/// </summary>
	public Transition? Transition {
		get => _transition;
		set {
			if (_transition != value) {
				_transition = value;

				if (!_useRandomTransition)
					presenter.Transition = _transition;
			}
		}
	}

	/// <summary>
	/// Indicates whether to use a random transition.
	/// </summary>
	public bool UseRandomTransition {
		get => _useRandomTransition;
		set {
			if (_useRandomTransition != value) {
				_useRandomTransition = value;

				presenter.Transition = _useRandomTransition ? null : _transition;
			}
		}
	}

}
