﻿namespace NZWalks.API.Models.Domin_Model
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }  

        //Navigation Property
        public Difficulty difficulty { get; set; }
        public Region Region { get; set; }  
    }
}
