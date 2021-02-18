using System;
using System.ComponentModel;
using System.IO;
using ActiproSoftware.Text.Searching;
using ActiproSoftware.Windows;

namespace ActiproSoftware.ProductSamples.DockingSamples.Demo.SimpleIde {

	/// <summary>
	/// Stores information about a document.
	/// </summary>
	public class DocumentData : ObservableObjectBase {

		private string fileName;
		private bool isModified;
	
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the file name.
		/// </summary>
		/// <value>The file name.</value>
		[Category("Name")]
		[DisplayName("File name")]
		public string FileName {
			get {
				return fileName;
			}
			set {
				if (fileName != value) {
					fileName = value;
					this.NotifyPropertyChanged("FileName");
					this.NotifyPropertyChanged("Title");
				}
			}
		}

		/// <summary>
		/// Gets or sets whether the document has been modified.
		/// </summary>
		/// <value>
		/// <c>true</c> if the document has been modified; otherwise, <c>false</c>.
		/// </value>
		[Category("State")]
		[DisplayName("Is modified")]
		public bool IsModified {
			get {
				return isModified;
			}
			set {
				if (isModified != value) {
					isModified = value;
					this.NotifyPropertyChanged("IsModified");
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the action to invoke when document outline data is updated.
		/// </summary>
		/// <value>The action to invoke when document outline data is updated.</value>
		[Browsable(false)]
		public Action<EditorDocumentWindow> NotifyDocumentOutlineUpdated { get; set; }

		/// <summary>
		/// Gets or sets the action to invoke when a search occurs.
		/// </summary>
		/// <value>The action to invoke when a search occurs.</value>
		[Browsable(false)]
		public Action<EditorDocumentWindow, ISearchResultSet> NotifySearchAction { get; set; }

		/// <summary>
		/// Gets the title.
		/// </summary>
		/// <value>The title.</value>
		[Category("Name")]
		public string Title {
			get {
				if (string.IsNullOrEmpty(fileName))
					return "Document";
				else
					return Path.GetFileName(fileName);
			}
		}

	}

}
