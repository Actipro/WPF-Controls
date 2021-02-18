using System;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Languages.CSharp.Implementation;
using ActiproSoftware.Text.Languages.DotNet;
using ActiproSoftware.Text.Languages.DotNet.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.DotNetAddOnServerTags {
	
	/// <summary>
	/// Creates child <c>C#</c> language <see cref="IDotNetContext"/> objects for a <see cref="TextSnapshotOffset"/>.
	/// </summary>
	public class TranslatedCSharpContextFactory : CSharpContextFactory {

		private Func<TextSnapshotOffset, TextSnapshotOffset?> translateFunc;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>TranslatedCSharpContextFactory</c> class.
		/// </summary>
		/// <param name="translateFunc">The snapshot offset translation function.</param>
		public TranslatedCSharpContextFactory(Func<TextSnapshotOffset, TextSnapshotOffset?> translateFunc) {
			if (translateFunc == null)
				throw new ArgumentNullException("translateFunc");

			// Initialize
			this.translateFunc = translateFunc;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Translates the specified snapshot offset to the parse data's <see cref="IParseErrorProvider.Snapshot"/>, if possible, prior to containing AST node lookup.
		/// </summary>
		/// <param name="parseData">The <see cref="IDotNetParseData"/> to examine.</param>
		/// <param name="snapshotOffset">The <see cref="TextSnapshotOffset"/> to translate.</param>
		/// <returns>The translated offset.</returns>
		protected override TextSnapshotOffset TranslateToParseDataSnapshot(IDotNetParseData parseData, TextSnapshotOffset snapshotOffset) {
			var generatedSnapshotOffset = translateFunc(snapshotOffset);
			if (generatedSnapshotOffset.HasValue)
				return base.TranslateToParseDataSnapshot(parseData, generatedSnapshotOffset.Value);
			else
				return base.TranslateToParseDataSnapshot(parseData, snapshotOffset);
		}

	}

}
