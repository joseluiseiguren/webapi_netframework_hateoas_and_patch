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
    [RoutePrefix("api/persons/{personId}/addresses")]
    public class AddressesController : ApiController
    {
        [HttpGet]
        [Route("", Name = "PersonAllAddresses")]
        public IHttpActionResult Get(int personId)
        {
            //validate if the person exists
            if (new PersonRepository().GetById(personId) == null)
            {
                return NotFound();
            }
            
            //get data from repository
            var repository = new PersonAddressRepository();
            var addresses = repository.GetByFilter(personId);
            
            //convert model to dto
            var lstAddressDto = addresses.Select(p => p.ToAddressDto()).ToList();

            //fill hateoas information
            foreach (var item in lstAddressDto)
            {
                item._links = this.CreateHateoas(item.Id);
            }

            return this.Ok(lstAddressDto);
        }

        [HttpGet]
        [Route("{id:int}", Name = "PersonAddressWithId")]
        public IHttpActionResult Get(int personId, int id)
        {
            //get data from repository
            var personAddressRepository = new PersonAddressRepository();
            var address = personAddressRepository.GetById(personId, id);
            if (address == null)
            {
                return NotFound();
            }
            
            //convert model to dto
            var addressDto = address.ToAddressDto();

            //fill hateoas information
            addressDto._links = this.CreateHateoas(id);

            return this.Ok(addressDto);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int personId, int id)
        {
            //get data from repository
            var personAddressRepository = new PersonAddressRepository();
            var address = personAddressRepository.GetById(personId, id);
            if (address == null)
            {
                return NotFound();
            }

            //delete from repository
            personAddressRepository.Delete(id);

            return Ok();
        }

        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put(int personId, int id, [FromBody] AddressDtoRequest address)
        {
            //TODO: validate each field
            if (address == null)
            {
                return BadRequest();
            }

            //get data from repository
            var personAddressRepository = new PersonAddressRepository();
            var addressFromDb = personAddressRepository.GetById(personId, id);
            if (addressFromDb == null)
            {
                return NotFound();
            }

            addressFromDb.Number = address.Number;
            addressFromDb.Province = address.Province;
            addressFromDb.Street = address.Street;

            //update repository
            personAddressRepository.Update(addressFromDb);

            return Ok();
        }

        [HttpPatch]
        [Route("{id:int}")]
        public IHttpActionResult Patch(int personId, int id, [FromBody] JsonPatchDocument address)
        {
            //validate patch object
            if (address == null)
            {
                return BadRequest();
            }

            //only acept remove and replace operations
            foreach (var item in address.Operations)
            {
                if (item.OperationType != Marvin.JsonPatch.Operations.OperationType.Remove &&
                    item.OperationType != Marvin.JsonPatch.Operations.OperationType.Replace)
                {
                    return BadRequest();
                }

                //validate if the fields exists in object address
                if (!new PersonAddress().HasProperty(item.path.Replace("/", "")))
                {
                    return BadRequest();
                }

                //TODO: filter witch fields can be updated

            }

            //get data from repository
            var personAddressRepository = new PersonAddressRepository();
            var addressFromDb = personAddressRepository.GetById(personId, id);
            if (addressFromDb == null)
            {
                return NotFound();
            }

            //replace the requested properties on the object person
            foreach (var item in address.Operations)
            {
                PropertyInfo prop = addressFromDb.GetType().GetProperty(item.path.Replace("/", ""), BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

                if (item.OperationType == Marvin.JsonPatch.Operations.OperationType.Remove)
                {
                    prop.SetValue(addressFromDb, null, null);
                }

                if (item.OperationType == Marvin.JsonPatch.Operations.OperationType.Replace)
                {
                    prop.SetValue(addressFromDb, item.value, null);
                }
            }

            //make the update on database
            personAddressRepository.Update(addressFromDb);

            return Ok();
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Post(int personId, [FromBody] AddressDtoRequest address)
        {
            //TODO: validate each field
            if (address == null)
            {
                return BadRequest();
            }

            //validate if the person exists
            var personRepository = new PersonRepository();
            var personFromDb = personRepository.GetByFilter().Where(p => p.Id == personId).FirstOrDefault();
            if (personFromDb == null)
            {
                return NotFound();
            }

            //insert address into repository (and get id inserted)
            var addressInserted = new PersonAddress()
            {
                Id = 100,
                Number = address.Number,
                Province = address.Province,
                Street = address.Street,
                PersonId = personId
            };

            var personAddressRepository = new PersonAddressRepository();
            personAddressRepository.Create(addressInserted);

            //create dto to respond
            var addressResponse = addressInserted.ToAddressDto();

            //fill hateoas information
            addressResponse._links = this.CreateHateoas(addressInserted.Id);

            return Created(new Uri(Url.Link("PersonAddressWithId", new { addressInserted.Id })), addressResponse);
        }

        private List<Link> CreateHateoas(int id)
        {
            var result = new List<Link>();

            //add get self item
            result.Add(new Link()
            {
                Href = Url.Link("PersonAddressWithId", new { id }),
                Method = "GET"
            });

            //add post new item
            result.Add(new Link()
            {
                Href = Url.Link("PersonAllAddresses", null),
                Method = "POST"
            });

            //add post new item
            result.Add(new Link()
            {
                Href = Url.Link("PersonAddressWithId", new { id }),
                Method = "PATCH"
            });

            //add put existing item
            result.Add(new Link()
            {
                Href = Url.Link("PersonAddressWithId", new { id }),
                Method = "PUT"
            });

            //add delete existing item
            result.Add(new Link()
            {
                Href = Url.Link("PersonAddressWithId", new { id }),
                Method = "DELETE"
            });

            return result;
        }
    }
}
