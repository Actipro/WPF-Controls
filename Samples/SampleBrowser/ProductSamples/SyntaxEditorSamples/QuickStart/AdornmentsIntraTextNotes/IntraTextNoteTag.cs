using System;
using System.Windows;
using System.Windows.Input;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Implementation;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting.Implementation;
using ActiproSoftware.Windows.Media;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsIntraTextNotes {
    
	/// <summary>
	/// Provides an <see cref="IIntraTextSpacerTag"/> implementation that reserves intra-text space for a note.
	/// </summary>
	public class IntraTextNoteTag : IClassificationTag, IIntraTextSpacerTag {
		
		private static readonly IClassificationType noteAcceptedClassificationType = new ClassificationType("NoteAccepted", "Note (accepted)");
		private static readonly IClassificationType notePendingClassificationType = new ClassificationType("NotePending", "Note (pending)");
		private static readonly IClassificationType noteRejectedClassificationType = new ClassificationType("NoteRejected", "Note (rejected)");
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes the <c>IntraTextNoteTag</c> class.
		/// </summary>
		static IntraTextNoteTag() {
			// This sample assumes the editor will use the AmbientHighlightingStyleRegistry
			var registry = AmbientHighlightingStyleRegistry.Instance;

			// Configure light/dark color palettes with default colors
			registry.LightColorPalette?.SetBackground(noteAcceptedClassificationType.Key, UIColor.FromWebColor("#ebf1dd"));
			registry.LightColorPalette?.SetBackground(notePendingClassificationType.Key, UIColor.FromWebColor("#ffee62"));
			registry.LightColorPalette?.SetBackground(noteRejectedClassificationType.Key, UIColor.FromWebColor("#ffcccc"));
			registry.DarkColorPalette?.SetBackground(noteAcceptedClassificationType.Key, UIColor.FromWebColor("#265e4d"));
			registry.DarkColorPalette?.SetBackground(notePendingClassificationType.Key, UIColor.FromWebColor("#6f5a00"));
			registry.DarkColorPalette?.SetBackground(noteRejectedClassificationType.Key, UIColor.FromWebColor("#3c0000"));

			// Associate a default style with the classification type
			// and the current color palette color will be automatically applied
			registry.Register(noteAcceptedClassificationType, new HighlightingStyle());
			registry.Register(notePendingClassificationType, new HighlightingStyle());
			registry.Register(noteRejectedClassificationType, new HighlightingStyle());
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the note author.
		/// </summary>
		/// <value>The note author.</value>
		public string Author { get; set; }

		/// <summary>
		/// Gets or sets the spacer baseline.
		/// </summary>
		/// <value>The spacer baseline.</value>
		public double Baseline { get; set; }
		
		/// <summary>
		/// Gets the <see cref="IClassificationType"/> associated with this tag.
		/// </summary>
		/// <value>The <see cref="IClassificationType"/> associated with this tag.</value>
		public IClassificationType ClassificationType { 
			get {
				switch (this.Status) {
					case ReviewStatus.Accepted:
						return noteAcceptedClassificationType;
					case ReviewStatus.Rejected:
						return noteRejectedClassificationType;
					default:
						return notePendingClassificationType;
				}
			}
		}

		/// <summary>
		/// Gets or sets the note creation time.
		/// </summary>
		/// <value>The note creation time.</value>
		public DateTime Created { get; set; }
		
		/// <summary>
		/// Gets or sets whether the spacer appears before the tagged range.
		/// </summary>
		/// <value>
		/// <c>true</c> if the spacer appears before the tagged range; otherwise, <c>false</c>.
		/// </value>
		public bool IsSpacerBefore { get; set; } = true;

		/// <summary>
		/// Gets or sets an object that can be used to uniquely identify the spacer.
		/// </summary>
		/// <value>An object that can be used to uniquely identify the spacer.</value>
		public object Key { get; set; }

		/// <summary>
		/// Gets or sets the note message.
		/// </summary>
		/// <value>The note message.</value>
		public string Message { get; set; }

		/// <summary>
		/// Gets or sets the spacer size.
		/// </summary>
		/// <value>The spacer size.</value>
		public Size Size { get; set; }
	
		/// <summary>
		/// Gets or sets the review status.
		/// </summary>
		/// <value>The review status.</value>
		public ReviewStatus Status { get; set; }
	
	}
	
}
