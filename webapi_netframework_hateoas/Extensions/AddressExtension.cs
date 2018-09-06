using webapiexample.Dto;
using webapiexample.Models;

namespace webapiexample.Extensions
{
    public static class AddressExtension
    {
        public static AddressDtoResponse ToAddressDto(this PersonAddress address)
        {
            var result = new AddressDtoResponse()
            {
                Id = address.Id,
                Number = address.Number,
                Province = address.Province,
                Street = address.Street
            };

            return result;
        }
    }
}