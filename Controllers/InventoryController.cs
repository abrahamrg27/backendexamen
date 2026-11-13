using backend.DTOs;
using backend.services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private IValidator<InventoryInsertDto> _inventoryInsertValidator;
        private IValidator<InventoryUpdateDto> _inventoryUpdateValidator;
        private ICommonServices<InventoryDto, InventoryInsertDto, InventoryUpdateDto> _inventoryService;

        public InventoryController(
            IValidator<InventoryInsertDto> inventoryInsertValidator,
            IValidator<InventoryUpdateDto> inventoryUpdateValidator,
            [FromKeyedServices("inventoryService")]
            ICommonServices<InventoryDto, InventoryInsertDto, InventoryUpdateDto> inventoryService)
        {
            _inventoryInsertValidator = inventoryInsertValidator;
            _inventoryUpdateValidator = inventoryUpdateValidator;
            _inventoryService = inventoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryDto>>> Get()
        {
            return Ok(await _inventoryService.Get());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryDto>> GetById(int id)
        {
            var inventory = await _inventoryService.GetById(id);
            return inventory == null ? NotFound() : Ok(inventory);
        }

        [HttpPost]
        public async Task<ActionResult<InventoryDto>> Add(InventoryInsertDto inventoryInsertDto)
        {
            var validationResult = await _inventoryInsertValidator.ValidateAsync(inventoryInsertDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));
            }

            var inventoryDto = await _inventoryService.Add(inventoryInsertDto);

            if (inventoryDto == null)
            {
                return BadRequest(new { message = "El número de lote ya existe en el inventario." });
            }

            return CreatedAtAction(nameof(GetById), new { id = inventoryDto.InventoryId }, inventoryDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<InventoryDto>> Update(int id, InventoryUpdateDto inventoryUpdateDto)
        {
            var validationResult = await _inventoryUpdateValidator.ValidateAsync(inventoryUpdateDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors
                    .Select(e => new { e.PropertyName, e.ErrorMessage }));
            }

            var inventoryDto = await _inventoryService.Update(id, inventoryUpdateDto);
            return inventoryDto == null ? NotFound() : Ok(inventoryDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<InventoryDto>> Delete(int id)
        {
            var inventoryDto = await _inventoryService.Delete(id);
            return inventoryDto == null ? NotFound() : Ok(inventoryDto);
        }
    }
}
