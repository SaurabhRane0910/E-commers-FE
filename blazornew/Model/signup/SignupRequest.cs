namespace blazornew.Model.signup
{
    public class SignupRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone_Number { get; set; }
        public string Gender { get; set; }
        public string Profile_Image { get; set; }

        public AddressRequest Address { get; set; }
    }

    public class AddressRequest
    {
        public int Country_Id { get; set; }
        public int State_Id { get; set; }
        public int City_Id { get; set; }
        public int Postal_Code { get; set; }
        public string Label { get; set; }
        public string Address_Line1 { get; set; }
        public string Address_Line2 { get; set; }
    }
}
