﻿using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace OzPerksApi.Models
{
    public class User : DocumentEntity
    {
        [BsonElement("fullname")]
        public string FullName { get; set; } = string.Empty;

        [BsonElement("email")]
        public string Email { get; set; } = string.Empty;

        [BsonElement("role")]
        public string Role { get; set; } = string.Empty;

        [BsonElement("phone")]
        public string Phone { get; set; } = string.Empty;
    }
}
