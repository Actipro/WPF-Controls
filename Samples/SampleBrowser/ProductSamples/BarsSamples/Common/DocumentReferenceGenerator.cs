using ActiproSoftware.SampleBrowser;
using ActiproSoftware.Windows.DocumentManagement;
using System;
using System.Windows.Media.Imaging;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common {
	
	/// <summary>
	/// Generates document references for use in samples.
	/// </summary>
	public static class DocumentReferenceGenerator {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Adds a <see cref="DocumentReference"/> to the <see cref="RecentDocumentManager"/>.
		/// </summary>
		/// <param name="manager">The <see cref="RecentDocumentManager"/> to update.</param>
		/// <param name="uri">The <see cref="Uri"/>.</param>
		/// <param name="isPinned">Whether it is pinned.</param>
		private static void AddDocumentReference(RecentDocumentManager manager, Uri uri, bool isPinned) {
			var docRef = new DocumentReference(uri) {
				LastOpenedDateTime = DateTime.Now.AddDays(-0.3 * manager.Documents.Count).AddMinutes(new Random().NextDouble() * -200),
				IsPinnedRecentDocument = isPinned,
			};

			var fileExt = System.IO.Path.GetExtension(uri.LocalPath).ToLowerInvariant();
			switch (fileExt) {
				case ".rtf":
					docRef.Description = "Rich-text file";
					docRef.ImageSourceSmall = ImageLoader.GetIcon("RichTextDocument16.png");
					docRef.ImageSourceLarge = ImageLoader.GetIcon("RichTextDocument32.png");
					break;
				default:
					docRef.Description = fileExt.Substring(1).ToUpperInvariant() + " file";
					docRef.ImageSourceSmall = ImageLoader.GetIcon("BlankPage16.png");
					docRef.ImageSourceLarge = ImageLoader.GetIcon("BlankPage32.png");
					break;
			}

			manager.Documents.Add(docRef);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Adds a number of <see cref="DocumentReference"/> objects to a <see cref="RecentDocumentManager"/>.
		/// </summary>
		/// <param name="manager">The <see cref="RecentDocumentManager"/> to update.</param>
		public static void BindRecentDocumentManager(RecentDocumentManager manager) {
			manager.Documents.BeginUpdate();
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Work\Software\EULA.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Work\Resume.rtf"), true);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Personal\Vacation Planning.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Personal\Investment Notes.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Personal\Holiday Recipes.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Work\Software\Release Notes.rtf"), true);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Work\Financial Report Q4.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Personal\Birthday Gift Ideas.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Work\Financial Report Q3.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Personal\Untitled Novel - A story about long file names and their impact on user interfaces.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Downloads\Actipro Software - Getting Started.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Downloads\How to Deliver Faster with Reusable Components.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Work\Software\Feature Requests.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Work\Financial Report Q2.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Personal\Bathroom Remodel Budget.rtf"), true);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Work\Financial Report Q1.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Work\Side Project Domain Names.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Work\Sales Presentation Notes.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Personal\Wish List.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Work\Privacy Policy.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\TODO List.rtf"), true); // Oldest document, but pinned for importance
			manager.Documents.EndUpdate();
		}

	}

}
