using ActiproSoftware.Windows.Controls.Ribbon.Controls;
using ActiproSoftware.Windows.DocumentManagement;
using System;

namespace ActiproSoftware.ProductSamples.RibbonSamples.Common {

	/// <summary>
	/// Represents a Word 2007-like <see cref="ApplicationMenu"/> implementation that can easily be included in Ribbon QuickStarts.
	/// </summary>
	public partial class WordApplicationMenu : ApplicationMenu {

		// --------------------------------------------------------------------------------------------------
		// OBJECT
		// --------------------------------------------------------------------------------------------------

		/// <summary>
		/// Initializes an instance of the class.
		/// </summary>
		public WordApplicationMenu() {
			InitializeComponent();

			var rand = new Random();
			DateTime dateTime = DateTime.Now;
			recentDocManager.Documents.BeginUpdate();
			for (int index = 0; index < 10; index++) {
				var docRef = new DocumentReference(new Uri(string.Format(@"C:\Documents\Another document {0}.rtf", index + 1))) {
					LastOpenedDateTime = dateTime
				};
				if (rand.NextDouble() < 0.35)
					docRef.IsPinnedRecentDocument = true;
				recentDocManager.Documents.Add(docRef);

				dateTime = dateTime.AddDays(-3 * rand.NextDouble());
			}
			recentDocManager.Documents.EndUpdate();
		}
	}
}
