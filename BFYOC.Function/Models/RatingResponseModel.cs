using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BFYOC.Function.Models
{
    public class RatingResponseModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public string UserId { get; set; }
        public string ProductId { get; set; }
        public string LocationName { get; set; }
        public int? Rating { get; set; }
        public string UserNotes { get; set; }
    }
}
