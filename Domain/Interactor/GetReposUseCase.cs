using System;
using System.Collections.Generic;
using Domain.Entity;
using Domain.Repository;

namespace Domain.Interactor
{
    public class GetReposUseCase
    {
        private IRepository Repository;

        public GetReposUseCase(IRepository repository)
        {
            Repository = repository;
        }

        public IObservable<List<RepoOrganization>> Get(string orgName) 
        {
            return Repository.GetOrganizationRepos(orgName);
        }
    }
}
