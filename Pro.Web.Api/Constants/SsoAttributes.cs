namespace Pro.Web.Api.Constants
{
    public class SsoAttributes
    {
        [Flags]
        public enum SsoAttribute
        {
            FirstName = 1 << 0, //1
            LastName = 1 << 1, //2
            Email = 1 << 2, // 4 
            AgencyName = 1 << 3,  //8
            PostalCode = 1 << 4,//16
            Phone = 1 << 5,//32
            MobilePhone = 1 << 6,//64
            RealEstateLicense = 1 << 7,//128
        }
    }
}
