using System;
using System.Collections.Generic;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Parsing;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted02 {
    
	/// <summary>
	/// Represents the parsing results for a <see cref="SimpleParser"/>.
	/// </summary>
    public class SimpleParseData : IParseData {

		private List<TextSnapshotRange> functions = new List<TextSnapshotRange>();
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the list of available functions.
		/// </summary>
		/// <value>The list of available functions.</value>
		public IList<TextSnapshotRange> Functions {
			get {
				return functions;
			}
		}
		
    }
}
