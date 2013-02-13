//
//  AppDelegate.cs
//
//  Author:
//       Alexander Matsibarov (macasun) <amatsibarov@gmail.com>
//
//  Copyright (c) 2013 Alexander Matsibarov
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using FileExplorerMobile.mTouch.Views;
using FileExplorerMobile.Core;
using FileExplorerMobile.Core.Managers;
using FileExplorerMobile.Core.Interfaces;

namespace FileExplorerMobile.mTouch
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		UIWindow window;
		RootTabBarVC _RootTabBarVC;

		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			// Create a new window instance based on the screen size
			window = new UIWindow(UIScreen.MainScreen.Bounds);

			// Create a root view controller - TabBar controller
			_RootTabBarVC = new RootTabBarVC();

			// Create managers and register references
			MgrAccessor.RegisterReference<IFileEntryManager>(new FileEntryManager());
			MgrAccessor.RegisterReference<ICommonUtils>(new CommonUtils());
			MgrAccessor.RegisterReference<IDiskUtils>(new DiskUtils());
#if DEBUG
			// Create test data
			MgrAccessor.DiskUtils.CreateTestData();
#endif

			// Make the window visible
			window.RootViewController = _RootTabBarVC;	
			window.MakeKeyAndVisible();

			return true;
		}
	}
}