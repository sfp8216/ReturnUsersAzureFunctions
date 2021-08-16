namespace Poc1
{
    public class Users
    {
        public string id { get; set; }

        public string GroupID { get; set; }

        public string SubscriberID { get; set; }
        public string DepdentCode { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string HomePhone { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}