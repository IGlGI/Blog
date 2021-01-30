﻿using System.Collections.Generic;

namespace BogApp.Models
{
    public record AppSettings
    {
        public string AppConnectionString { get; set; }

        public string IdentityServerConnectionString { get; set; }

        public string DbConnectionString { get; set; }

        public string DbName { get; set; }

        public List<string> AllowedCors { get; set; }
    }
}
