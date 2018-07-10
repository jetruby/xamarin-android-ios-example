using System;
using System.Collections.Generic;
using Data.Api;
using Domain.Entity;

namespace Data.Repository.DataSource
{
    public class ItemsDataSource : IDataSource
    {
        private IGithubApi api;
        private int defaultPage = 1;
        private int perPage = 10;


        public ItemsDataSource(IGithubApi api)
        {
            this.api = api;
        }

        public IObservable<List<RepoOrganization>> FetchItems(string orgName) {
            return api.FetchRepos(orgName, defaultPage, perPage);
        }
    }
}
