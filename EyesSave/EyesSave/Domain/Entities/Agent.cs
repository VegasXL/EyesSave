using EyesSave.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace EyesSave.Domain.Entities
{
    public partial class Agent
    {
        private string? _logo;
        private string _phone;
        public Agent()
        {
            AgentPriorityHistories = new HashSet<AgentPriorityHistory>();
            ProductSales = new HashSet<ProductSale>();
            Shops = new HashSet<Shop>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int AgentTypeId { get; set; }
        public string? Address { get; set; }
        public string Inn { get; set; } = null!;
        public string? Kpp { get; set; }
        public string? DirectorName { get; set; }
        public string Phone
        {
            get
            {
                return "+7 " + _phone;
            }
        }
        public string? Email { get; set; }
        public string? Logo
        {
            get
            {
                return _logo = (_logo == "" || _logo == null) ? "\\Resources\\picture.png" : "\\Resources" + _logo;
            }
        }
        public int Priority { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return AgentType.Title + " | " + Title;
            }
        }

        private string _sell;

        [NotMapped]
        public string Sell
        {
            get 
            {
                int sel = 0;
                if (ProductSales.Count == 0)
                {
                    sel = 0;
                }
                else
                {
                    var listSales = ProductSales.ToList();
                    var maxData = listSales[0].SaleDate.Year;
                    foreach (var item in ProductSales)
                    {
                        if (maxData < item.SaleDate.Year)
                        {
                            maxData = item.SaleDate.Year;
                        }
                    }

                    foreach (var item in ProductSales)
                    {
                        if (maxData == item.SaleDate.Year)
                        {
                            sel += item.ProductCount;
                        }
                    }
                }
                return sel + " продаж за год";
            }
            set { _sell = value; }
        }


        [NotMapped]
        public string Prior
        {
            get
            {
                return "Приоритет: " + Priority;
            }
        }

        private string _procent;
        [NotMapped]
        public string Procent
        {
            get
            {
                decimal sum = 0;
                if (ProductSales.Count == 0)
                {
                    sum = 0;
                }
                else
                {
                    foreach (var item in ProductSales)
                    {
                        sum += item.ProductCount * item.Product.MinCostForAgent;
                    }
                }

                if (sum <= 10000)
                {
                    _procent = "0%";
                }
                else if (sum <= 50000)
                {
                    _procent = "5%";
                }
                else if (sum <= 150000)
                {
                    _procent = "10%";
                }
                else if (sum <= 500000)
                {
                    _procent = "20%";
                }
                else if (sum > 500000)
                {
                    _procent = "25%";
                }
                return _procent;

            }
            set { _procent = value; }
        }

        public virtual AgentType AgentType { get; set; } = null!;
        public virtual ICollection<AgentPriorityHistory> AgentPriorityHistories { get; set; }
        public virtual ICollection<ProductSale> ProductSales { get; set; }
        public virtual ICollection<Shop> Shops { get; set; }
    }
}
