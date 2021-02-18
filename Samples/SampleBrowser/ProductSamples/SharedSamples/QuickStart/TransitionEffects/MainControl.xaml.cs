using ActiproSoftware.Windows.Media.Animation;

namespace ActiproSoftware.ProductSamples.SharedSamples.QuickStart.TransitionEffects {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		private Transition transition;
		private bool useRandomTransition;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the <see cref="Transition"/> to use.
		/// </summary>
		/// <value>The <see cref="Transition"/> to use.</value>
		public Transition Transition {
			get {
				return transition;
			}
			set {
				if (transition != value) {
					transition = value;

					if (!useRandomTransition)
						presenter.Transition = transition;
				}
			}
		}

		/// <summary>
		/// Gets or sets whether to use a random transition.
		/// </summary>
		/// <value>
		/// <c>true</c> if a random transition should be used; otherwise, <c>false</c>.
		/// </value>
		public bool UseRandomTransition {
			get {
				return useRandomTransition;
			}
			set {
				if (useRandomTransition != value) {
					useRandomTransition = value;

					if (useRandomTransition)
						presenter.Transition = null;
					else
						presenter.Transition = transition;
				}
			}
		}

	}

}