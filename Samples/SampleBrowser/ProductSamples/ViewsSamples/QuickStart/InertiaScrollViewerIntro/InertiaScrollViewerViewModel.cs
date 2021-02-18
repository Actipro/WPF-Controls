using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media.Animation;

namespace ActiproSoftware.ProductSamples.ViewsSamples.QuickStart.InertiaScrollViewerIntro {

	/// <summary>
	/// The view model for InertiaScrollViewer QuickStart.
	/// </summary>
	public class InertiaScrollViewerViewModel : INotifyPropertyChanged {

		private readonly ObservableCollection<IEasingFunction> easingFunctions = new ObservableCollection<IEasingFunction>();
		private IEasingFunction selectedEasingFunction;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="InertiaScrollViewerViewModel"/> class.
		/// </summary>
		public InertiaScrollViewerViewModel() {
			InitializeEasingFunctions();
		}

		#endregion OBJECT

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region EVENTS
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Occurs when a property value changes.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		#endregion EVENTS

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

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

			foreach (EasingFunctionBase easingFunction in EasingFunctions)
				easingFunction.EasingMode = EasingMode.EaseOut;

			SelectedEasingFunction = quarticEase;
		}

		#endregion NON-PUBLIC PROCEDURES

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the easing functions.
		/// </summary>
		/// <value>The easing functions.</value>
		public ObservableCollection<IEasingFunction> EasingFunctions {
			get { return easingFunctions; }
		}

		/// <summary>
		/// Called when property changed.
		/// </summary>
		/// <param name="propertyName">Name of the property.</param>
		protected virtual void OnPropertyChanged(string propertyName) {
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
				handler(this, new PropertyChangedEventArgs(propertyName));
		}

		/// <summary>
		/// Gets or sets the selected easing function.
		/// </summary>
		/// <value>The selected easing function.</value>
		public IEasingFunction SelectedEasingFunction {
			get { return selectedEasingFunction; }
			set {
				selectedEasingFunction = value;
				OnPropertyChanged("SelectedEasingFunction");
			}
		}

		#endregion PUBLIC PROCEDURES
	}
}
