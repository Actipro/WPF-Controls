namespace ActiproSoftware.ProductSamples.NavigationSamples.Demo.NavigationBarIntro;

/// <summary>
/// Provides information about a mail item.
/// </summary>
public class MailItem {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The sender of the mail.
	/// </summary>
	public string? From { get; set; }

	/// <summary>
	/// Indicates whether the mail is flagged.
	/// </summary>
	public bool IsFlagged { get; set; }

	/// <summary>
	/// When the mail was sent.
	/// </summary>
	public DateTime Sent { get; set; }

	/// <summary>
	/// The subject of the mail.
	/// </summary>
	public string? Subject { get; set; }

}
