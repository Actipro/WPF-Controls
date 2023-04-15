---
title: "Microsoft Ribbon UI Design Guidelines"
page-title: "Microsoft Ribbon UI Design Guidelines - Ribbon Reference"
order: 9
---
# Microsoft Ribbon UI Design Guidelines

Microsoft has created a 160+ page document that spells out exactly which parts of the ribbon UI must be implemented if you choose to use a ribbon user interface in your product.

Actipro comes in by taking care of implementation of the guidelines.  The Actipro Ribbon control has been designed by strictly following the design guidelines that Microsoft created.  This ensures that you are able to meet your licensing requirements for when you use a ribbon user interface.

## Actipro Ribbon Design Guideline Implementation Status

The list below details out all of the Microsoft ribbon UI design guideline sections and their implementation status in Actipro Ribbon.

**Actipro Ribbon implements ALL required and nearly all optional sections of the official Microsoft ribbon UI design guidelines.**

This means that if you use Actipro Ribbon to provide your ribbon user interface, you know that you are meeting the Microsoft requirements, and that you have access to all of the items that Microsoft has dictated as optional.

| Section | Office 2007 UI Required | Office 2010 UI Required | "Must" Item Status | "Should" Item Status |
|-----|-----|-----|-----|-----|
| **Accessing Document Level Command UI** | Yes | Yes | -   | -   |
| Office 2007 UI Application Button | Yes | No  | Complete | Complete |
| Office 2010 UI File Tab | No  | Yes | Complete | Complete |
| **Application Menu** | Yes | Yes | -   | -   |
| Application Menu Requirements | Yes | Yes | Complete | Complete |
| Application Menu Controls | Yes | Yes | Complete | Complete |
| **Backstage View** | No  | Yes | -   | -   |
| Backstage View Requirements | No  | Yes | Complete | Complete |
| Tabs | No  | Yes | Complete | Complete |
| Tab Pane Requirements | No  | Yes | Complete | Complete |
| Commands in the Tab Pane | No  | Yes | Complete | Complete |
| Control Panes | No  | Yes | Complete | Complete |
| **Backstage Controls** | No  | No  | -   | -   |
| Button | No  | No  | Complete | Complete |
| Menu | No  | No  | Complete | Complete |
| Icon Gallery | No  | No  | Not Implemented | Not Implemented |
| ListBox | No  | No  | Not Implemented | Not Implemented |
| Most Recently-Used List | No  | No  | Complete | Complete |
| Second Tier Tabs | No  | No  | Complete | Complete |
| **Ribbon** | Yes | Yes | Complete | Complete |
| **Tabs** | Yes | Yes | -   | -   |
| Displaying Tabs | Yes | Yes | Complete | Complete |
| Minimizing the Ribbon | No  | No  | Complete | Complete |
| Tab Scrolling | No  | No  | Complete | Complete |
| **Group** | Yes | Yes | -   | -   |
| Displaying Groups | Yes | Yes | Complete | Complete |
| Dialog Box Launchers | Yes | Yes | Complete | Complete |
| **Controls** | Yes | Yes | -   | -   |
| Displaying Controls | Yes | Yes | Complete | Complete |
| Control Layouts | Yes | Yes | Complete | Complete |
| Control Labels | Yes | Yes | Complete | Complete |
| Control Behaviors | Yes | Yes | Complete | Complete |
| **Ribbon Resizing** | Yes | Yes | -   | -   |
| Defining Groups for Ribbon Resizing | Yes | Yes | Complete | Complete |
| Collapsed Group Behavior | Yes | Yes | Complete | Complete |
| Defining Group Combinations for Ribbon Resizing | Yes | Yes | Complete | Complete |
| **Quick Access Toolbar** | Yes | Yes | -   | -   |
| Displaying the Quick Access Toolbar | Yes | Yes | Complete | Complete |
| Customizing the Quick Access Toolbar | No  | No  | Complete | Complete |
| Displaying Many Controls in the Quick Access Toolbar | No  | No  | Complete | Complete |
| **Visual Appearance** | Yes | Yes | -   | -   |
| Application Button | Yes | Yes | Complete | Complete |
| Position of UI Elements | Yes | Yes | Complete | Complete |
| Quick Access Toolbar | Yes | Yes | Complete | Complete |
| Application Title Bar | Yes | Yes | Complete | Complete |
| Tabs | Yes | Yes | Complete | Complete |
| Ribbon Background | Yes | Yes | Complete | Complete |
| Groups | Yes | Yes | Complete | Complete |
| Scrollbars | No  | No  | Complete | Complete |
| Status Bar | No  | No  | Complete | Complete |
| File Tab | No  | No  | Complete | Complete |
| **Keyboard Access** | Yes | Yes | -   | -   |
| Displaying KeyTips | Yes | Yes | Complete | Complete |
| Dismissing KeyTips | Yes | Yes | Complete | Complete |
| Keyboard Navigation | Yes | Yes | Complete | Complete |
| KeyTip Size and Positioning | Yes | Yes | Complete | Complete |
| Backstage View KeyTips | Yes | Yes | Complete*\* | Complete*\* |
| KeyTips for Collapsed Groups | Yes | Yes | Complete | Complete |
| **Contextual Tabs** | No  | No  | -   | -   |
| Selecting Contextual Tabs When Inserting a New Object | Yes* | Yes* | Complete | Complete |
| Showing Contextual Tabs After Selecting An Existing Object | Yes* | Yes* | Complete | Complete |
| Contextual Tab Labels | Yes* | Yes* | Complete | Complete |
| **Galleries** | No  | No  | -   | -   |
| Displaying Galleries | Yes* | Yes* | Complete | Complete |
| In-Ribbon Gallery Navigation Arrows | Yes* | Yes* | Complete | Complete |
| Displaing Expanded In-Ribbon Galleries | Yes* | Yes* | Complete | Complete |
| Resizing Expanded In-Ribbon Galleries | No  | No  | Complete | Complete |
| Gallery Filters | No  | No  | Complete | Complete |
| **Mini Toolbar** | No  | No  | -   | -   |
| Displaying the Mini Toolbar | Yes* | Yes* | Complete | Complete |
| Dismissing the Mini Toolbar | Yes* | Yes* | Complete | Complete |
| Controls Displayed on the Mini Toolbar | Yes* | Yes* | Complete | Complete |
| Displaying the Mini Toolbar with Context Menus | Yes* | Yes* | Complete | Complete |
| Previewing Context Menu Control Actions | No  | No  | Not Implemented | Not Implemented |
| **ScreenTips** | Yes | Yes | -   | -   |
| Displaying ScreenTips | Yes* | Yes* | Complete | Complete |

