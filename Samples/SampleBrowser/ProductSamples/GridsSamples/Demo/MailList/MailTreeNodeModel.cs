using System;
using ActiproSoftware.ProductSamples.GridsSamples.Common;

namespace ActiproSoftware.ProductSamples.GridsSamples.Demo.MailList {
	
	/// <summary>
	/// Provides a tree node model implementation for a mail.
	/// </summary>
	public class MailTreeNodeModel : TreeNodeModel {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the author.
		/// </summary>
		/// <value>
		/// The author.
		/// </value>
		public string Author { get; set; }
		
		/// <summary>
		/// Gets or sets the date/time.
		/// </summary>
		/// <value>
		/// The date/time.
		/// </value>
		public DateTime DateTime { get; set; }

		/// <summary>
		/// Gets the date/time text.
		/// </summary>
		/// <value>The date/time text.</value>
		public string DateTimeText {
			get {
				if (this.DateTime.Date == DateTime.Today)
					return this.DateTime.ToShortTimeString();
				else
					return this.DateTime.ToShortDateString();
			}
		}
		
		/// <summary>
		/// Gets or sets whether the mail is flagged.
		/// </summary>
		/// <value>
		/// <c>true</c> if the mail is flagged; otherwise, <c>false</c>.
		/// </value>
		public bool IsFlagged { get; set; }

		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		/// <value>
		/// The text.
		/// </value>
		public string Text { get; set; }
		
	}

}
