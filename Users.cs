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
            var s = "========OBJECT========\n\n";
            s += "id: " + this.id + "\n";
            s += "FirstName: " + this.FirstName + "\n";
            s += "MiddleInitial: " + this.MiddleInitial + "\n";
            s += "LastName: " + this.LastName + "\n";
            s += "SubscriberID: " + this.SubscriberID + "\n";
            s += "DepdentCode: " + this.DepdentCode + "\n";
            s += "GroupID: " + this.GroupID + "\n";
            s += "Gender: " + this.Gender + "\n";
            s += "DoB: " + this.DOB + "\n";
            s += "Address 1: " + this.AddressLine1 + "\n";
            s += "Address 2: " + this.AddressLine2 + "\n";
            s += "City: " + this.City + "\n";
            s += "State: " + this.State + "\n";
            s += "Postal: " + this.PostalCode + "\n";
            s += "HomePhone: " + this.HomePhone + "\n";
            s += "CellPhone: " + this.CellPhone + "\n";
            s += "Email: " + this.Email + "\n";
            return s;
        }
    }
}