using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text.Json;
using MapReader.Services;
using MapReader.Models;
using MapReader.Executors;

namespace Mapping.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MappingController : ControllerBase
    {
        private readonly MapExecutor _executor;
        private readonly PipelineExecutor _pipelineExecutor;

        public MappingController(MapExecutor executor, PipelineExecutor pipelineExecutor)
        {
            _executor = executor;
            _pipelineExecutor = pipelineExecutor;
        }

        [HttpPost]
        public IActionResult Execute([FromBody] FunctoidRequest request)
        {
            if (request == null)
                return BadRequest("Request body cannot be null.");

            try
            {
                var result = _executor.ExecuteFunctoid(
                    request.FunctoidType,
                    request.Inputs ?? new object[0],
                    request.Parameters ?? new Dictionary<string, object>()
                );

                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest($"Parameter error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while executing the functoid: {ex.Message}");
            }
        }

        [HttpPost("pipeline")]
        public IActionResult ExecutePipeline([FromBody] List<FunctoidRequest> steps)
        {
            if (steps == null || steps.Count == 0)
                return BadRequest("Request must include at least one step.");

            try
            {
                var json = JsonSerializer.Serialize(steps); 
                var result = _pipelineExecutor.ExecutePipeline(json);
                return Ok(new { result });
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest($"Parameter error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred during pipeline execution: {ex.Message}");
            }
        }
    }
}
