//
//  ICommonUtils.cs
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

#if MonoTouch
using MonoTouch.UIKit;
#endif
using FileExplorerMobile.Core.Data.Enums;

namespace FileExplorerMobile.Core.Interfaces
{
	public interface ICommonUtils : IBaseManager
	{
		/// <summary>
		/// Gets the FileEntry type by <see cref="extension"/>.
		/// </summary>
		/// <param name='extension'>The extension.</param>
		/// <returns>The FileEntry type.</returns>
		FileEntryTypes GetTypeByExtension(string extension);

		/// <summary>
		/// Gets the correct path.
		/// </summary> 
		/// <param name='path'>The original path.</param>
		/// <returns>The correct path.</returns>
		string GetCorrectPath(string path);

		/// <summary>
		/// Gets the FileEntry type image.
		/// </summary>
		/// <param name='type'>The FileEntry type.</param>
		/// <returns>The FileEntry type image.</returns>
#if MonoTouch
		UIImage GetTypeImage(FileEntryTypes type);
#endif
		/// <summary>
		/// Shows the alert window and returns the user choise.
		/// </summary>
		AlertResults ShowAlert(string title, string message, params string[] buttons);
	}
}

