
This sample shows how to integrate Docking/MDI for WPF into a Prism application,
using Unity for inversion of control/dependency injection. 

The project targets a specific version of Prism and .NET Framework, but those versions
should not been seen as minimum or maximum requirements for the integration to be
successful. The Prism version and target framework can be changed to verify if the
sample code is compatible with your configuration needs.


ABOUT THIS SAMPLE

The sample leverages open source code and logic written by Actipro to easily integrate
Docking/MDI with Prism.  The code in "ViewModels/Docking/Core" is the core set of
view-models that should serve as a foundation for any document or tool view-models.
They include many properties that allow you to control docking window appearance
and default location.  Implementation classes of document and tool view-models are
in the "ViewModels/Docking" folder.  These particular types don't have any dependency
on the Docking/MDI assembly types at all.

The "Regions" folder contains numerous types that integrate the document and tool 
view-models with a DockSite.  The key piece of this is DockSiteRegionAdapter,
which creates a region, sets up the DockSite items sources, and creates a region
behavior that implements the primary integration functionality between Docking/MDI
and Prism.


NOTES

The sample references the Prism and Unity assemblies, which are distributed via NuGet.

Please make sure all NuGet packages are restored properly before running the sample project.

