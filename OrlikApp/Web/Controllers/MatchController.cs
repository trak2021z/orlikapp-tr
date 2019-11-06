﻿using AutoMapper;
using BusinessLayer.Entities;
using BusinessLayer.Helpers;
using BusinessLayer.Helpers.Pagination;
using BusinessLayer.Models.Match;
using BusinessLayer.Models.Role;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Helpers.Pagination;
using Web.Models.Helpers;
using Web.Models.Match;

namespace Web.Controllers
{
    [Authorize]
    [Route("api/matches")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<MatchController> _logger;
        private readonly IMatchRepository _matchRepository;
        private readonly IUserRepository _userRepository;

        public MatchController(IMapper mapper, IMatchRepository matchRepository, ILogger<MatchController> logger,
            IUserRepository userRepository)
        {
            _mapper = mapper;
            _matchRepository = matchRepository;
            _userRepository = userRepository;
            _logger = logger;
        }

        #region List()
        [HttpGet("list")]
        public async Task<ActionResult> GetPagedList([FromQuery]long? fieldId,
                                                     [FromQuery]int page,
                                                     [FromQuery]int size)
        {
            var pager = new Pager(page, size);
            var filter = new MatchSearch(fieldId);

            var pagedDBMatches = await _matchRepository.GetPagedList(filter, pager);
            var pagedResult = new PagedResult<MatchItem>
            {
                Items = _mapper.Map<IEnumerable<MatchItem>>(pagedDBMatches.Items),
                RowNumber = pagedDBMatches.RowNumber
            };

            return Ok(pagedResult);
        }
        #endregion

        #region Create()
        [HttpPost]
        [Authorize(Roles = RoleNames.User)]
        public async Task<ActionResult> Create([FromBody]MatchRequest request)
        {
            try
            {
                var createdMatch = await _matchRepository.Create(_mapper.Map<Match>(request), User);
                return Ok(_mapper.Map<MatchCreateResponse>(createdMatch));
            }
            catch (BusinessLogicException e)
            {
                return BadRequest(_mapper.Map<BadRequestModel>(e));
            }
        }
        #endregion

        #region Delete()
        [HttpDelete("{id:long}")]
        [Authorize(Roles = RoleNames.Admin + ", " + RoleNames.FieldKeeper)]
        public async Task<ActionResult> Delete(long id)
        {
            var match = await _matchRepository.GetWithRelations(id);
            if (match == null)
            {
                return NotFound();
            }

            if (!_userRepository.HasKeeperPermissionToField(match.Field, User))
            {
                return Forbid();
            }

            await _matchRepository.Delete(match);
            return Ok();
        }
        #endregion
    }
}
