using System;
using System.Collections.Generic;
using Domain.Entity;

namespace Domain.Repository
{
    public interface IRepository
    {
        IObservable<List<RepoOrganization>> GetOrganizationRepos(string orgName);
    }
}
