namespace webapiexample.Dto
{
    //this class is used to receive post, put and patch
    public class AddressDtoRequest
    {
        public string Street { get; set; }

        public int Number { get; set; }

        public string Province { get; set; }
    }
}