\* Subsection is required only if its optional parent section is implemented.

\*\* Key tips on Backstage view work for top-level buttons and tabs.

## Ribbon UI Guideline Implementation Hints

While Actipro Ribbon does meet all the required guidelines, there are some items in the guidelines out of our control that you should be made aware of when developing your ribbon user interface.  Many of these are listed in the Best Practices portions of the guidelines document.  We've compiled a list of the other items not in the Best Practices portions below to help you fulfill all the requirements and recommendations.  Our comments are listed in italics after each item.

- Accessing Document Level Command UI

  - *The Office 2007 UI application button must use the traditional application menu.  The Office 2010 UI File tab may use either the traditional application menu or the Backstage view.*

- Backstage View

  - Tab Pane Requirements

    - #1 - The first tab MUST be selected whenever the user clicks on the File Tab.  The tab that was selected in the Backstage View when it was last closed MUST NOT be selected when it is next opened, unless it is the first tab. *This can be done as in our samples by handling the Ribbon.IsApplicationMenuOpenChanged event and setting the Backstage.SelectedItem property.*

- Application Menu

  - Displaying the Application Menu

    - #4 - The Application Menu MUST display controls for performing actions on the entire document like New, Open, Save, Save As, Print, Send, and Close. *Be sure to include these basic document commands in your application menu's menu items.*

    - #7 - The right pane SHOULD display a list of recent documents if applicable. *You can use our [RecentDocumentMenu](controls/miscellaneous/recentdocumentmenu.md) control for showing a list of recent documents.*

    - #10 - Vertical scrollbars MUST NOT be displayed for either the left or right pane of the Application Menu. *Be sure that whatever you place in the right pane, it does not have vertical scrollbars.*

