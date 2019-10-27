using AutoMapper;
using BusinessLayer.Entities;
using BusinessLayer.Helpers;
using BusinessLayer.Helpers.Pagination;
using BusinessLayer.Models.Field;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Helpers.Pagination;
using Web.Models.Field;
using Web.Models.Helpers;

namespace Web.Controllers
{
    [Authorize]
    [Route("api/fields")]
    [ApiController]
    public class FieldController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFieldRepository _fieldRepository;

        public FieldController(IMapper mapper, IFieldRepository fieldRepository)
        {
            _mapper = mapper;
            _fieldRepository = fieldRepository;
        }

        #region List()
        [HttpGet("list")]
        public async Task<ActionResult> GetPagedList([FromQuery]string street,
                                                     [FromQuery]int? streetNumber,
                                                     [FromQuery]int page,
                                                     [FromQuery]int size)
        {
            var pager = new Pager(page, size);
            var filter = new FieldSearch(street, streetNumber);

            var pagedDBFields = await _fieldRepository.GetPagedList(filter, pager);
            var pagedResult = new PagedResult<FieldItem>
            {
                Items = _mapper.Map<IEnumerable<FieldItem>>(pagedDBFields.Items),
                RowNumber = pagedDBFields.RowNumber
            };

            return Ok(pagedResult);
        }
        #endregion

        #region GetDetails()
        [AllowAnonymous]
        [HttpGet("{id:long}")]
        public async Task<ActionResult> GetDetails(long id)
        {
            var field = await _fieldRepository.GetWithRelations(id);
            if (field == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<FieldItem>(field));
        }
        #endregion

        #region Create()
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Create([FromBody]FieldRequest request)
        {
            try
            {
                var createdField = await _fieldRepository.Create(_mapper.Map<Field>(request));
                return CreatedAtAction("GetDetails", new { createdField.Id }, new { createdField.Id });
            }
            catch (BusinessLogicException e)
            {
                return BadRequest(_mapper.Map<BadRequestModel>(e));
            }
        }
        #endregion

        #region Update()
        [AllowAnonymous]
        [HttpPut("{id:long}")]
        public async Task<ActionResult> Update(long id, [FromBody]FieldRequest request)
        {
            try
            {
                var field = await _fieldRepository.Get(id);
                if (field == null)
                {
                    return NotFound();
                }

                var updatedField = await _fieldRepository.Update(id, _mapper.Map<Field>(request),
                    _mapper.Map<IEnumerable<WorkingTime>>(request.WorkingTime));
                return Ok(_mapper.Map<FieldUpdateResponse>(updatedField));
            }
            catch (BusinessLogicException e)
            {
                return BadRequest(_mapper.Map<BadRequestModel>(e));
            }
        }
        #endregion

        #region Delete()
        [AllowAnonymous]
        [HttpDelete("{id:long}")]
        public async Task<ActionResult> Delete(long id)
        {
            var field = await _fieldRepository.GetWithRelations(id);
            if (field == null)
            {
                return NotFound();
            }

            await _fieldRepository.Delete(field);
            return Ok();
        }
        #endregion

    }
}
