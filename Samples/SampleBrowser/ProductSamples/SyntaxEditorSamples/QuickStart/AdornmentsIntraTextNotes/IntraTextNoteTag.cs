using ActiproSoftware.Text;
using ActiproSoftware.Text.Implementation;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting.Implementation;
using ActiproSoftware.Windows.Media;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsIntraTextNotes;

/// <summary>
/// Provides an <see cref="IIntraTextSpacerTag"/> implementation that reserves intra-text space for a note.
/// </summary>
public class IntraTextNoteTag : IClassificationTag, IIntraTextSpacerTag {

	private static readonly ClassificationType _noteAcceptedClassificationType = new("NoteAccepted", "Note (accepted)");
	private static readonly ClassificationType _notePendingClassificationType = new("NotePending", "Note (pending)");
	private static readonly ClassificationType _noteRejectedClassificationType = new("NoteRejected", "Note (rejected)");

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes the class.
	/// </summary>
	static IntraTextNoteTag() {
		// This sample assumes the editor will use the AmbientHighlightingStyleRegistry
		var registry = AmbientHighlightingStyleRegistry.Instance;

		// Configure light/dark color palettes with default colors
		registry.LightColorPalette?.SetBackground(_noteAcceptedClassificationType.Key, UIColor.FromWebColor("#ebf1dd"));
		registry.LightColorPalette?.SetBackground(_notePendingClassificationType.Key, UIColor.FromWebColor("#ffee62"));
		registry.LightColorPalette?.SetBackground(_noteRejectedClassificationType.Key, UIColor.FromWebColor("#ffcccc"));
		registry.DarkColorPalette?.SetBackground(_noteAcceptedClassificationType.Key, UIColor.FromWebColor("#265e4d"));
		registry.DarkColorPalette?.SetBackground(_notePendingClassificationType.Key, UIColor.FromWebColor("#6f5a00"));
		registry.DarkColorPalette?.SetBackground(_noteRejectedClassificationType.Key, UIColor.FromWebColor("#3c0000"));

		// Associate a default style with the classification type
		//   and the current color palette color will be automatically applied
		registry.Register(_noteAcceptedClassificationType, new HighlightingStyle());
		registry.Register(_notePendingClassificationType, new HighlightingStyle());
		registry.Register(_noteRejectedClassificationType, new HighlightingStyle());
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The note author.
	/// </summary>
	public string? Author { get; set; }

	/// <inheritdoc cref="IIntraTextSpacerTag.Baseline"/>
	public double Baseline { get; set; }

	/// <summary>
	/// The <see cref="IClassificationType"/> associated with this tag.
	/// </summary>
	public IClassificationType ClassificationType {
		get => Status switch {
			ReviewStatus.Accepted => _noteAcceptedClassificationType,
			ReviewStatus.Rejected => _noteRejectedClassificationType,
			ReviewStatus.Pending or _ => _notePendingClassificationType
		};
	}

	/// <summary>
	/// The note creation time.
	/// </summary>
	public DateTime Created { get; set; }

	/// <inheritdoc cref="IIntraTextSpacerTag.IsSpacerBefore"/>
	public bool IsSpacerBefore { get; set; } = true;

	/// <inheritdoc cref="IIntraTextSpacerTag.Key"/>
	public object? Key { get; set; }

	/// <summary>
	/// The note message.
	/// </summary>
	public string? Message { get; set; }

	/// <inheritdoc cref="IIntraTextSpacerTag.Size"/>
	public Size Size { get; set; }

	/// <summary>
	/// The review status.
	/// </summary>
	public ReviewStatus Status { get; set; }

}
