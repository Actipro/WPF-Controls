using System;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Parsing.LLParser;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted06 {
	
	/// <summary>
	/// Provides a <c>Simple</c> language outliner service.
	/// </summary>
	public class SimpleOutliner : IOutliner {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Returns an <see cref="IOutliningSource"/> for the specified <see cref="ITextSnapshot"/>.
		/// </summary>
		/// <param name="snapshot">The <see cref="ITextSnapshot"/> for which to return an outlining source.</param>
		/// <returns>An <see cref="IOutliningSource"/> for the specified <see cref="ITextSnapshot"/>.</returns>
		public IOutliningSource GetOutliningSource(ITextSnapshot snapshot) {
			if (snapshot != null) {
				ICodeDocument document = snapshot.Document as ICodeDocument;
				if (document != null) {
					// Get the parse data
					ILLParseData parseData = document.ParseData as ILLParseData;
					if (parseData != null) {
						// Create an outlining source based on the parse data
						SimpleOutliningSource source = new SimpleOutliningSource(snapshot, parseData);

						// Translate the data to the desired snapshot, which could be slightly newer than the parsed source
						source.TranslateTo(snapshot);

						return source;
					}
				}
			}
			return null;
		}
		
		/// <summary>
		/// Gets an <see cref="AutomaticOutliningUpdateTrigger"/> indicating whether automatic outlining should update
		/// in the main thread following a text change, or rather be driven via parse data results.
		/// </summary>
		/// <value>An <see cref="AutomaticOutliningUpdateTrigger"/> indicating the trigger by which automatic outlining should update.</value>
		public AutomaticOutliningUpdateTrigger UpdateTrigger { 
			get {
				return AutomaticOutliningUpdateTrigger.ParseDataChanged;
			}
		}

	}

}
