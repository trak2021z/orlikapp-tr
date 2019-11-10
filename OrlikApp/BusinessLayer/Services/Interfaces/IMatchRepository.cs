﻿using System.Security.Principal;
using System.Threading.Tasks;
using BusinessLayer.Entities;
using BusinessLayer.Helpers.Pagination;
using BusinessLayer.Models.Match;
using Web.Helpers.Pagination;

namespace BusinessLayer.Services.Interfaces
{
    public interface IMatchRepository
    {
        Task<Match> Get(long id);
        Task<Match> GetWithMatchMembers(long id);
        Task<Match> GetWithRelations(long id);
        Task<PagedResult<Match>> GetPagedList(MatchSearch filter, Pager pager, bool isConfirmed = true);
        Task<Match> Create(Match match, IPrincipal loggedUser);
        Task<Match> Delete(Match match);
    }
}