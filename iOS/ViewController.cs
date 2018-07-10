using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Autofac;
using Domain.Interactor;
using UIKit;
using iAndroidExample.iOS.RepoTable;
using Foundation;
using ReactiveUI;

namespace iAndroidExample.iOS
{
    public partial class ViewController : UIViewController
    {
        IDisposable Disposable;
        GetReposUseCase reposUseCase;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
              
            using (var scope = AppDelegate.Container.BeginLifetimeScope())
            {
                reposUseCase = scope.Resolve<GetReposUseCase>();
            }

            // Perform any additional setup after loading the view, typically from a nib.
            //Button.AccessibilityIdentifier = "myButton";
            //Button.TouchUpInside += delegate
            //{
            //    var title = string.Format("{0} clicks!", count++);
            //    Button.SetTitle(title, UIControlState.Normal);
            //};

            table_repos.RegisterNibForCellReuse(RepoCell.Nib, "cell_repo");
            table_repos.ReloadData();

            btn_load.TouchUpInside += delegate 
            {
                Disposable = reposUseCase
                    .Get("jetruby")
                    .SubscribeOn(ThreadPoolScheduler.Instance)
                    .ObserveOn(new NSRunloopScheduler())
                    .Subscribe(
                        list => {
                            table_repos.Source = new RepoTableSource(list);
                            table_repos.ReloadData();
                    },
                        error => {},
                        () => {}
                    );
            };
        }


        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.		
        }
    }
}
