﻿using System;

namespace DiabloII.Application.Responses.Read.Domains.Items
{
    public class ItemPropertyDto
    {
        public Guid Id { get; set; }

        public string FormattedName { get; set; }

        public string Name { get; set; }

        public double Par { get; set; }

        public double Minimum { get; set; }

        public double Maximum { get; set; }

        public bool IsPercent { get; set; }

        public string FirstChararacter { get; set; }

        public double OrderIndex { get; set; }
    }
}