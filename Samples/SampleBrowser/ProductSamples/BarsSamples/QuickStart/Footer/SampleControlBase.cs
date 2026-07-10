using ActiproSoftware.ProductSamples.BarsSamples.Common;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.Footer;

/// <summary>
/// Provides the base user control of shared logic for this sample that is extended for MVVM- and XAML-based samples.
/// </summary>
public abstract class SampleControlBase : UserControl {

	#region Dependency Properties

	public static readonly DependencyProperty OptionsProperty
		= DependencyProperty.Register(nameof(Options), typeof(OptionsViewModel), typeof(SampleControlBase), new PropertyMetadata(defaultValue: null, OnOptionsPropertyValueChanged));

	#endregion Dependency Properties

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public SampleControlBase() {
		// Initialize the Ribbon view models (used by both XAML and MVVM samples for the Ribbon configuration not related to the footer)
		Ribbon = InitializeRibbonViewModels();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes the view models for ribbon.
	/// </summary>
	private static RibbonViewModel InitializeRibbonViewModels() {
		// The focus of this sample is the Ribbon Footer, so the MVVM- and XAML-based samples
		//   will reuse the same core Ribbon MVVM configuration for non-footer configuration to keep
		//   sample focused only on the footer configuration
		var ribbonViewModel = SampleViewModelFactory.CreateDefaultRichTextEditorRibbonWindowViewModel().Ribbon;
		ribbonViewModel.IsApplicationButtonVisible = false;
		ribbonViewModel.IsCollapsible = false;
		return ribbonViewModel;
	}

	/// <summary>
	/// Occurs when the <see cref="OptionsProperty"/> dependency property value has changed.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private static void OnOptionsPropertyValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
		=> ((SampleControlBase)obj).OnOptionsPropertyValueChanged(e.OldValue as OptionsViewModel, e.NewValue as OptionsViewModel);

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Handles a change in one of the individual property values on <see cref="Options"/>.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="args">The event data.</param>
	protected virtual void OnOptionsPropertyChanged(object? sender, PropertyChangedEventArgs args) {
		if (Ribbon is null)
			return;

		// Synchronize the footer with current options
		UpdateFooter();
	}

	/// <summary>
	/// Handles a change in the <see cref="OptionsProperty"/> dependency property value.
	/// </summary>
	/// <param name="oldValue">The old value.</param>
	/// <param name="newValue">The new value.</param>
	protected virtual void OnOptionsPropertyValueChanged(OptionsViewModel? oldValue, OptionsViewModel? newValue) {
		// Stop listening for changes
		if (oldValue is not null)
			oldValue.PropertyChanged -= OnOptionsPropertyChanged;

		// Listen for changes
		if (newValue is not null)
			newValue.PropertyChanged += OnOptionsPropertyChanged;

		// Synchronize the footer with current options
		UpdateFooter();

	}

	/// <summary>
	/// The options associated with this control.
	/// </summary>
	public OptionsViewModel? Options {
		get => (OptionsViewModel)GetValue(OptionsProperty);
		set => SetValue(OptionsProperty, value);
	}

	/// <summary>
	/// The view model for the Ribbon control.
	/// </summary>
	public RibbonViewModel Ribbon { get; }

	/// <summary>
	/// Updates the footer based on the current sample options.
	/// </summary>
	protected abstract void UpdateFooter();

}
