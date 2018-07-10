using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.Widget;
using Domain.Interactor;
using Autofac;
using System;
using System.Collections.Generic;
using Domain.Entity;
using System.Reactive.Linq;
using System.Reactive.Concurrency;
using System.Reactive.PlatformServices;
using System.Threading.Tasks;
using ReactiveUI;
using Java.Lang;
using Android.Runtime;

namespace iAndroidExample.Droid
{
    [Activity(Label = "iAndroidExample", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {

        RecyclerView RvRepos;
        ReposAdapter Adapter;
        Button button;
        GetReposUseCase reposUseCase;
        IDisposable Disposable;

        private long UiThreadId = Thread.CurrentThread().Id;
        private Handler handler = new Handler(); 

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            InitViews();

            using (var scope = App.Container.BeginLifetimeScope())
            {
                reposUseCase = scope.Resolve<GetReposUseCase>();
            }

        }

        void InitViews()
        {
            RvRepos = FindViewById<RecyclerView>(Resource.Id.rvRepos);
            Adapter = new ReposAdapter();
            RvRepos.SetAdapter(Adapter);
            RvRepos.SetLayoutManager(new LinearLayoutManager(this));



            button = FindViewById<Button>(Resource.Id.btnLoad);
            button.Click += delegate
            {
                Disposable = reposUseCase
                    .Get("jetruby")
                    .SubscribeOn(new TaskPoolScheduler(new TaskFactory()))
                    .ObserveOn(new UiThreadScheduler(handler, UiThreadId))
                    .Subscribe(
                        list => Adapter.addItems(list),
                        error =>
                        {
                            Toast.MakeText(this, ((System.Exception)error).ToString(), ToastLength.Short).Show();
                            //Toast.MakeText(this, Resource.String.failed, ToastLength.Short).Show();
                        },
                        () => { }
                    );
            };
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            Disposable.Dispose();
        }
    }
}

