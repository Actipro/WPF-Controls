---
title: "IME (Input Method Editor) Support"
page-title: "IME (Input Method Editor) Support - SyntaxEditor Globalization and Accessibility"
order: 2
---
# IME (Input Method Editor) Support

SyntaxEditor has complete support for editing text using IME (Input Method Editor).

IME is a Window feature that allows users to enter characters and symbols not found on their input device.  It allows Western keyboards to enter Chinese, Japanese, etc. characters.

> [!NOTE]
> SyntaxEditor's IME features are not available in XBAP applications since they require the use of Windows API calls.

IME support is enabled by default.  To disable it for any reason, set the [SyntaxEditor](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor).[IsImeEnabled](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor.IsImeEnabled) property to `false`.
