﻿// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace DeepLibClient.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;

    public partial class Creator
    {
        /// <summary>
        /// Initializes a new instance of the Creator class.
        /// </summary>
        public Creator() { }

        /// <summary>
        /// Initializes a new instance of the Creator class.
        /// </summary>
        public Creator(int? creatorId = default(int?), string name = default(string), string surname = default(string), IList<MediaElement> mediaElements = default(IList<MediaElement>))
        {
            CreatorId = creatorId;
            Name = name;
            Surname = surname;
            MediaElements = mediaElements;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "creatorId")]
        public int? CreatorId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "surname")]
        public string Surname { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "mediaElements")]
        public IList<MediaElement> MediaElements { get; set; }

    }
}
