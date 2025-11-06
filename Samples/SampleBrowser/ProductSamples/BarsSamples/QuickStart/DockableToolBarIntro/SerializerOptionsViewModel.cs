using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Controls.Bars;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.DockableToolBarIntro {

	/// <summary>
	/// Defines a view model for turning options on or off that will be used during dockable toolbar serialization.
	/// </summary>
	public class SerializerOptionsViewModel : ObservableObjectBase {

		private bool floatingLocation = true;
		private bool isFloating = true;
		private bool isVisible = true;
		private bool lineIndex = true;
		private bool offset = true;
		private bool placement = true;
		private bool sortOrder = true;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the <see cref="DockableToolBarSerializerOptions"/> that are reflected by the current configuration.
		/// </summary>
		/// <returns>One or more of the <see cref="DockableToolBarSerializerOptions"/> values.</returns>
		public DockableToolBarSerializerOptions CreateOptions() {
			// Start with no options enabled
			var options = DockableToolBarSerializerOptions.None;

			// Add each option that is currently turned on
			if (FloatingLocation)
				options |= DockableToolBarSerializerOptions.FloatingLocation;
			if (IsFloating)
				options |= DockableToolBarSerializerOptions.IsFloating;
			if (IsVisible)
				options |= DockableToolBarSerializerOptions.IsVisible;
			if (LineIndex)
				options |= DockableToolBarSerializerOptions.LineIndex;
			if (Offset)
				options |= DockableToolBarSerializerOptions.Offset;
			if (Placement)
				options |= DockableToolBarSerializerOptions.Placement;
			if (SortOrder)
				options |= DockableToolBarSerializerOptions.SortOrder;

			return options;
		}

		/// <summary>
		/// Gets or sets if the option will be processed when serializing or deserializing.
		/// </summary>
		/// <value><c>true</c> to process the option; otherwise <c>false</c> to ignore it.</value>
		public bool FloatingLocation {
			get => floatingLocation;
			set {
				if (floatingLocation != value) {
					floatingLocation = value;
					NotifyPropertyChanged(nameof(FloatingLocation));
				}
			}
		}

		/// <summary>
		/// Gets or sets if the option will be processed when serializing or deserializing.
		/// </summary>
		/// <value><c>true</c> to process the option; otherwise <c>false</c> to ignore it.</value>
		public bool IsFloating {
			get => isFloating;
			set {
				if (isFloating != value) {
					isFloating = value;
					NotifyPropertyChanged(nameof(IsFloating));
				}
			}
		}

		/// <summary>
		/// Gets or sets if the option will be processed when serializing or deserializing.
		/// </summary>
		/// <value><c>true</c> to process the option; otherwise <c>false</c> to ignore it.</value>
		public bool IsVisible {
			get => isVisible;
			set {
				if (isVisible != value) {
					isVisible = value;
					NotifyPropertyChanged(nameof(IsVisible));
				}
			}
		}

		/// <summary>
		/// Gets or sets if the option will be processed when serializing or deserializing.
		/// </summary>
		/// <value><c>true</c> to process the option; otherwise <c>false</c> to ignore it.</value>
		public bool LineIndex {
			get => lineIndex;
			set {
				if (lineIndex != value) {
					lineIndex = value;
					NotifyPropertyChanged(nameof(LineIndex));
				}
			}
		}

		/// <summary>
		/// Gets or sets if the option will be processed when serializing or deserializing.
		/// </summary>
		/// <value><c>true</c> to process the option; otherwise <c>false</c> to ignore it.</value>
		public bool Offset {
			get => offset;
			set {
				if (offset != value) {
					offset = value;
					NotifyPropertyChanged(nameof(Offset));
				}
			}
		}

		/// <summary>
		/// Gets or sets if the option will be processed when serializing or deserializing.
		/// </summary>
		/// <value><c>true</c> to process the option; otherwise <c>false</c> to ignore it.</value>
		public bool Placement {
			get => placement;
			set {
				if (placement != value) {
					placement = value;
					NotifyPropertyChanged(nameof(Placement));
				}
			}
		}

		/// <summary>
		/// Gets or sets if the option will be processed when serializing or deserializing.
		/// </summary>
		/// <value><c>true</c> to process the option; otherwise <c>false</c> to ignore it.</value>
		public bool SortOrder {
			get => sortOrder;
			set {
				if (sortOrder != value) {
					sortOrder = value;
					NotifyPropertyChanged(nameof(SortOrder));
				}
			}
		}

	}
}
