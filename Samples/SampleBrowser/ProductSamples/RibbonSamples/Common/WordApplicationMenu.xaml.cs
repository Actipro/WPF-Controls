using System;
using ActiproSoftware.Windows.Controls.Ribbon.Controls;
using ActiproSoftware.Windows.DocumentManagement;

namespace ActiproSoftware.ProductSamples.RibbonSamples.Common {

	/// <summary>
	/// Represents a Word 2007-like <see cref="ApplicationMenu"/> implementation that can easily be included in Ribbon QuickStarts.
	/// </summary>
	public partial class WordApplicationMenu : ApplicationMenu {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>WordApplicationMenu</c> class.
		/// </summary>
		public WordApplicationMenu() {
			InitializeComponent();

			Random rand = new Random();
			DateTime dateTime = DateTime.Now;
			recentDocManager.Documents.BeginUpdate();
			for (int index = 0; index < 10; index++) {
				DocumentReference docRef = new DocumentReference(new Uri(String.Format(@"C:\Documents\Another document {0}.rtf", index + 1)));
				docRef.LastOpenedDateTime = dateTime;
				if (rand.NextDouble() < 0.35)
					docRef.IsPinnedRecentDocument = true;
				recentDocManager.Documents.Add(docRef);

				dateTime = dateTime.AddDays(-3 * rand.NextDouble());
			}
			recentDocManager.Documents.EndUpdate();
		}
	}
}