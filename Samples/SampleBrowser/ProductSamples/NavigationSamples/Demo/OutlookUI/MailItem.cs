using System;

namespace ActiproSoftware.ProductSamples.NavigationSamples.Demo.NavigationBarIntro {

	/// <summary>
	/// Provides information about a mail item.
	/// </summary>
	public class MailItem {

		private string		from;
		private bool		isFlagged;
		private DateTime	sent;
		private string		subject;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets who the mail is from.
		/// </summary>
		/// <value>Who the mail is from.</value>
		public string From {
			get {
				return from;
			}
			set {
				from = value;
			}
		}

		/// <summary>
		/// Gets or sets whether the mail is flagged.
		/// </summary>
		/// <value>
		/// <c>true</c> if the mail is flagged; otherwise, <c>false</c>.
		/// </value>
		public bool IsFlagged {
			get {
				return isFlagged;
			}
			set {
				isFlagged = value;
			}
		}

		/// <summary>
		/// Gets or sets when the mail was sent.
		/// </summary>
		/// <value>When the mail was sent.</value>
		public DateTime Sent {
			get {
				return sent;
			}
			set {
				sent = value;
			}
		}

		/// <summary>
		/// Gets or sets the subject of the mail.
		/// </summary>
		/// <value>The subject of the mail.</value>
		public string Subject {
			get {
				return subject;
			}
			set {
				subject = value;
			}
		}

	}
}
