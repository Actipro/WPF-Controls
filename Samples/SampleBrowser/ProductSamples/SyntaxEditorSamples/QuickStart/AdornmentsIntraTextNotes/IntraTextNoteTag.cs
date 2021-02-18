using System;
using System.Windows;
using System.Windows.Media;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Implementation;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Tagging.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsIntraTextNotes {
    
	/// <summary>
	/// Provides an <see cref="IIntraTextSpacerTag"/> implementation that reserves intra-text space for a note.
	/// </summary>
	public class IntraTextNoteTag : IClassificationTag, IIntraTextSpacerTag {
		
		private static IClassificationType noteAcceptedClassificationType = new ClassificationType("NoteAccepted", "Note (accepted)");
		private static IClassificationType notePendingClassificationType = new ClassificationType("NotePending", "Note (pending)");
		private static IClassificationType noteRejectedClassificationType = new ClassificationType("NoteRejected", "Note (rejected)");
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes the <c>IntraTextNoteTag</c> class.
		/// </summary>
		static IntraTextNoteTag() {
			AmbientHighlightingStyleRegistry.Instance.Register(noteAcceptedClassificationType, new HighlightingStyle(null, Color.FromArgb(0x40, 0x6C, 0xE2, 0x6C)));
			AmbientHighlightingStyleRegistry.Instance.Register(notePendingClassificationType, new HighlightingStyle(null, Color.FromArgb(0x40, 0xFF, 0xF2, 0x0)));
			AmbientHighlightingStyleRegistry.Instance.Register(noteRejectedClassificationType, new HighlightingStyle(null, Color.FromArgb(0x40, 0xE2, 0x6C, 0x6C)));
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
