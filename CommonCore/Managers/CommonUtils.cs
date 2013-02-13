//
//  CommonUtils.cs
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

using System.Collections;
using SysIO = System.IO;
#if MonoTouch
using MonoTouch.UIKit;
using MonoTouch.Foundation;
#endif
using FileExplorerMobile.Core.Data.Enums;
using FileExplorerMobile.Core.Interfaces;

namespace FileExplorerMobile.Core.Managers
{
	public class CommonUtils : BaseManager, ICommonUtils
	{
		#if MonoTouch
		/// <summary>
		/// The list of the hashed document type images. 
		/// </summary>
		private Hashtable _DocumentTypeImages;
		#endif

		/// <summary>
		/// Working constructor.
		/// </summary>
		public CommonUtils()
		{
			#if MonoTouch
			_DocumentTypeImages = new Hashtable();
			#endif
		}

		/// <summary>
		/// Gets the FileEntry type by <see cref="extension"/>.
		/// </summary>
		/// <param name='extension'>The extension.</param>
		/// <returns>The FileEntry type.</returns>
		public FileEntryTypes GetTypeByExtension(string extension)
		{
			if (string.IsNullOrEmpty(extension)) {
				return FileEntryTypes.Unknown;
			}
			switch (extension.ToLower()) {
				case ".pdf":
					return FileEntryTypes.PDF;
				case ".doc":
				case ".docx":
					return FileEntryTypes.Word;
				case ".xls":
				case ".xlsx":
					return FileEntryTypes.Excel;
				case ".ppt":
				case ".pptx":
					return FileEntryTypes.PowerPoint;
				case ".jpg":
				case ".jpeg":
				case ".png":
				case ".tiff":
					return FileEntryTypes.Image;
				default:
					return FileEntryTypes.Unknown;
			}
		}	

		/// <summary>
		/// Gets the correct path.
		/// </summary> 
		/// <param name='path'>The original path.</param>
		/// <returns>The correct path.</returns>
		public string GetCorrectPath(string path)
		{
			if (path != null) {
				return path.Replace('/', SysIO.Path.DirectorySeparatorChar);
			}
			return null;
		}

		/// <summary>
		/// Gets the FileEntry type image.
		/// </summary>
		/// <param name='type'>The FileEntry type.</param>
		/// <returns>The FileEntry type image.</returns>
#if MonoTouch
		public UIImage GetTypeImage(FileEntryTypes type)
		{
			string imageFolder;
			imageFolder = "Content/Images/DocumentTypes/";

			switch (type) {
				case FileEntryTypes.Folder:
					return _GetTypeImageFromHash(type, GetCorrectPath(imageFolder + "Folder48.png"));
				case FileEntryTypes.PDF:
					return _GetTypeImageFromHash(type, GetCorrectPath(imageFolder + "PDF48.png"));
				case FileEntryTypes.Word:
					return _GetTypeImageFromHash(type, GetCorrectPath(imageFolder + "Word48.png"));
				case FileEntryTypes.Excel:
					return _GetTypeImageFromHash(type, GetCorrectPath(imageFolder + "Excel48.png"));
				case FileEntryTypes.PowerPoint:
					return _GetTypeImageFromHash(type, GetCorrectPath(imageFolder + "PowerPoint48.png"));
				case FileEntryTypes.Image:
					return _GetTypeImageFromHash(type, GetCorrectPath(imageFolder + "Image48.png"));
				default:
					return _GetTypeImageFromHash(type, GetCorrectPath(imageFolder + "Undefined48.png"));
			}
		}

		/// <summary>
		/// Gets the type image from the hash.
		/// </summary>
		/// <param name='type'>The FileEntry type.</param>
		/// <param name='imageFileName'>The image file name.</param>
		/// <returns>The FileEntry type image.</returns>
		private UIImage _GetTypeImageFromHash(FileEntryTypes type, string imageFileName)
		{
			if (_DocumentTypeImages.ContainsKey(type)) {
				return _DocumentTypeImages[type] as UIImage;
			} else {
				var img = UIImage.FromFile(imageFileName);
				_DocumentTypeImages.Add(type, img);
				return img;
			}
		}
#endif

		/// <summary>
		/// Shows the alert window and returns the user choise.
		/// </summary>
		public AlertResults ShowAlert(string title, string message, params string[] buttons)
		{
#if MonoTouch
			using (var alert = new UIAlertView()) {
				alert.Title = title;
				alert.Message = message;
				foreach (var btn in buttons) {
					alert.AddButton(btn);
				}
				int clickedButtonIndex = -1;
				alert.Clicked += (sender, e) => {
					clickedButtonIndex = e.ButtonIndex;
				};
				alert.Show();	
				while (clickedButtonIndex == -1) {
					NSRunLoop.Current.RunUntil(NSDate.FromTimeIntervalSinceNow(0.2));
				}
				
				switch (clickedButtonIndex) {
				case 0:
					return AlertResults.Yes;
				case 1:
					return AlertResults.No;
				case 2:
					return AlertResults.Cancel;
				default:
					return AlertResults.Unknown;
				}
			}
#else
			return AlertResults.Unknown;
#endif
		}
	}
}

