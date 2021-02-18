using ActiproSoftware.Text;
using System.Activities.Presentation.Model;
using System.Collections.Generic;

namespace ActiproSoftware.Windows.WindowsWorkflowIntegration.ExpressionEditing {

	public interface IExpressionEditorSyntaxLanguage : ISyntaxLanguage {

		/// <summary>
		/// Returns the header text that for parsing purposes will surround the visible document's text.
		/// </summary>
		/// <returns>The header text.</returns>
		/// <remarks>
		/// This method combined with <see cref="GetFooterText"/> surround the document text to create a complete compilation unit.
		/// </remarks>
		string GetHeaderText(IEnumerable<ModelItem> variableModels);

		/// <summary>
		/// Returns the footer text that for parsing purposes will surround the visible document's text.
		/// </summary>
		/// <returns>The footer text.</returns>
		/// <remarks>
		/// This method combined with <see cref="GetHeaderText"/> surround the document text to create a complete compilation unit.
		/// </remarks>
		string GetFooterText();

	}

}
