using System;
using System.Collections.Generic;
using Domain.Entity;
using Foundation;
using UIKit;

namespace iAndroidExample.iOS.RepoTable
{
    public class RepoTableSource : UITableViewSource
    {
        private List<RepoOrganization> Repos = new List<RepoOrganization>();
        private string CellIdentifier = "cell_repo";

        public RepoTableSource(List<RepoOrganization> items)
        {
            Repos.AddRange(items);
        }

        public void addItems(List<RepoOrganization> items) 
        {
            Repos.AddRange(items);
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(CellIdentifier, indexPath) as RepoCell;
            RepoOrganization item = Repos[indexPath.Row];

            if (cell == null)
            {
                cell = new RepoCell();
            }

            cell.UpdateCell(item);

            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section) => Repos.Count;
    }
}
