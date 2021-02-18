using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Media;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Implementation;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Tagging.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsCustomDecorator {
    
	/// <summary>
	/// Provides a custom <see cref="ITag"/> implementation.
	/// </summary>
	/// <remarks>
	/// Normally a custom tag would store some data as well, but in this sample we are just illustrating
	/// how to create a custom tag.
	/// </remarks>
	public class CustomTag : ITag {}
	
}
