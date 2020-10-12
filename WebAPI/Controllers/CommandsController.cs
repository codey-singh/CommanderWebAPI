using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;

namespace Commander.Controllers
{
    // api/commands
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        public CommandsController(ICommanderRepo repo, IMapper mapper)
        {
            _repository = repo;
            _mapper = mapper;
        }
        private readonly IMapper _mapper;
        private readonly ICommanderRepo _repository;

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var commands = _mapper.Map<IEnumerable<CommandReadDto>>(_repository.GetAllCommands());
            return Ok(commands);
        }
        [HttpGet("{id}", Name = "GetCommandById")]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            var command = _repository.GetCommandById(id);
            if (command != null)
                return Ok(_mapper.Map<CommandReadDto>(command));

            return NotFound();
        }

        [HttpPost]
        public ActionResult<CommandCreateDto> CreateCommand(CommandCreateDto command)
        {
            var commandModel = _mapper.Map<Command>(command);
            _repository.CreateCommand(commandModel);
            _repository.SaveChanges();
            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);
            return CreatedAtRoute(nameof(GetCommandById), new { Id = commandReadDto.Id }, commandReadDto);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto command)
        {
            var commandModelFromRepo = _repository.GetCommandById(id);
            if (commandModelFromRepo != null)
            {
                var mappedCommand = _mapper.Map(command, commandModelFromRepo);
                _repository.UpdateCommand(commandModelFromRepo);
                _repository.SaveChanges();
                return NoContent();
            }
            return NotFound();
        }

        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDocument)
        {
            var commandModelFromRepo = _repository.GetCommandById(id);
            if (commandModelFromRepo != null)
            {
                var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModelFromRepo);
                patchDocument.ApplyTo(commandToPatch, ModelState);
                if (!TryValidateModel(commandToPatch))
                {
                    return ValidationProblem(ModelState);
                }
                var mappedCommand = _mapper.Map(commandToPatch, commandModelFromRepo);
                _repository.UpdateCommand(commandModelFromRepo);
                _repository.SaveChanges();
                return NoContent();
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var commandModelFromRepo = _repository.GetCommandById(id);
            if (commandModelFromRepo != null)
            {
                _repository.DeleteCommand(commandModelFromRepo);
                return NoContent();
            }
            return NotFound();
        }
    }
}