- Ribbon

  - #2 - The Ribbon MUST NOT coexist with top-level menus and toolbars. *Don't use any other main menus or toolbars when using a ribbon user interface.*

  - #4 - The Ribbon MUST be positioned at the top of the application window in a space that is dedicated to displaying the Ribbon. *Place the Ribbon control at the top of the containing window.*

  - #5 - The Ribbon MUST NOT be displayed on either side of the application window or at the bottom of the application window. *Place the Ribbon control at the top of the containing window.*

  - #11 - The Ribbon MUST extend the full width of the application window at all times. *Ensure that the Ribbon control stretches across the full width of the containing window.*

- Tabs

  - Displaying Tabs

    - #4 - The tab selected on the Ribbon MUST NOT automatically switch as a result of user selections made in the document (except as noted in the Contextual Tabs section). *Don't programmatically change the selected tab except under the conditions in the Contextual Tabs section.*

  - Minimizing the Ribbon

    - #11 - The Ribbon MUST be minimized when the application is opened if the Ribbon was minimized when the application was last closed. *You must persist whether the ribbon was minimized when your application was last closed.*

    - #12 - The Ribbon MUST NOT be minimized when the application is opened for the very first time. *Don't initialize your ribbon to be minimized the first time your application is run.*

- Groups

  - Displaying Groups

    - #3 - Controls displayed in a group MUST NOT change as a result of selection.  If a control is not active, then the control MUST be grayed out, rather than removed from the group. *Don't add or remove controls from a group based on the selection in the document.*

  - Dialog Box Launchers

    - #5 - Dialog Box Launchers MUST NOT execute a command that modifies a selection in the document.  Dialog Box Launchers MUST only provide access to additional controls by opening a dialog box or task pane. *Only open dialog boxes or task panes when a dialog box launcher button is pressed.*

- Controls

  - Displaying Controls

    - #7 -Both the menu and button portion of split button controls MUST be disabled on the Ribbon if all of the items in the menu are inactive. *Because of the way WPF commands work, the SplitButton control only examines child menu items if a command is assigned to its Command property.  So as long as you have a command set to the SplitButton this functionality will work.  If you need to use a temporary command while prototyping, use ApplicationCommands.NotACommand.*

  - Control Labels

    - #3 - Text labels for controls in the Ribbon MUST NOT use ellipses to indicate that a dialog box will be displayed. *Don't add ellipses to any control labels.*

  - Control Behaviors

    - #1 - For large split button controls, clicking on the icon MUST always perform the default action for the control (i.e., either the first item in the menu or the most recently used item from the menu). *Always make the split button click perform a command of an item that appears in its drop-down menu.*

- Ribbon Resizing

  - Defining Groups for Ribbon Resizing

    - #7 - Groups SHOULD have three – four predefined variants to provide a smooth transition between group variants when the application window is horizontally resized. *Use Actipro Ribbon's powerful variant features to easy make variants for your groups and controls.*

  - Collapsed Group Behavior

    - #6 - Clicking outside a collapsed group displayed below the Ribbon MUST NOT dismiss or clear the current selection of objects or text in the document. *Set the Ribbon.TargetContentContainer property to your document container area and Ribbon will handle this for you.*

  - Defining Group Combinations for Ribbon Resizing

    - #13 - The entire Ribbon SHOULD completely disappear when the application window is less than 300 pixels wide and 250 pixels tall to provide more space for displaying the document. *Set the Ribbon.CollapseThresholdSize property to specify the size a which the ribbon collapses if the Ribbon.IsCollapsible property is set to true.*

