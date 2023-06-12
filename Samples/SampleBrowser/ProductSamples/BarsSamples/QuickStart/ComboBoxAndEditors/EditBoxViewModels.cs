using ActiproSoftware.Windows.Controls.Bars;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using ActiproSoftware.Windows.Controls.Editors;
using System;
using System.Windows.Media;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.ComboBoxAndEditors {
	
	/// <summary>
	/// Represents an abstract base view model for an editbox control within a bar control.
	/// </summary>
	public abstract class PartEditBoxViewModelBase<T> : BarKeyedObjectViewModelBase {

		private string description;
		private T editorValue;
		private string label;
		private double requestedWidth = 110.0;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		protected PartEditBoxViewModelBase()  // Parameterless constructor required for XAML support
			: this(key: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified key.  The label is auto-generated.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		protected PartEditBoxViewModelBase(string key)
			: this(key, label: null) { }
		
		/// <summary>
		/// Initializes a new instance of the class with the specified key and label.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		/// <param name="label">The text label to display, which is auto-generated from the <paramref name="key"/> if <c>null</c>.</param>
		protected PartEditBoxViewModelBase(string key, string label) 
			: base(key) {

			this.label = label ?? BarControlService.LabelGenerator.FromKey(key);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the text label to display.
		/// </summary>
		/// <value>The text label to display.</value>
		public string Label {
			get => label;
			set {
				if (label != value) {
					label = value;
					this.NotifyPropertyChanged(nameof(Label));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the text description to display in screen tips.
		/// </summary>
		/// <value>The text description to display in screen tips.</value>
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
		/// Gets or sets the requested width of the control.
		/// </summary>
		/// <value>
		/// The requested width of the control.
		/// The default value is <c>110</c>.
		/// </value>
		public double RequestedWidth {
			get {
				return requestedWidth;
			}
			set {
				if (requestedWidth != value) {
					requestedWidth = value;
					this.NotifyPropertyChanged(nameof(RequestedWidth));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the text description to display in screen tips.
		/// </summary>
		/// <value>The text description to display in screen tips.</value>
		public T Value {
			get => editorValue;
			set {
				if (!editorValue?.Equals(value) ?? true) {
					editorValue = value;
					this.NotifyPropertyChanged(nameof(Value));
				}
			}
		}
		
	}
	
	/// <summary>
	/// Represents a view model for a <see cref="ColorEditBox"/> control within a bar control.
	/// </summary>
	public class ColorEditBoxViewModel : PartEditBoxViewModelBase<Color?> {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class with the specified key.  The label is auto-generated.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		public ColorEditBoxViewModel(string key) : base(key) { }

	}

	/// <summary>
	/// Represents a view model for a <see cref="DateEditBox"/> control within a bar control.
	/// </summary>
	public class DateEditBoxViewModel : PartEditBoxViewModelBase<DateTime?> {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class with the specified key.  The label is auto-generated.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		public DateEditBoxViewModel(string key) : base(key) { }

	}

	/// <summary>
	/// Represents a view model for a <see cref="Int32EditBox"/> control within a bar control.
	/// </summary>
	public class Int32EditBoxViewModel : PartEditBoxViewModelBase<int?> {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class with the specified key.  The label is auto-generated.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		public Int32EditBoxViewModel(string key) : base(key) { }

	}

}
