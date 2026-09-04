using ActiproSoftware.Windows.Media.Animation;
using System.Windows.Media.Animation;
using TransitionEffects;

namespace ActiproSoftware.ProductSamples.SharedSamples.Common;

/// <summary>
/// Implements a transition that uses a .NET 3.5 SP1 shader effect.
/// </summary>
[ContentProperty(nameof(Effect))]
public class EffectTransition : StoryboardTransitionBase {

	#region Dependency Properties

	/// <summary>
	/// Defines the <see cref="BeginTime"/> property.
	/// </summary>
	public static readonly DependencyProperty BeginTimeProperty
		= DependencyProperty.Register(nameof(BeginTime), typeof(TimeSpan), typeof(EffectTransition), new FrameworkPropertyMetadata(defaultValue: TimeSpan.Zero));

	/// <summary>
	/// Defines the <see cref="Duration"/> property.
	/// </summary>
	public static readonly DependencyProperty DurationProperty
		= DependencyProperty.Register(nameof(Duration), typeof(Duration), typeof(EffectTransition), new FrameworkPropertyMetadata(defaultValue: Duration.Automatic));

	/// <summary>
	/// Defines the <see cref="Effect"/> property.
	/// </summary>
	public static readonly DependencyProperty EffectProperty
		= DependencyProperty.Register(nameof(Effect), typeof(TransitionEffect), typeof(EffectTransition), new FrameworkPropertyMetadata(defaultValue: null));

	#endregion

	// --------------------------------------------------------------------------------------------------
	/// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Returns the <see cref="Storyboard"/> to apply to the content.
	/// </summary>
	/// <param name="presenter">The <see cref="TransitionPresenter"/> that is managing the transition.</param>
	private Storyboard GetCoreContentStoryboard(TransitionPresenter presenter) {
		// Get the duration (ensure there is a timespan)
		var duration = ((Duration == Duration.Automatic) && (presenter is not null)) ? presenter.DefaultDuration : Duration;
		if (!duration.HasTimeSpan)
			duration = new Duration(TimeSpan.FromMilliseconds(500));

		// Create the storyboard
		var storyboard = new Storyboard {
			BeginTime = BeginTime,
			FillBehavior = FillBehavior.Stop
		};

		// Add the progress animation
		var progressAnimation = new DoubleAnimation(toValue: 1.0, duration);
		Storyboard.SetTargetProperty(progressAnimation, new PropertyPath("Effect.Progress", []));
		storyboard.Children.Add(progressAnimation);

		return storyboard;
	}

	/// <summary>
	/// Returns the <see cref="Style"/> to apply to the content during the transition.
	/// </summary>
	/// <param name="previousElement">The previous element.</param>
	private Style? GetCoreContentStyle(FrameworkElement previousElement) {
		var effect = Effect?.Clone() as TransitionEffect;
		if (effect is null)
			return null;

		if ((previousElement is not null) && (previousElement.ActualHeight != 0) && (previousElement.ActualWidth != 0)) {
			var visualBrush = new VisualBrush(previousElement) {
				Viewbox = new Rect(0, 0, previousElement.ActualWidth, previousElement.ActualHeight),
				ViewboxUnits = BrushMappingMode.Absolute
			};
			effect.OldImage = visualBrush;
		}
		else {
			effect.OldImage = new VisualBrush();
		}

		var style = new Style(typeof(FrameworkElement));
		style.Setters.Add(new Setter(UIElement.EffectProperty, effect));
		return style;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The time at which this transition should begin.
	/// </summary>
	public TimeSpan BeginTime {
		get => (TimeSpan)GetValue(BeginTimeProperty);
		set => SetValue(BeginTimeProperty, value);
	}

	/// <summary>
	/// The length of time for which this transition plays, not counting repetitions.
	/// </summary>
	/// <value>
	/// The default value is <see cref="Duration.Automatic"/>.
	/// </value>
	public Duration Duration {
		get => (Duration)GetValue(DurationProperty);
		set => SetValue(DurationProperty, value);
	}

	/// <summary>
	/// The <see cref="TransitionEffect"/> used to transition from one element to another.
	/// </summary>
	public TransitionEffect? Effect {
		get => (TransitionEffect)GetValue(EffectProperty);
		set => SetValue(EffectProperty, value);
	}

	/// <inheritdoc/>
	public override Transition GetOppositeTransition()
		=> (EffectTransition)Clone();

	/// <inheritdoc/>
	protected override Storyboard GetToContentStoryboard(TransitionPresenter presenter, FrameworkElement content)
		=> GetCoreContentStoryboard(presenter);

	/// <inheritdoc/>
	protected override Style? GetToContentStyle(TransitionPresenter presenter, FrameworkElement toContent, FrameworkElement fromContent)
		=> GetCoreContentStyle(fromContent);

}
