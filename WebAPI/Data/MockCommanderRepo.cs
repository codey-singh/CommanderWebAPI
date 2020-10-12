using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data
{
    public class MockCommanderRepo : ICommanderRepo
    {
        public void CreateCommand(Command command)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCommand(Command command)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommands()
        {
            return new List<Command>()
            {
                new Command() { Id = 0, HowTo = "Boil an egg", Line = "Boil water", Platform = "Kettle and Pan" },
                new Command() { Id = 1, HowTo = "Make Ramen", Line = "Boil water", Platform = "Pan"}
            };
        }

        public Command GetCommandById(int id)
        {
            return new Command()
            {
                Id = 0,
                HowTo = "Boil an egg",
                Line = "Boil water",
                Platform = "Kettle and Pan"
            };
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCommand(Command command)
        {
            throw new System.NotImplementedException();
        }
    }
}
