using ActiproSoftware.Windows.Media.Animation;
using System;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using TransitionEffects;

namespace ActiproSoftware.ProductSamples.SharedSamples.Common {

	/// <summary>
	/// Implements a transition that uses a .NET 3.5 SP1 shader effect.
	/// </summary>
	[ContentProperty("Effect")]
	public class EffectTransition : StoryboardTransitionBase {

		#region Dependency Properties

		/// <summary>
		/// Identifies the <see cref="BeginTime"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="BeginTime"/> dependency property.</value>
		public static readonly DependencyProperty BeginTimeProperty = DependencyProperty.Register("BeginTime", typeof(TimeSpan), typeof(EffectTransition), new FrameworkPropertyMetadata(TimeSpan.Zero));

		/// <summary>
		/// Identifies the <see cref="Duration"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="Duration"/> dependency property.</value>
		public static readonly DependencyProperty DurationProperty = DependencyProperty.Register("Duration", typeof(Duration), typeof(EffectTransition), new FrameworkPropertyMetadata(Duration.Automatic));

		/// <summary>
		/// Identifies the <see cref="Effect"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="Effect"/> dependency property.</value>
		public static readonly DependencyProperty EffectProperty = DependencyProperty.Register("Effect", typeof(TransitionEffect), typeof(EffectTransition), new FrameworkPropertyMetadata(null));

		#endregion // Dependency Properties

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		/// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Returns the <see cref="Storyboard"/> to apply to the content.
		/// </summary>
		/// <param name="presenter">The <see cref="TransitionPresenter"/> that is managing the transition.</param>
		/// <returns>The <see cref="Storyboard"/> to apply to the "to" content.</returns>
		private Storyboard GetCoreContentStoryboard(TransitionPresenter presenter) {

			// Get the duration (ensure there is a timespan)
			Duration duration = ((this.Duration == Duration.Automatic) && (presenter != null) ? presenter.DefaultDuration : this.Duration);
			if (!duration.HasTimeSpan)
				duration = new Duration(TimeSpan.FromMilliseconds(500));

			// Create the storyboard
			Storyboard storyboard = new Storyboard();
			storyboard.BeginTime = this.BeginTime;
			storyboard.FillBehavior = FillBehavior.Stop;

			// Add the progress animation
			DoubleAnimation progressAnimation = new DoubleAnimation(1.0, duration);
			Storyboard.SetTargetProperty(progressAnimation, new PropertyPath("Effect.Progress", new object[] { }));
			storyboard.Children.Add(progressAnimation);

			return storyboard;
		}


		/// <summary>
		/// Returns the <see cref="Style"/> to apply to the content during the transition.
		/// </summary>
		/// <param name="presenter">The <see cref="TransitionPresenter"/> that is managing the transition.</param>
		/// <param name="previousElement">The previous element.</param>
		/// <returns>
		/// The <see cref="Style"/> to apply to the "to" content during the transition.
		/// </returns>
		private Style GetCoreContentStyle(TransitionPresenter presenter, FrameworkElement previousElement) {
			TransitionEffect effect = this.Effect;
			if (null == effect)
				return null;

			effect = effect.Clone() as TransitionEffect;
			if (null == effect)
				return null;

			if (null != previousElement && 0 != previousElement.ActualHeight && 0 != previousElement.ActualWidth) {
				VisualBrush visualBrush = new VisualBrush(previousElement);
				visualBrush.Viewbox = new Rect(0, 0, previousElement.ActualWidth, previousElement.ActualHeight);
				visualBrush.ViewboxUnits = BrushMappingMode.Absolute;
				effect.OldImage = visualBrush;
			}
			else {
				effect.OldImage = new VisualBrush();
			}

			Style style = new Style(typeof(FrameworkElement));
			style.Setters.Add(new Setter(FrameworkElement.EffectProperty, effect));
			return style;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the time at which this transition should begin. 
		/// </summary>
		/// <value>The time at which this transition should begin, relative to the parent's begin time.</value>
		public TimeSpan BeginTime {
			get {
				return (TimeSpan)this.GetValue(EffectTransition.BeginTimeProperty);
			}
			set {
				this.SetValue(EffectTransition.BeginTimeProperty, value);
			}
		}

		/// <summary>
		/// Gets or sets the length of time for which this transition plays, not counting repetitions.
		/// </summary>
		/// <value>
		/// The transition's simple duration: the amount of time this effect takes to complete a single forward iteration. 
		/// The default value is <c>Automatic</c>.
		/// </value>
		public Duration Duration {
			get {
				return (Duration)this.GetValue(EffectTransition.DurationProperty);
			}
			set {
				this.SetValue(EffectTransition.DurationProperty, value);
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="TransitionEffect"/> used to transition from one element to another.
		/// </summary>
		/// <value>the <see cref="TransitionEffect"/> used to transition from one element to another.</value>
		public TransitionEffect Effect {
			get {
				return (TransitionEffect)this.GetValue(EffectTransition.EffectProperty);
			}
			set {
				this.SetValue(EffectTransition.EffectProperty, value);
			}
		}

		/// <summary>
		/// Returns a variation of the transition that can be used for backing out a content that was inserted into the presenter using this transition.
		/// </summary>
		/// <returns>A variation of the transition that can be used for backing out a content that was inserted into the presenter using this transition.</returns>
		public override Transition GetOppositeTransition() {
			return (EffectTransition)this.Clone();
		}

		/// <summary>
		/// Returns the <see cref="Storyboard"/> to apply to the "to" content.
		/// </summary>
		/// <param name="presenter">The <see cref="TransitionPresenter"/> that is managing the transition.</param>
		/// <param name="content">The element requesting a <see cref="Style"/>.</param>
		/// <returns>
		/// The <see cref="Storyboard"/> to apply to the "to" content.
		/// </returns>
		protected override Storyboard GetToContentStoryboard(TransitionPresenter presenter, FrameworkElement content) {
			return this.GetCoreContentStoryboard(presenter);
		}

		/// <summary>
		/// Returns the <see cref="Style"/> to apply to the "to" content during the transition.
		/// </summary>
		/// <param name="presenter">The <see cref="TransitionPresenter"/> that is managing the transition.</param>
		/// <param name="toContent">The element requesting a <see cref="Style"/>.</param>
		/// <param name="fromContent">The element from which a transition is occurring.</param>
		/// <returns>
		/// The <see cref="Style"/> to apply to the "to" content during the transition.
		/// </returns>
		protected override Style GetToContentStyle(TransitionPresenter presenter, FrameworkElement toContent,
			FrameworkElement fromContent) {
			return this.GetCoreContentStyle(presenter, fromContent);
		}
	}
}
