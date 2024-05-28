using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace api.Models
{
    public class Buyer:IdentityUser
    {
        [JsonIgnore]
        public List<Pocket> Pockets {get;set;} = new List<Pocket>();



        public List<UserLikes> UserLikes {get;set;} = new List<UserLikes>();

        [JsonIgnore]
		public override string? Id { get; set; }

        [JsonIgnore]
		public override string? PasswordHash { get; set; }

        [JsonIgnore]
		public override bool EmailConfirmed { get; set; }

        [JsonIgnore]
		public override bool LockoutEnabled { get; set; }

        [JsonIgnore]
		public override int AccessFailedCount { get; set; }

        [JsonIgnore]
		public override string? PhoneNumber { get; set; }

        [JsonIgnore]
		public override bool PhoneNumberConfirmed { get; set; }

        [JsonIgnore]
		public override string ?SecurityStamp { get; set; }

        [JsonIgnore]
		public override string ?NormalizedUserName{ get; set; }

        [JsonIgnore]
		public override string ?NormalizedEmail{ get; set; }

        [JsonIgnore]
		public override string ?ConcurrencyStamp{ get; set; }

        [JsonIgnore]
		public override bool TwoFactorEnabled { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public override DateTimeOffset? LockoutEnd { get; set; }

        
    }
}