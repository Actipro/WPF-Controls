using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using ActiproSoftware.ProductSamples.DockingSamples.Common;
using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Input;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStart.MvvmDocumentWindows {

	/// <summary>
	/// Represents the main view-model.
	/// </summary>
	public class MainViewModel : ObservableObjectBase {

		private int documentIndex = 1;
		private DeferrableObservableCollection<DocumentItemViewModel> documentItems = new DeferrableObservableCollection<DocumentItemViewModel>();

		private DelegateCommand<object> activateNextDocumentCommand;
		private DelegateCommand<object> closeActiveDocumentCommand;
		private DelegateCommand<object> createNewImageDocumentCommand;
		private DelegateCommand<object> createNewTextDocumentCommand;
		private DelegateCommand<object> selectFirstDocumentCommand;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="MainViewModel"/> class.
		/// </summary>
		public MainViewModel() {
			this.CreateNewTextDocument(false);
			this.CreateNewTextDocument(false);
			this.CreateNewImageDocument(false);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the activate next document command.
		/// </summary>
		/// <value>The activate next document command.</value>
		public ICommand ActivateNextDocumentCommand {
			get {
				if (activateNextDocumentCommand == null)
					activateNextDocumentCommand = new DelegateCommand<object>(
						(param) => {
							if (documentItems.Count > 0) {
								var index = 0;
								var activeDocumentItem = documentItems.FirstOrDefault(d => d.IsActive);
								if (activeDocumentItem != null)
									index = documentItems.IndexOf(activeDocumentItem) + 1;
								if (index >= documentItems.Count)
									index = 0;

								documentItems[index].IsActive = true;
							}
						}
					);

				return activateNextDocumentCommand;
			}
		}

		/// <summary>
		/// Gets the close active document command.
		/// </summary>
		/// <value>The close active document command.</value>
		public ICommand CloseActiveDocumentCommand {
			get {
				if (closeActiveDocumentCommand == null)
					closeActiveDocumentCommand = new DelegateCommand<object>(
						(param) => {
							var activeDocumentItem = documentItems.FirstOrDefault(d => d.IsActive);
							if (activeDocumentItem != null)
								documentItems.Remove(activeDocumentItem);
						}
					);

				return closeActiveDocumentCommand;
			}
		}
		
		/// <summary>
		/// Creates a new image document.
		/// </summary>
		/// <param name="activate">Whether to activate the document.</param>
		public void CreateNewImageDocument(bool activate) {
			var viewModel = new ImageDocumentItemViewModel();
			viewModel.SerializationId = String.Format("Document{0}.png", documentIndex);  // NOTE: Every docking window must have a unique SerializationId if you wish to use layout serialization
			viewModel.FileName = String.Format("Document{0}.png", documentIndex++);
			viewModel.Title = viewModel.FileName;
			viewModel.Uri = new Uri("/Images/Icons/Save32.png", UriKind.Relative);

			documentItems.Add(viewModel);

			if (activate)
				viewModel.IsActive = true;
			else
				viewModel.IsOpen = true;
		}
		
		/// <summary>
		/// Creates a new text document.
		/// </summary>
		/// <param name="activate">Whether to activate the document.</param>
		public void CreateNewTextDocument(bool activate) {
			var viewModel = new TextDocumentItemViewModel();
			viewModel.SerializationId = String.Format("Document{0}.txt", documentIndex);  // NOTE: Every docking window must have a unique SerializationId if you wish to use layout serialization
			viewModel.FileName = String.Format("Document{0}.txt", documentIndex++);
			viewModel.Title = viewModel.FileName;
			viewModel.Text = String.Format("Dynamically created at {0}.", DateTime.Now);

			documentItems.Add(viewModel);

			if (activate)
				viewModel.IsActive = true;
			else
				viewModel.IsOpen = true;
		}
		
		/// <summary>
		/// Gets the create new image document command.
		/// </summary>
		/// <value>The create new image document command.</value>
		public ICommand CreateNewImageDocumentCommand {
			get {
				if (createNewImageDocumentCommand == null)
					createNewImageDocumentCommand = new DelegateCommand<object>(
						(param) => {
							this.CreateNewImageDocument(true);
						}
					);

				return createNewImageDocumentCommand;
			}
		}
		
		/// <summary>
		/// Gets the create new text document command.
		/// </summary>
		/// <value>The create new text document command.</value>
		public ICommand CreateNewTextDocumentCommand {
			get {
				if (createNewTextDocumentCommand == null)
					createNewTextDocumentCommand = new DelegateCommand<object>(
						(param) => {
							this.CreateNewTextDocument(true);
						}
					);

				return createNewTextDocumentCommand;
			}
		}
		
		/// <summary>
		/// Gets the document items associated with this view-model.
		/// </summary>
		/// <value>The document items associated with this view-model.</value>
		public IList<DocumentItemViewModel> DocumentItems {
			get {
				return documentItems;
			}
		}

		/// <summary>
		/// Gets the select first document command.
		/// </summary>
		/// <value>The select first document command.</value>
		public ICommand SelectFirstDocumentCommand {
			get {
				if (selectFirstDocumentCommand == null)
					selectFirstDocumentCommand = new DelegateCommand<object>(
						(param) => {
							var documentItem = documentItems.FirstOrDefault();
							if (documentItem != null)
								documentItem.IsSelected = true;
						}
					);

				return selectFirstDocumentCommand;
			}
		}

	}

}
