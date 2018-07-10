using System;
using Android.App;
using Autofac;
using Data.Api;
using Data.Repository;
using Data.Repository.DataSource;
using Domain.Interactor;
using Domain.Repository;

namespace iAndroidExample.Droid
{
    [Application]
    public class App : Application
    {
        public static IContainer Container { get; set; } 

        public App(IntPtr javaReference, Android.Runtime.JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            var builder = new ContainerBuilder();

            builder.RegisterType<ItemsDataSource>()
                   .As<IDataSource>();
            
            builder.RegisterType<RepositoryImpl>()
                   .As<IRepository>();

            builder.RegisterType<AppHttpClientHandler>()
                   .AsSelf();

            builder.Register(c => 
                             new ApiFactory(
                                 c.Resolve<AppHttpClientHandler>()).Create())
                   .As<IGithubApi>();

            builder.RegisterType<GetReposUseCase>()
                   .AsSelf();

            Container = builder.Build();
        }

    }
}
