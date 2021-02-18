using System;
using System.Windows.Data;
using ActiproSoftware.Windows.Controls.Docking;
using ActiproSoftware.Windows.Extensions;
using ActiproSoftware.Windows.PrismIntegration.ViewModels;

namespace ActiproSoftware.Windows.PrismIntegration.Regions {

	/// <summary>
	/// Provides extensions for binding docking window properties to properties on <see cref="DocumentItemViewModel"/> and <see cref="ToolItemViewModel"/> instances.
	/// </summary>
	public static class DockingWindowBindingExtensions {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
	
		/// <summary>
		/// Clears all bindings from the view-model to the <see cref="DockingWindow"/> container.
		/// </summary>
		/// <param name="container">The container <see cref="DockingWindow"/>.</param>
		public static void ClearContainerBindings(this DockingWindow container) {
			// DockingWindow properties
			container.ClearValue(DockingWindow.DescriptionProperty);
			container.ClearValue(DockingWindow.ImageSourceProperty);
			container.ClearValue(DockingWindow.IsActiveProperty);
			container.ClearValue(DockingWindow.IsOpenProperty);
			container.ClearValue(DockingWindow.IsSelectedProperty);
			container.ClearValue(DockingWindow.SerializationIdProperty);
			container.ClearValue(DockingWindow.TitleProperty);
			container.ClearValue(DockingWindow.WindowGroupNameProperty);
		
			if (container is DocumentWindow) {
				// DocumentWindow properties
				container.ClearValue(DocumentWindow.FileNameProperty);
				container.ClearValue(DocumentWindow.IsReadOnlyProperty);
			}

			if (container is ToolWindow) {
				// ToolWindow properties
				container.ClearValue(ToolWindow.DefaultDockSideProperty);
				container.ClearValue(ToolWindow.StateProperty);
			}
		}
		
		/// <summary>
		/// Prepares all bindings from the view-model to the <see cref="DockingWindow"/> container.
		/// </summary>
		/// <param name="container">The container <see cref="DockingWindow"/>.</param>
		/// <param name="viewModel">The view model.</param>
		public static void PrepareContainerBindings(this DockingWindow container, DockingItemViewModelBase viewModel) {
			if (container == null)
				throw new ArgumentNullException("container");
			if (viewModel == null)
				throw new ArgumentNullException("viewModel");

			// DockingWindow properties
			container.BindToProperty(DockingWindow.DescriptionProperty, viewModel, "Description", BindingMode.TwoWay);
			container.BindToProperty(DockingWindow.ImageSourceProperty, viewModel, "ImageSource", BindingMode.TwoWay);
			container.BindToProperty(DockingWindow.SerializationIdProperty, viewModel, "SerializationId", BindingMode.TwoWay);
			container.BindToProperty(DockingWindow.TitleProperty, viewModel, "Title", BindingMode.TwoWay);
			container.BindToProperty(DockingWindow.WindowGroupNameProperty, viewModel, "WindowGroupName", BindingMode.TwoWay);

			if (container is DocumentWindow) {
				var documentViewModel = viewModel as DocumentItemViewModel;
				if (documentViewModel != null) {
					// DocumentWindow properties
					container.BindToProperty(DocumentWindow.FileNameProperty, viewModel, "FileName", BindingMode.TwoWay);
					container.BindToProperty(DocumentWindow.IsReadOnlyProperty, viewModel, "IsReadOnly", BindingMode.TwoWay);
				}
			}

			if (container is ToolWindow) {
				var toolViewModel = viewModel as ToolItemViewModel;
				if (toolViewModel != null) {
					// ToolWindow properties
					container.BindToProperty(ToolWindow.DefaultDockSideProperty, viewModel, "DefaultDockSide", BindingMode.TwoWay, new ToolItemDockSideConverter());
					container.BindToProperty(ToolWindow.StateProperty, viewModel, "State", BindingMode.TwoWay, new ToolItemStateConverter());
				}
			}

			// More DockingWindow properties that should be bound at the end in this order
			container.BindToProperty(DockingWindow.IsOpenProperty, viewModel, "IsOpen", BindingMode.TwoWay);
			container.BindToProperty(DockingWindow.IsSelectedProperty, viewModel, "IsSelected", BindingMode.TwoWay);
			container.BindToProperty(DockingWindow.IsActiveProperty, viewModel, "IsActive", BindingMode.TwoWay);
		}

	}

}
