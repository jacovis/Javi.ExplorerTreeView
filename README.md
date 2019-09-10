[![NuGet](https://img.shields.io/nuget/v/Javi.ExplorerTreeView.svg)](https://www.nuget.org/packages/Javi.ExplorerTreeView/) 

# 

# <img align="center" src="./PackageIcon.png">  Javi.MediaInfo

This package provides a Windows Explorer application navigation like usercontrol.
The package [Dirkster.WSF](https://www.nuget.org/packages/Dirkster.WSF/) is used to interact with the 
windows shell in order to obtain all special folders.
The treeview supports lazy loading.

- [Features](#features)
- [Getting Started](#getting-started)
- [Usage](#usage)
- [Demo application](#demo)
- [License](#license)
- [Acknowledgments](#acknowledgments)

## Features
- provides a Windows Explorer navigation pane alike treeview usercontrol for WPF
- targets .NET Full Framework 4
- supports lazy loading of treeview items
    
## Getting Started

- Install package using nuget

Install Javi.ExplorerTreeView from NuGet using the Package Manager Console with the following command

    PM> Install-Package Javi.ExplorerTreeView

Alternatively search on [NuGet Javi.ExplorerTreeView](https://www.nuget.org/packages/Javi.ExplorerTreeView)

## Usage

See the [demo application](#demo).

## Demo application

A C# WPF [demo application](https://github.com/jacovis/Javi.ExplorerTreeView/tree/master/Demo) is available which 
shows the usage of the package. Code from this demo should not be used in production code, the code is merely to 
demonstrate the usage of this package.
    
## License

This project is licensed under the [MIT License](https://github.com/jacovis/Javi.ExplorerTreeView/blob/master/LICENSE.md).

## Acknowledgments

[Dirkster99](https://www.nuget.org/profiles/Dirkster99) for his excellent package providing access to the Windows Shell.
