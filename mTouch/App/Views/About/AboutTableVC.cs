//
//  AboutVC.cs
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
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace FileExplorerMobile.mTouch.Views
{
	public class AboutTableVC : UITableViewController
	{
		#region Initialization
		public AboutTableVC() : base(UITableViewStyle.Grouped)
		{
			TabBarItem.Title = "About";
			TabBarItem.Image = UIImage.FromBundle("Content/Images/TabBar/about32.png");
		}
		#endregion

		#region UI Logic		
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			TableView.Source = new TableSource();
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation(UIInterfaceOrientation toInterfaceOrientation)
		{
			return true;
		}
		#endregion

		#region TableSource
		private class TableSource : UITableViewSource 
		{
			/// <summary>
			/// The cell identifier.
			/// </summary>
			private const string CellIdentifier = "CellIdentifier";

			/// <summary>
			/// The counts of sections.
			/// </summary>
			private const int SectionCounts = 2;

			/// <summary>
			/// The row counts in sections.
			/// </summary>
			private readonly int[] RowCounts = new int[] { 4, 1 };

			/// <summary>
			/// Working constructor.
			/// </summary>
			public TableSource()
			{
			}	

			public override string[] SectionIndexTitles(UITableView tableView)
			{
				return new string[] { "", "" };
			}

			public override int NumberOfSections(UITableView tableView)
			{
				return SectionCounts;
			}
			
			public override int RowsInSection(UITableView tableview, int section)
			{
				return RowCounts[section];
			}

			public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
			{
				UITableViewCell cell = null;
				cell = tableView.DequeueReusableCell(CellIdentifier);
				if (cell == null) {
					cell = new UITableViewCell(UITableViewCellStyle.Subtitle, CellIdentifier);
				}

				string title = null;
				string details = null;
				switch (indexPath.Section) {
					case 0: 
						switch (indexPath.Row) {
							case 0:
								title = "FileExplorer";
								details = "";
								break;
							case 1:
								title = "Author";
								details = "Alexander Matsibarov (macasun)";
								break;
							case 2:
								title = "Email";
								details = "amatsibarov@gmail.com";
								break;
							case 3:
								title = "Copyrights";
								details = "2013";
								break;
						}
						break;
					
					case 1:
						title = "Application description";
						details = "FileExplorer application for iOS and Android";
						break;
				}

				cell.TextLabel.Text = title;
				cell.DetailTextLabel.Text = details;
				return cell;
			}
		}
		#endregion
	}
}