- Quick Access Toolbar

  - Displaying the Quick Access Toolbar

    - #5 - The controls on the Quick Access Toolbar MUST NOT dynamically change when different tabs are selected in the Ribbon or when different objects are selected in the document. *Don't programmatically change the controls on the QAT based on the selected tab or objects in the document.*

    - #18 - If the Quick Access Toolbar is displayed below the Ribbon when the application is closed, then the Quick Access Toolbar MUST be displayed below the Ribbon when the application is next opened. *Restore the previous location of the QAT each time your application is loaded.  You can set the Ribbon.IsQuickAccessToolBarBelowRibbon property.*

- Visual Appearance

  - Status Bar

    - #2 - If the status bar both displays status information and contains view changing controls, then it MUST be divided into two sides.  The left side of the status bar MUST display status notifications.  The right side of the status bar MUST display view changing controls. *See the topic on StatusBar Styles to learn how to apply both styles to items.*

    - #4 - The status bar MUST be no taller than approximately 23 pixels at 96 dpi. *Keep the height equal to a regular small-sized control.*

- Keyboard Access

  - Displaying KeyTips

    - #5 - Every control MUST have a KeyTip unique to its tab.  There MUST NOT be any duplicate KeyTips for any controls on the same tab. *This really applies to any key tip scope, including menus.*

- Contextual Tabs

  - Selecting Contextual Tabs When Inserting a New Object

    - #3 - If Contextual Tabs are available for an object, they MUST be selected when a new object is inserted into the document if the new object is selected (or has keyboard focus) after being inserted. *Always select the first tab within the appropriate contextual tab group in this scenario.*

    - #4 - If there is more than one Contextual Tab available for the object, the leftmost Contextual Tab MUST be selected. *Always select the first tab when selecting a tab in a contextual tab group.*

    - #6 - Contextual Tabs MUST disappear when the object is no longer selected. *Deactivate the contextual tab group when it is no longer needed.*

    - #8 - If more than one object type with associated Contextual Tabs can be selected at once, then all relevant Contextual Tabs MUST be displayed at the same time. *Actipro Ribbon supports the display of more than one contextual tab group at the same time.*

  - Showing Contextual Tabs After Selecting An Existing Object

    - #1 - Contextual Tabs MUST be available when the user selects an existing object in the document, but the Contextual Tabs MUST NOT become selected.  The currently selected tab MUST continue to be selected in the Ribbon. *You can activate a contextual tab group but don't set select any of its tabs in this scenario.*

    - #2 - Contextual Tabs MUST NOT be automatically selected when an existing object is selected in the document.  Users MUST click the Contextual Tab for it to be selected. *Don't select a contextual tab programmatically in this scenario.*

    - #3 - Contextual Tabs SHOULD be selected when a user double-clicks on an existing object in the document. *Do select a contextual tab programmatically in this scenario.*

    - #4 - If a Contextual Tab is selected in the Ribbon when a user deselects and reselects the same object (or type of object) in the document without performing any other action, then the Contextual Tabs SHOULD be reselected. *You can track the contextual tab groups that you display and whether to reselect them upon redisplay or not.*

- Mini Toolbar

  - Displaying the Mini Toolbar

    - #2 - The Mini Toolbar MUST NOT be displayed when text is selected with the keyboard. *Only show the mini-toolbar when selections are made using the mouse.*

  - Displaying the Mini Toolbar with Context Menus

    - #13 - The ability to turn off the Mini Toolbar SHOULD be provided in the application's options.  This option SHOULD NOT turn off the Mini Toolbar displayed with context menus.  The Mini Toolbar SHOULD always be displayed with context menus. *When you add such an option in your application, don't execute any code that would show the mini-toolbar following a selection.*

Microsoft Office User Interface is subject to protection under U.S. and international intellectual property laws and is used by Actipro Software LLC under license from Microsoft.
