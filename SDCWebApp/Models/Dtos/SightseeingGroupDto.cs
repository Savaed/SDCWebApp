﻿using System;

namespace SDCWebApp.Models.Dtos
{
    public class SightseeingGroupDto : DtoBase
    {
        public DateTime SightseeingDate { get; set; }
        public int MaxGroupSize { get; set; }
        public int CurrentGroupSize { get; set; }
        public bool IsAvailablePlace { get; set; }
    }
}