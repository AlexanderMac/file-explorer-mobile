//
//  MgrAccessor.cs
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
using FileExplorerMobile.Core.Interfaces;
using FileExplorerMobile.Core.Managers;

namespace FileExplorerMobile.Core
{
	public static class MgrAccessor
	{
		#region Fields
		public static IDiskUtils DiskUtils { get; private set; }
		public static ICommonUtils CommonUtils { get; private set; }
		public static IFileEntryManager FileEntryMgr { get; private set; }
		#endregion

		#region Logic
		/// <summary>
		/// Registers the reference.
		/// </summary>
		/// <param name='instance'>The instance.</param>
		public static void RegisterReference<IInterface>(BaseManager instance) where IInterface : IBaseManager
		{
			var interfaceType = typeof(IInterface);
			if (interfaceType == typeof(IDiskUtils)) {
				DiskUtils = (IDiskUtils)instance;
			} else if (interfaceType == typeof(ICommonUtils)) {
				CommonUtils = (ICommonUtils)instance;
			} else if (interfaceType == typeof(IFileEntryManager)) {
				FileEntryMgr = (IFileEntryManager)instance;
			} else {
				throw new Exception(string.Format("Unknown instance interface: ", typeof(IInterface).Name));
			}
		}
		#endregion
	}
}

