namespace Single_Capstone.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Inventory
    {
        public int Id { get; set; }

        public int BusinessId { get; set; }

        public double TotalInventoryWorth { get; set; }

        public double ProfitMargin { get; set; }

        public string GetDate { get; set; }

        public int LastInventoryId { get; set; }

        public double COGS { get; set; }

        public double AverageInventory { get; set; }

        public double GMROI { get; set; }
    }
}
