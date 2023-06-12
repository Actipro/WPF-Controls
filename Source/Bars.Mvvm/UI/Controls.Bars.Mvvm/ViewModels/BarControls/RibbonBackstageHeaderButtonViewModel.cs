using System;
using System.Windows.Input;
using System.Windows.Media;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a button control within a ribbon backstage header.
	/// </summary>
	public class RibbonBackstageHeaderButtonViewModel : BarKeyedObjectViewModelBase {

		private ICommand command;
		private object commandParameter;
		private string description;
		private RibbonBackstageHeaderAlignment headerAlignment = RibbonBackstageHeaderAlignment.Top;
		private string keyTipText;
		private string label;
		private ImageSource smallImageSource;
		private string title;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="BarButtonViewModel()"/>
		public RibbonBackstageHeaderButtonViewModel()  // Parameterless constructor required for XAML support
			: this(key: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string)"/>
		public RibbonBackstageHeaderButtonViewModel(string key)
			: this(key, label: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string)"/>
		public RibbonBackstageHeaderButtonViewModel(string key, string label)
			: this(key, label, keyTipText: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string, string)"/>
		public RibbonBackstageHeaderButtonViewModel(string key, string label, string keyTipText)
			: this(key, label, keyTipText, command: null) { }

		/// <inheritdoc cref="BarButtonViewModel(RoutedCommand)"/>
		public RibbonBackstageHeaderButtonViewModel(RoutedCommand routedCommand)
			: this(routedCommand?.Name, routedCommand) { }

		/// <inheritdoc cref="BarButtonViewModel(string, ICommand)"/>
		public RibbonBackstageHeaderButtonViewModel(string key, ICommand command)
			: this(key, label: null, command) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string, ICommand)"/>
		public RibbonBackstageHeaderButtonViewModel(string key, string label, ICommand command)
			: this(key, label, keyTipText: null, command) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string, string, ICommand)"/>
		public RibbonBackstageHeaderButtonViewModel(string key, string label, string keyTipText, ICommand command)
			: base(key) {

			this.label = label ?? BarControlService.LabelGenerator.FromCommand(command) ?? BarControlService.LabelGenerator.FromKey(key);
			this.keyTipText = keyTipText ?? BarControlService.KeyTipTextGenerator.FromCommand(command) ?? BarControlService.KeyTipTextGenerator.FromLabel(this.label);
			this.command = command;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="BarButtonViewModel.Command"/>
		public ICommand Command {
			get => command;
			set {
				if (command != value) {
					command = value;
					this.NotifyPropertyChanged(nameof(Command));
				}
			}
		}

		/// <inheritdoc cref="BarButtonViewModel.CommandParameter"/>
		public object CommandParameter {
			get => commandParameter;
			set {
				if (commandParameter != value) {
					commandParameter = value;
					this.NotifyPropertyChanged(nameof(CommandParameter));
				}
			}
		}
		
		/// <inheritdoc cref="BarButtonViewModel.Description"/>
		public string Description {
			get => description;
			set {
				if (description != value) {
					description = value;
					this.NotifyPropertyChanged(nameof(Description));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets a <see cref="RibbonBackstageHeaderAlignment"/> indicating the alignment of the control within the ribbon Backstage header.
		/// </summary>
		/// <value>
		/// A <see cref="RibbonBackstageHeaderAlignment"/> indicating the alignment of the control within the ribbon Backstage header.
		/// The default value is <see cref="RibbonBackstageHeaderAlignment.Top"/>.
		/// </value>
		public RibbonBackstageHeaderAlignment HeaderAlignment {
			get => headerAlignment;
			set {
				if (headerAlignment != value) {
					headerAlignment = value;
					this.NotifyPropertyChanged(nameof(HeaderAlignment));
				}
			}
		}

		/// <inheritdoc cref="BarButtonViewModel.KeyTipText"/>
		public string KeyTipText {
			get => keyTipText;
			set {
				if (keyTipText != value) {
					keyTipText = value;
					this.NotifyPropertyChanged(nameof(KeyTipText));
				}
			}
		}

		/// <inheritdoc cref="BarButtonViewModel.Label"/>
		public string Label {
			get => label;
			set {
				if (label != value) {
					label = value;
					this.NotifyPropertyChanged(nameof(Label));
				}
			}
		}
		
		/// <inheritdoc cref="BarButtonViewModel.SmallImageSource"/>
		public ImageSource SmallImageSource {
			get => smallImageSource;
			set {
				if (smallImageSource != value) {
					smallImageSource = value;
					this.NotifyPropertyChanged(nameof(SmallImageSource));
				}
			}
		}
		
		/// <inheritdoc cref="BarButtonViewModel.Title"/>
		public string Title {
			get => title;
			set {
				if (title != value) {
					title = value;
					this.NotifyPropertyChanged(nameof(Title));
				}
			}
		}

	}

}
