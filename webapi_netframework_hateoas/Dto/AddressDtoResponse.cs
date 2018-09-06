using webapiexample.Hatetoas;

namespace webapiexample.Dto
{
    public class AddressDtoResponse : LinkHelper
    {
        public int Id { get; set; }

        public string Street { get; set; }

        public int Number { get; set; }

        public string Province { get; set; }
    }
}