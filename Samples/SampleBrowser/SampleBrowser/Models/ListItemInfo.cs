using System;
using System.Windows.Media;

namespace ActiproSoftware.SampleBrowser {

    /// <summary>
    /// Provides information about a list item.
    /// </summary>
    public class ListItemInfo {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the blurb text.
		/// </summary>
		/// <value>The blurb text.</value>
		public string BlurbText { get; set; }

		/// <summary>
		/// Gets whether there is any blurb text.
		/// </summary>
		/// <value>
		/// <c>true</c> if there is any blurb text; otherwise, <c>false</c>.
		/// </value>
		public bool HasBlurbText {
			get {
				return !string.IsNullOrEmpty(this.BlurbText);
			}
		}

        /// <summary>
        /// Gets or sets the <see cref="ImageSource"/> to display.
        /// </summary>
        /// <value>The <see cref="ImageSource"/> to display.</value>
        public ImageSource ImageSource { get; set; }

		/// <summary>
		/// Gets whether the linked item is external.
		/// </summary>
		/// <value>
		/// <c>true</c> if the linked item is external; otherwise, <c>false</c>.
		/// </value>
		public bool IsExternal {
			get {
				return ((this.TargetUri != null) && (this.TargetUri.Query != null) && (this.TargetUri.Query.Contains("action=open")));
			}
		}
		
        /// <summary>
        /// Gets or sets the target <see cref="Uri"/> if a link should be created.
        /// </summary>
        /// <value>The target <see cref="Uri"/> if a link should be created.</value>
        public Uri TargetUri { get; set; }
		
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

    }

}
