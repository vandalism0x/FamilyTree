using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FamilyTree
{
    public class ProjectContainer
    {
        private Model1Container model1 = new Model1Container();
        public int Id;
        public List<Event> Events;
        public List<Material> Materials;
        public List<Person> Persons;

        public ProjectContainer()
        {
            Events = new List<Event>();
            Materials = new List<Material>();
            Persons = new List<Person>();
        }

        public ProjectContainer(Project pr)
        {
            Id = pr.Id;
            Persons = pr.Persons.ToList();
        }
    }
}
