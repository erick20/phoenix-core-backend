﻿using Identity.Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.UserContext
{
    public class UserContext
    {
        public int CredentialId { get; set; }
        public int CustomerId { get; set; }
        public int RoleId { get; set; }
        public int RoleGroupId { get; set; }
        public int DeviceId { get; set; }
        public int? WarehouseId { get; set; }
        public string Email { get; set; }
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
                WarehouseId != other.WarehouseId || Email != other.Email ||
                Magic != other.Magic || CustomerState != other.CustomerState ||
                ExpDate != other.ExpDate

                )
                return false;

            return true;
        }

    }
}
