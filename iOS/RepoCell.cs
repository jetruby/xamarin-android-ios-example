using System;

using Foundation;
using UIKit;
using Domain.Entity;

namespace iAndroidExample.iOS.RepoTable
{
    public partial class RepoCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("RepoCell");
        public static readonly UINib Nib;

        static RepoCell()
        {
            Nib = UINib.FromName("RepoCell", NSBundle.MainBundle);
        }

        public RepoCell()
        {
            
        }

        public void UpdateCell(RepoOrganization repo) {
            lbl_repo_name.Text = repo.name;
            lbl_owner.Text = repo.owner.login;
            lbl_language.Text = repo.language;
            lbl_description.Text = repo.description;
        }

        protected RepoCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
    }
}
