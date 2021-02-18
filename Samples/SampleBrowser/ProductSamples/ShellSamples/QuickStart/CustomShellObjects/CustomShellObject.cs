using System.Windows.Media;
using ActiproSoftware.Shell;

namespace ActiproSoftware.ProductSamples.ShellSamples.QuickStart.CustomShellObjects {
	
	/// <summary>
	/// Represents a custom <see cref="IShellObject"/> implementation.
	/// </summary>
	public class CustomShellObject : ShellObjectBase {

		private ImageSource extraLargeIcon;
		private ImageSource extraLargeIconOverlay;
		private ImageSource extraLargeThumbnail;
		private ShellObjectKind kind;
		private ImageSource largeIcon;
		private ImageSource largeIconOverlay;
		private ImageSource largeThumbnail;
		private ImageSource mediumIcon;
		private ImageSource mediumIconOverlay;
		private ImageSource mediumThumbnail;
		private string name;
		private string parsingName;
		private string relativeParsingName;
		private string editingName;
		private ImageSource smallIcon;
		private ImageSource smallIconOverlay;
		private object toolTip;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <see cref="CustomShellObject"/> class.
		/// </summary>
		/// <param name="shellService">The <see cref="IShellService"/> used to return this shell object's children.</param>
		/// <param name="kind">The kind of shell object.</param>
		/// <param name="name">The name of the shell object.</param>
		/// <param name="parsingName">The full parsing name of the shell object, if available.</param>
		/// <param name="relativeParsingName">Optionally define the relative parsing name of the shell object used as the individual part of a full parsing name, if different than <paramref name="name"/>.</param>
		/// <param name="editingName">Optionally define the user-friendly editing name of the shell object, if different than <paramref name="parsingName"/>.</param>
		public CustomShellObject(IShellService shellService, ShellObjectKind kind, string name, string parsingName, string relativeParsingName = null, string editingName = null) : base(shellService) {
			this.kind = kind;
			this.name = name;
			this.parsingName = parsingName;
			this.relativeParsingName = relativeParsingName;
			this.editingName = editingName;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the full user-friendly editing name of the shell object.
		/// </summary>
		/// <value>The full user-friendly editing name of the shell object.</value>
		public override string EditingName {
			get {
				return editingName ?? ParsingName;
			}
		}

		/// <summary>
		/// Gets or sets the extra-large icon.
		/// </summary>
		/// <value>The extra-large icon.</value>
		public override ImageSource ExtraLargeIcon {
			get {
				return extraLargeIcon;
			}
			set {
				if (extraLargeIcon != value) {
					extraLargeIcon = value;
					this.NotifyPropertyChanged("ExtraLargeIcon");
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the extra-large icon overlay.
		/// </summary>
		/// <value>The extra-large icon overlay.</value>
		public override ImageSource ExtraLargeIconOverlay {
			get {
				return extraLargeIconOverlay;
			}
			set {
				if (extraLargeIconOverlay != value) {
					extraLargeIconOverlay = value;
					this.NotifyPropertyChanged("ExtraLargeIconOverlay");
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the extra-large thumbnail.
		/// </summary>
		/// <value>The extra-large thumbnail.</value>
		public override ImageSource ExtraLargeThumbnail {
			get {
				return extraLargeThumbnail;
			}
			set {
				if (extraLargeThumbnail != value) {
					extraLargeThumbnail = value;
					this.NotifyPropertyChanged("ExtraLargeThumbnail");
				}
			}
		}
		
		/// <summary>
		/// Gets a <see cref="ShellObjectKind"/> that specifies the kind of this shell object.
		/// </summary>
		/// <value>A <see cref="ShellObjectKind"/> that specifies the kind of this shell object.</value>
		public override ShellObjectKind Kind {
			get {
				return kind;
			}
		}
		
		/// <summary>
		/// Gets or sets the large icon.
		/// </summary>
		/// <value>The large icon.</value>
		public override ImageSource LargeIcon {
			get {
				return largeIcon;
			}
			set {
				if (largeIcon != value) {
					largeIcon = value;
					this.NotifyPropertyChanged("LargeIcon");
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the large icon overlay.
		/// </summary>
		/// <value>The large icon overlay.</value>
		public override ImageSource LargeIconOverlay {
			get {
				return largeIconOverlay;
			}
			set {
				if (largeIconOverlay != value) {
					largeIconOverlay = value;
					this.NotifyPropertyChanged("LargeIconOverlay");
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the large thumbnail.
		/// </summary>
		/// <value>The large thumbnail.</value>
		public override ImageSource LargeThumbnail {
			get {
				return largeThumbnail;
			}
			set {
				if (largeThumbnail != value) {
					largeThumbnail = value;
					this.NotifyPropertyChanged("LargeThumbnail");
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the medium icon.
		/// </summary>
		/// <value>The medium icon.</value>
		public override ImageSource MediumIcon {
			get {
				return mediumIcon;
			}
			set {
				if (mediumIcon != value) {
					mediumIcon = value;
					this.NotifyPropertyChanged("MediumIcon");
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the medium icon overlay.
		/// </summary>
		/// <value>The medium icon overlay.</value>
		public override ImageSource MediumIconOverlay {
			get {
				return mediumIconOverlay;
			}
			set {
				if (mediumIconOverlay != value) {
					mediumIconOverlay = value;
					this.NotifyPropertyChanged("MediumIconOverlay");
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the medium thumbnail.
		/// </summary>
		/// <value>The medium thumbnail.</value>
		public override ImageSource MediumThumbnail {
			get {
				return mediumThumbnail;
			}
			set {
				if (mediumThumbnail != value) {
					mediumThumbnail = value;
					this.NotifyPropertyChanged("MediumThumbnail");
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the name of the shell object.
		/// </summary>
		/// <value>The name of the shell object.</value>
		public override string Name {
			get {
				return name;
			}
			set {
				if (name != value) {
					name = value;
					this.NotifyPropertyChanged("Name");
				}
			}
		}
		
		/// <summary>
		/// Gets the full parsing name of the shell object, if available.
		/// </summary>
		/// <value>The full parsing name of the shell object, if available.</value>
		public override string ParsingName {
			get {
				return parsingName;
			}
		}

		/// <summary>
		/// Gets the parent-relative parsing name of the shell object, if available.
		/// </summary>
		/// <value>The parent-relative parsing name of the shell object, if available.</value>
		/// <remarks>
		/// This value is usually the same as <see cref="Name"/>, except that it can also return a special syntax to identify special shell objects.
		/// </remarks>
		public override string RelativeParsingName {
			get {
				return relativeParsingName ?? base.RelativeParsingName;
			}
		}

		/// <summary>
		/// Gets or sets the small icon.
		/// </summary>
		/// <value>The small icon.</value>
		public override ImageSource SmallIcon {
			get {
				return smallIcon;
			}
			set {
				if (smallIcon != value) {
					smallIcon = value;
					this.NotifyPropertyChanged("SmallIcon");
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the small icon overlay.
		/// </summary>
		/// <value>The small icon overlay.</value>
		public override ImageSource SmallIconOverlay {
			get {
				return smallIconOverlay;
			}
			set {
				if (smallIconOverlay != value) {
					smallIconOverlay = value;
					this.NotifyPropertyChanged("SmallIconOverlay");
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the tool-tip object that is displayed for this shell object in the user interface (UI).
		/// </summary>
		/// <value>The tool-tip object.</value>
		public override object ToolTip {
			get {
				return toolTip;
			}
			set {
				if (toolTip != value) {
					toolTip = value;
					this.NotifyPropertyChanged("ToolTip");
				}
			}
		}

	}

}