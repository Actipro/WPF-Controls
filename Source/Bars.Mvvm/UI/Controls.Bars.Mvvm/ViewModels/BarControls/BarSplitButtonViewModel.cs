using System;
using System.Windows.Input;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a split button control within a bar control.
	/// </summary>
	public class BarSplitButtonViewModel : BarPopupButtonViewModel {

		private string	inputGestureText;
		private bool	isInputGestureTextVisible = true;
		private bool	staysOpenOnClick;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="BarButtonViewModel()"/>
		public BarSplitButtonViewModel()  // Parameterless constructor required for XAML support
			: this(key: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string)"/>
		public BarSplitButtonViewModel(string key)
			: this(key, label: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string)"/>
		public BarSplitButtonViewModel(string key, string label)
			: this(key, label, keyTipText: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string, string)"/>
		public BarSplitButtonViewModel(string key, string label, string keyTipText)
			: this(key, label, keyTipText, command: null) { }

		/// <inheritdoc cref="BarButtonViewModel(RoutedCommand)"/>
		public BarSplitButtonViewModel(RoutedCommand routedCommand)
			: this(routedCommand?.Name, routedCommand) { }

		/// <inheritdoc cref="BarButtonViewModel(string, ICommand)"/>
		public BarSplitButtonViewModel(string key, ICommand command)
			: this(key, label: null, command) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string, ICommand)"/>
		public BarSplitButtonViewModel(string key, string label, ICommand command)
			: this(key, label, keyTipText: null, command) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string, string, ICommand)"/>
		public BarSplitButtonViewModel(string key, string label, string keyTipText, ICommand command)
			: base(key, label, keyTipText, command) { }

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="BarButtonViewModel.InputGestureText"/>
		public string InputGestureText {
			get => inputGestureText;
			set {
				if (inputGestureText != value) {
					inputGestureText = value;
					this.NotifyPropertyChanged(nameof(InputGestureText));
				}
			}
		}

		/// <inheritdoc cref="BarButtonViewModel.IsInputGestureTextVisible"/>
		public bool IsInputGestureTextVisible {
			get => isInputGestureTextVisible;
			set {
				if (isInputGestureTextVisible != value) {
					isInputGestureTextVisible = value;
					this.NotifyPropertyChanged(nameof(IsInputGestureTextVisible));
				}
			}
		}

		/// <inheritdoc cref="BarButtonViewModel.StaysOpenOnClick"/>
		public bool StaysOpenOnClick {
			get => staysOpenOnClick;
			set {
				if (staysOpenOnClick != value) {
					staysOpenOnClick = value;
					this.NotifyPropertyChanged(nameof(StaysOpenOnClick));
				}
			}
		}

	}

}
