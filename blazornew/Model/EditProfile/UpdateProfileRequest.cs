namespace blazornew.Model.EditProfile
{
    public class UpdateProfileRequest
    {
        public string Name { get; set; }
        public string Phone_Number { get; set; }
        public string Gender { get; set; }
        public string Profile_Image { get; set; }
        public AddressRequests Address { get; set; }
    }

    public class AddressRequests
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

