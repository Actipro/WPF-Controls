using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.ContextualTabs {

	/// <summary>
	/// Provides the base user control of shared logic for this sample that is extended for MVVM- and XAML-based samples.
	/// </summary>
	public abstract class SampleControlBase : UserControl {

		#region Dependency Properties

		public static readonly DependencyProperty OptionsProperty = DependencyProperty.Register(nameof(Options), typeof(OptionsViewModel), typeof(SampleControlBase), new PropertyMetadata(null, OnOptionsPropertyValueChanged));

		#endregion Dependency Properties

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Occurs when the <see cref="OptionsProperty"/> dependency property value has changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> containing data related to this event.</param>
		private static void OnOptionsPropertyValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
			=> ((SampleControlBase)obj).OnOptionsPropertyValueChanged(e.OldValue as OptionsViewModel, e.NewValue as OptionsViewModel);

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Handles a change in one of the individual property values on <see cref="Options"/>.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="args">The event data.</param>
		protected virtual void OnOptionsPropertyChanged(object sender, PropertyChangedEventArgs args) {
			// No operation in base class
		}

		/// <summary>
		/// Handles a change in the <see cref="OptionsProperty"/> dependency property value.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		protected virtual void OnOptionsPropertyValueChanged(OptionsViewModel oldValue, OptionsViewModel newValue) {
			// Stop listening for changes
			if (oldValue != null)
				oldValue.PropertyChanged -= OnOptionsPropertyChanged;

			// Listen for changes
			if (newValue != null)
				newValue.PropertyChanged += OnOptionsPropertyChanged;

		}

		/// <summary>
		/// Gets or sets the options associated with this control.
		/// </summary>
		public OptionsViewModel Options {
			get => (OptionsViewModel)GetValue(OptionsProperty);
			set => SetValue(OptionsProperty, value);
		}

	}

}
