using System.Windows.Media;

namespace ActiproSoftware.Windows.PrismIntegration.ViewModels {

	/// <summary>
	/// Represents a base class for all docking item view-models.
	/// </summary>
	public abstract class DockingItemViewModelBase : ObservableObjectBase {

		private string description;
		private ImageSource imageSource;
		private bool isActive;
		private bool isOpen;
		private bool isSelected;
		private string serializationId;
		private string title;
		private string windowGroupName;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the description associated with the view-model.
		/// </summary>
		/// <value>The description associated with the view-model.</value>
		public string Description {
			get {
				return this.description;
			}
			set {
				if (this.description != value) {
					this.description = value;
					this.NotifyPropertyChanged("Description");
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the image associated with the view-model.
		/// </summary>
		/// <value>The image associated with the view-model.</value>
		public ImageSource ImageSource {
			get {
				return this.imageSource;
			}
			set {
				if (this.imageSource != value) {
					this.imageSource = value;
					this.NotifyPropertyChanged("ImageSource");
				}
			}
		}
		
		/// <summary>
		/// Gets or sets whether the view is currently active.
		/// </summary>
		/// <value>
		/// <c>true</c> if the view is currently active; otherwise, <c>false</c>.
		/// </value>
		public bool IsActive {
			get {
				return this.isActive;
			}
			set {
				if (this.isActive != value) {
					this.isActive = value;
					this.NotifyPropertyChanged("IsActive");
				}
			}
		}
		
		/// <summary>
		/// Gets or sets whether the view is currently open.
		/// </summary>
		/// <value>
		/// <c>true</c> if the view is currently open; otherwise, <c>false</c>.
		/// </value>
		public bool IsOpen {
			get {
				return this.isOpen;
			}
			set {
				if (this.isOpen != value) {
					this.isOpen = value;
					this.NotifyPropertyChanged("IsOpen");
				}
			}
		}
		
		/// <summary>
		/// Gets or sets whether the view is currently selected in its parent container.
		/// </summary>
		/// <value>
		/// <c>true</c> if the view is currently selected in its parent container; otherwise, <c>false</c>.
		/// </value>
		public bool IsSelected {
			get {
				return this.isSelected;
			}
			set {
				if (this.isSelected != value) {
					this.isSelected = value;
					this.NotifyPropertyChanged("IsSelected");
				}
			}
		}
		
		/// <summary>
		/// Gets whether the container generated for this view model should be a tool window.
		/// </summary>
		/// <value>
		/// <c>true</c> if the container generated for this view model should be a tool window; otherwise, <c>false</c>.
		/// </value>
		public abstract bool IsTool { get; }
		
		/// <summary>
		/// Gets or sets the name that uniquely identifies the view-model for layout serialization.
		/// </summary>
		/// <value>The name that uniquely identifies the view-model for layout serialization.</value>
		public string SerializationId {
			get {
				return this.serializationId;
			}
			set {
				if (this.serializationId != value) {
					this.serializationId = value;
					this.NotifyPropertyChanged("SerializationId");
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the title associated with the view-model.
		/// </summary>
		/// <value>The title associated with the view-model.</value>
		public string Title {
			get {
				return this.title;
			}
			set {
				if (this.title != value) {
					this.title = value;
					this.NotifyPropertyChanged("Title");
				}
			}
		}

		/// <summary>
		/// Gets or sets the window group name associated with the view-model.
		/// </summary>
		/// <value>The window group name associated with the view-model.</value>
		public string WindowGroupName {
			get {
				return this.windowGroupName;
			}
			set {
				if (this.windowGroupName != value) {
					this.windowGroupName = value;
					this.NotifyPropertyChanged("WindowGroupName");
				}
			}
		}

	}

}
