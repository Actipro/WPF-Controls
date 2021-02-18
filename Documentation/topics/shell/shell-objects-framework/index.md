---
title: "Overview"
page-title: "Shell Object Framework"
order: 1
---
# Overview

The Shell Object Framework provides a managed way to access a file system shell.  A Windows shell service is included and custom shell services can be written to access other file system shells.

Various shell user interface controls in the product like [ShellTreeListBox](../shelltreelistbox.md) use the Shell Object Framework to determine what to display and how to interact with shell objects/properties.

## Shell Objects

Shell objects represent a folder, file, or link from a [shell service](shell-services.md).  For instance, our Windows shell service implementation creates shell objects to represent the folders, files, and links found on your PC.

See the [Shell Objects](shell-objects.md) topic for more information.

## Shell Services

A shell service creates and interacts with shell objects, and is the main customization point for interfacing with a file system such as the Windows shell.

See the [Shell Services](shell-services.md) topic for more information.

## Custom Shell Objects

The built-in Windows shell functionality can be customized/extended.  Or in other cases, completely custom shell objects and services can be written to support non-Windows shells (i.e. a remote file system in a FTP client).

See the [Custom Shell Objects](custom-shell-objects.md) topic for more information.
