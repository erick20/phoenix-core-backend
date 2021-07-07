using Identity.Application.Enums;

namespace Identity.Application.Models.UserContext
{
    public class UserContext
    {
        public int CredentialId { get; set; }
        public int CustomerId { get; set; }
        public int RoleId { get; set; }
        public int RoleGroupId { get; set; }
        public int DeviceId { get; set; }
        public int? WarehouseId { get; set; }
        public string Magic { get; set; }
        public CustomerStateEnum CustomerState { get; set; }
        public long ExpDate { get; set; }


        public override bool Equals(object obj)
        {
            var other = obj as UserContext;

            if (other == null)
                return false;

            if (CredentialId != other.CredentialId || CustomerId != other.CustomerId ||
                RoleId != other.RoleId || RoleGroupId != other.RoleGroupId || DeviceId != other.DeviceId ||
                WarehouseId != other.WarehouseId || Magic != other.Magic ||
                CustomerState != other.CustomerState || ExpDate != other.ExpDate
                )
                return false;

            return true;
        }

    }
}
