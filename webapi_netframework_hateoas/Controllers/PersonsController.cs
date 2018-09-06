using Marvin.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using webapiexample.Dto;
using webapiexample.Extensions;
using webapiexample.Hatetoas;
using webapiexample.Models;
using webapiexample.Repository;

namespace webapiexample.Controllers
{
    [RoutePrefix("api/persons")]
    public class PersonsController : ApiController
    {
        [HttpGet]
        [Route("", Name = "AllPersons")]
        public IHttpActionResult Get()
        {
            //get data from repository
            var personRepository = new PersonRepository();
            var persons = personRepository.GetByFilter();

            //convert model to dto
            var lstPersonDto = persons.Select(p => p.ToPersonDto()).ToList();

            //fill hateoas information
            foreach (var item in lstPersonDto)
            {
                item._links = this.CreateHateoas(item.Id);
            }

            return this.Ok(lstPersonDto);
        }

        [HttpGet]
        [Route("{id:int}", Name = "PersonWithId")]
        public IHttpActionResult Get(int id)
        {
            //get data from repository
            var personRepository = new PersonRepository();
            var person = personRepository.GetByFilter().Where(p => p.Id == id).FirstOrDefault();
            if (person == null)
            {
                return NotFound();
            }

            //convert model to dto
            var personDto = person.ToPersonDto();

            //fill hateoas information
            personDto._links = this.CreateHateoas(id);

            return this.Ok(personDto);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            //get data from repository
            var personRepository = new PersonRepository();
            var person = personRepository.GetByFilter().Where(p => p.Id == id).FirstOrDefault();
            if (person == null)
            {
                return NotFound();
            }

            //delete from repository
            personRepository.Delete(id);

            return Ok();
        }

        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put(int id, [FromBody] PersonDtoRequest person)
        {
            //get data from repository
            var personRepository = new PersonRepository();
            var personFromDb = personRepository.GetById(id);
            if (personFromDb == null)
            {
                return NotFound();
            }

            personFromDb.Birthdate = person.Birthdate;
            personFromDb.Name = person.Name;

            //update repository
            personRepository.Update(personFromDb);

            return Ok();
        }

        [HttpPatch]
        [Route("{id:int}")]
        public IHttpActionResult Patch(int id, [FromBody] JsonPatchDocument person)
        {
            //validate patch object
            if (person == null)
            {
                return BadRequest();
            }

            //only acept remove and replace operations
            foreach (var item in person.Operations)
            {                
                if (item.OperationType != Marvin.JsonPatch.Operations.OperationType.Remove &&
                    item.OperationType != Marvin.JsonPatch.Operations.OperationType.Replace)
                {
                    return BadRequest();
                }

                //validate if the fields exists in object person
                if (!new Person().HasProperty(item.path.Replace("/", "")))
                {
                    return BadRequest();
                }

                //TODO: filter witch fields can be updated

            }

            //get data from repository
            var personRepository = new PersonRepository();
            var personFromDb = personRepository.GetByFilter().Where(p => p.Id == id).FirstOrDefault();
            if (personFromDb == null)
            {
                return NotFound();
            }

            //replace the requested properties on the object person
            foreach (var item in person.Operations)
            {
                PropertyInfo prop = personFromDb.GetType().GetProperty(item.path.Replace("/", ""), BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

                if (item.OperationType == Marvin.JsonPatch.Operations.OperationType.Remove)
                {                    
                    prop.SetValue(personFromDb, null, null);
                }

                if (item.OperationType == Marvin.JsonPatch.Operations.OperationType.Replace)
                {
                    prop.SetValue(personFromDb, item.value, null);
                }
            }

            //make the update on database
            personRepository.Update(personFromDb);

            return Ok();
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody] PersonDtoRequest person)
        {
            //TODO: validate each field
            if (person == null)
            {
                return BadRequest();
            }

            //insert person into repository (and get id inserted)
            var personInserted = new Person()
            {
                Birthdate = person.Birthdate,
                Name = person.Name
            };

            var personRepository = new PersonRepository();
            personRepository.Create(personInserted);
            
            //create dto to respond
            var personResponse = personInserted.ToPersonDto();

            //fill hateoas information
            personResponse._links = this.CreateHateoas(personInserted.Id);
            
            return Created(new Uri(Url.Link("PersonWithId", new { personInserted.Id })), personResponse);
        }

        private List<Link> CreateHateoas(int id)
        {
            var result = new List<Link>();

            //add get self item
            result.Add(new Link()
            {
                Href = Url.Link("PersonWithId", new { id }),
                Method = "GET"
            });

            //add post new item
            result.Add(new Link()
            {
                Href = Url.Link("AllPersons", null),
                Method = "POST"
            });

            //add post new item
            result.Add(new Link()
            {
                Href = Url.Link("PersonWithId", new { id }),
                Method = "PATCH"
            });

            //add put existing item
            result.Add(new Link()
            {
                Href = Url.Link("PersonWithId", new { id }),
                Method = "PUT"
            });

            //add delete existing item
            result.Add(new Link()
            {
                Href = Url.Link("PersonWithId", new { id }),
                Method = "DELETE"
            });

            return result;
        }
    }
}
