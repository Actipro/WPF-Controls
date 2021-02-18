using System.ComponentModel;
using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Controls;
using System.Collections.Generic;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridDynamicProperties {
	
	/// <summary>
	/// Specifies where output should be displayed.
	/// </summary>
	public class DisplayOutput : ObservableObjectBase, IDynamicPropertyStateProvider {

		private int height = DefaultHeight;
		private string location = LocationPrimary;
		private ScreenOrientation orientation;
		private DisplayTarget target;
		private int width = DefaultWidth;

		private const int DefaultWidth = 1920;
		private const int DefaultHeight = 1080;

		private const string LocationPrimary = "Primary";
		private const string LocationSecondary = "Secondary";
		private const string LocationTertiary = "Tertiary";

		private const string LocationBottomLeft = "Bottom-left";
		private const string LocationBottomRight = "Bottom-right";
		private const string LocationTopLeft = "Top-left";
		private const string LocationTopRight = "Top-right";

		private string[] PaneLocations = new string[] { LocationTopLeft, LocationTopRight, LocationBottomRight, LocationBottomLeft };
		private string[] ScreenLocations = new string[] { LocationPrimary, LocationSecondary, LocationTertiary };
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>DisplayOutput</c> class.
		/// </summary>
		public DisplayOutput() {
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Updates the location.
		/// </summary>
		private void UpdateLocation() {
			if (this.target == DisplayTarget.Screen)
				this.Location = LocationPrimary;
			else
				this.Location = LocationTopRight;
		}
		
		/// <summary>
		/// Updates the size properties.
		/// </summary>
		private void UpdateSize() {
			if (this.target == DisplayTarget.Screen) {
				this.Width = (orientation == ScreenOrientation.Landscape ? DefaultWidth : DefaultHeight);
				this.Height = (orientation == ScreenOrientation.Landscape ? DefaultHeight : DefaultWidth);
			}
			else {
				this.Width = DefaultWidth / 4;
				this.Height = DefaultHeight / 4;
			}
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets whether the property supports dynamic <see cref="StandardValues"/>.
		/// </summary>
		/// <param name="propertyName">The name of the property to examine.</param>
		/// <returns>
		/// <c>true</c> if the property supports dynamic <see cref="StandardValues"/>; otherwise, <c>false</c>.
		/// </returns>
		public bool GetPropertyHasStandardValues(string propertyName) {
			switch (propertyName) {
				case "Location":
					return true;
				default:
					return false;
			}
		}

		/// <summary>
		/// Gets a value indicating whether the specified property is read-only.
		/// </summary>
		/// <param name="propertyName">The name of the property to examine.</param>
		/// <returns>
		/// <c>true</c> if the specified property is read-only; otherwise, <c>false</c>.
		/// </returns>
		public bool GetPropertyReadOnly(string propertyName) {
			switch (propertyName) {
				case "Height":
				case "Width":
					return (this.Target != DisplayTarget.Pane);
				default:
					return false;
			}
		}
		
		/// <summary>
		/// Gets the standard list of values for the <see cref="Value"/> property.
		/// </summary>
		/// <param name="propertyName">The name of the property to examine.</param>
		/// <returns>
		/// The standard list of values for the <see cref="Value"/> property.
		/// </returns>
		public IEnumerable<object> GetPropertyStandardValues(string propertyName) {
			switch (propertyName) {
				case "Location": 
					return (this.Target == DisplayTarget.Screen ? ScreenLocations : PaneLocations);
				default:
					return null;
			}
		}

		/// <summary>
		/// Gets a value indicating whether the specified property is visible.
		/// </summary>
		/// <param name="propertyName">The name of the property to examine.</param>
		/// <returns>
		/// <c>true</c> if the specified property is visible; otherwise, <c>false</c>.
		/// </returns>
		public bool GetPropertyVisibility(string propertyName) {
			switch (propertyName) {
				case "ScreenProfile":
				case "Orientation":
					return (this.Target == DisplayTarget.Screen);
				default:
					return true;
			}
		}
		
		/// <summary>
		/// Gets or sets the display target.
		/// </summary>
		/// <value>The display target.</value>
		[Description("The display target, which is either a full screen or a pane within the current monitor.")]
		public DisplayTarget Target {
			get {
				return this.target;
			}
			set {
				if (this.target != value) {
					this.target = value;
					this.NotifyPropertyChanged("Target");
					
					this.NotifyPropertyChanged("Orientation");

					this.UpdateLocation();
					this.UpdateSize();
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the location.
		/// </summary>
		/// <value>The location.</value>
		[Description("The location of the screen (which screen) or pane (which corner).  This property has dynamic standard values based on the Target property selection.")]
		public string Location {
			get {
				return this.location;
			}
			set {
				if (this.location != value) {
					this.location = value;
					this.NotifyPropertyChanged("Location");
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the screen orientation.
		/// </summary>
		/// <value>The screen orientation.</value>
		[Description("The screen orientation.  This property is only visible when the Target is a Screen.")]
		public ScreenOrientation Orientation {
			get {
				return this.orientation;
			}
			set {
				if (this.orientation != value) {
					this.orientation = value;
					this.NotifyPropertyChanged("Orientation");

					this.UpdateSize();
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the width.
		/// </summary>
		/// <value>The width.</value>
		[Description("The width of the screen or pane.  This property is read-only when the Target is a Screen.")]
		public int Width {
			get {
				return this.width;
			}
			set {
				if (this.width != value) {
					this.width = value;
					this.NotifyPropertyChanged("Width");
				}
			}
		}

		/// <summary>
		/// Gets or sets the height.
		/// </summary>
		/// <value>The height.</value>
		[Description("The height of the screen or pane.  This property is read-only when the Target is a Screen.")]
		public int Height {
			get {
				return this.height;
			}
			set {
				if (this.height != value) {
					this.height = value;
					this.NotifyPropertyChanged("Height");
				}
			}
		}

	}

}
