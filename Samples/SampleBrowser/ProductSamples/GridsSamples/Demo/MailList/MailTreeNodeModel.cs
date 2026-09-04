using ActiproSoftware.ProductSamples.GridsSamples.Common;

namespace ActiproSoftware.ProductSamples.GridsSamples.Demo.MailList;

/// <summary>
/// Provides a tree node model implementation for a mail.
/// </summary>
public class MailTreeNodeModel : TreeNodeModel {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The author.
	/// </summary>
	public string? Author { get; set; }

	/// <summary>
	/// The date/time.
	/// </summary>
	public DateTime DateTime { get; set; }

	/// <summary>
	/// The date/time text.
	/// </summary>
	public string DateTimeText {
		get {
			return (DateTime.Date == DateTime.Today)
				? DateTime.ToShortTimeString()
				: DateTime.ToShortDateString();
		}
	}

	/// <summary>
	/// Indicates whether the mail is flagged.
	/// </summary>
	public bool IsFlagged { get; set; }

	/// <summary>
	/// The text.
	/// </summary>
	public string? Text { get; set; }

}
