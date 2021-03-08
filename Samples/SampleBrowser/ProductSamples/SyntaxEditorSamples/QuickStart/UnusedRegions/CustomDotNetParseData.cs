using ActiproSoftware.Text;
using ActiproSoftware.Text.Languages.DotNet;
using ActiproSoftware.Text.Languages.DotNet.Ast.Implementation;
using ActiproSoftware.Text.Parsing;
using System;
using System.Collections.Generic;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.UnusedRegions {

	/// <summary>
	/// Stores the results of a .NET language parsing operation.
	/// </summary>
	public class CustomDotNetParseData : IDotNetParseData {

		private IDotNetParseData wrappedParseData;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes an instance of the <c>CustomDotNetParseData</c> class.
		/// </summary>
		public CustomDotNetParseData(IDotNetParseData parseData) {
			if (parseData == null)
				throw new ArgumentNullException(nameof(parseData));

			this.wrappedParseData = parseData;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the object that contains the abstract syntax tree result.
		/// </summary>
		/// <value>The object that contains the abstract syntax tree result.</value>
		public IAstNode Ast {
			get {
				return wrappedParseData.Ast;
			}
		}
		
		/// <summary>
		/// Gets the collection of <see cref="IParseError"/> objects that specify parse errors.
		/// </summary>
		/// <value>The collection of <see cref="IParseError"/> objects that specify parse errors.</value>
		public IEnumerable<IParseError> Errors {
			get {
				return wrappedParseData.Errors;
			}
		}

		/// <summary>
		/// Gets the collection of preprocessor directives.
		/// </summary>
		/// <value>The collection of preprocessor directives.</value>
		public IList<PreprocessorDirective> PreprocessorDirectives {
			get {
				return wrappedParseData.PreprocessorDirectives;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="ITextSnapshot"/>, if known, from which the parse errors were created.
		/// </summary>
		/// <value>The <see cref="ITextSnapshot"/>, if known, from which the parse errors were created.</value>
		public ITextSnapshot Snapshot {
			get {
				return wrappedParseData.Snapshot;
			}
		}

		/// <summary>
		/// Gets or sets a <see cref="NormalizedTextSnapshotRangeCollection"/> containing the unused regions in the snapshot.
		/// </summary>
		/// <value>A <see cref="NormalizedTextSnapshotRangeCollection"/> containing the unused regions in the snapshot.</value>
		public NormalizedTextSnapshotRangeCollection UnusedRanges { get; set; }

	}

}
